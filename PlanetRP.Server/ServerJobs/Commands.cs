using AltV.Net.Elements.Args;
using AltV.Net.Elements.Entities;
using AltV.Net.FunctionParser;
using AltV.Net;
using PlanetRP.DependencyInjectionsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PlanetRP.Core.Entities;
using PlanetRP.Core.Constants.Enums;
using AltV.Net.Async;
using PlanetRP.Core.Services.CommandService;

namespace PlanetRP.Server.ServerJobs
{
    public class Commands
        : IServerJob
    {
        private class RestrictedAccessCommandDelegate
        {
            public RestrictedAccessCommandDelegate(CommandDelegate action, AccessLevel requiredAccessLevel)
            {
                Action = action;
                RequiredAccessLevel = requiredAccessLevel;
            }

            public CommandDelegate Action { get; }

            public AccessLevel RequiredAccessLevel { get; }
        }

        public Commands(
            IEnumerable<IStartupSingletonScript> startupSingletonScripts,
            IEnumerable<ISingletonScript> singletonScripts,
            IEnumerable<ITransientScript> transientScripts)
        {
            foreach (var script in startupSingletonScripts)
            {
                RegisterEvents(script);
            }
            foreach (var script in singletonScripts)
            {
                RegisterEvents(script);
            }
            foreach (var script in transientScripts)
            {
                RegisterEvents(script);
            }

            Alt.OnClient<PlanetPlayer, string>("Commands:Execute", OnCommandRequest, OnCommandRequestParser);
            //TODO AltAsync.OnClient<PlanetPlayer, string, Task>("Commands:Execute", OnCommandRequest, OnCommandRequestParser);
        }

        #region IServerJob

        public async Task OnStartup()
        {
            await Task.CompletedTask;
        }

        public async Task OnSave()
        {
            await Task.CompletedTask;
        }

        public async Task OnShutdown()
        {
            Functions.Clear();

            foreach (var handle in Handles)
            {
                handle.Free();
            }

            Handles.Clear();

            await Task.CompletedTask;
        }

        #endregion

        #region CommandDoesNotExists

        private static readonly HashSet<CommandDoesNotExistDelegate> CommandDoesNotExistDelegates =
            new HashSet<CommandDoesNotExistDelegate>();

        public delegate void CommandDoesNotExistDelegate(PlanetPlayer player, string command);

        public static event CommandDoesNotExistDelegate OnCommandDoesNotExist
        {
            add => CommandDoesNotExistDelegates.Add(value);
            remove => CommandDoesNotExistDelegates.Remove(value);
        }

        #endregion

        #region CommandAccessLevelViolation

        private static readonly HashSet<CommandAccessLevelViolationDelegate> CommandAccessViolationDelegates =
            new HashSet<CommandAccessLevelViolationDelegate>();

        public delegate void CommandAccessLevelViolationDelegate(PlanetPlayer player, string command);

        public static event CommandAccessLevelViolationDelegate OnCommandAccessViolation
        {
            add => CommandAccessViolationDelegates.Add(value);
            remove => CommandAccessViolationDelegates.Remove(value);
        }

        #endregion

        private delegate void CommandDelegate(PlanetPlayer player, string[] arguments);

        private static readonly LinkedList<Function> Functions = new LinkedList<Function>();

        private static readonly LinkedList<GCHandle> Handles = new LinkedList<GCHandle>();

        private readonly IDictionary<string, LinkedList<RestrictedAccessCommandDelegate>> commandDelegates =
            new Dictionary<string, LinkedList<RestrictedAccessCommandDelegate>>();

        private static readonly string[] EmptyArgs = new string[0];

        private static void OnCommandRequestParser(IPlayer player, MValueConst[] mValueArray, Action<PlanetPlayer, string> action)
        {
            if (mValueArray.Length != 1) return;
            var arg = mValueArray[0];
            if (arg.type != MValueConst.Type.String) return;
            action((PlanetPlayer)player, arg.GetString());
        }

        //private static void OnChatMessageParser(IPlayer player, MValueConst[] mValueArray,
        //    Action<IPlayer, string> action)
        //{
        //    if (mValueArray.Length != 1) return;
        //    var arg = mValueArray[0];
        //    if (arg.type != MValueConst.Type.String) return;
        //    action(player, arg.GetString());
        //}

        //private static void OnChatMessageParserServer(MValueConst[] mValueArray, Action<IPlayer, string> action)
        //{
        //    if (mValueArray.Length != 1) return;
        //    var argMsg = mValueArray[0];
        //    if (argMsg.type != MValueConst.Type.String) return;
        //    action(null, argMsg.GetString());
        //}

        private void OnCommandRequest(PlanetPlayer player, string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            if (!message.StartsWith("/")) return;
            message = message.Trim().Remove(0, 1);
            if (message.Length > 0)
            {
                var args = message.Split(' ');
                var argsLength = args.Length;
                if (argsLength < 1) return;
                var cmd = args[0];

                LinkedList<RestrictedAccessCommandDelegate> delegates;

                if (argsLength < 2)
                {
                    if (commandDelegates.TryGetValue(cmd, out delegates) && delegates.Count > 0)
                    {
                        foreach (var commandDelegate in delegates)
                        {
                            //if (commandDelegate.RequiredAccessLevel <= player.Account.AccessLevel)
                            //{
                            commandDelegate.Action(player, EmptyArgs);
                            //}
                            //else
                            //{
                            //    foreach (var accessViolationDelegate in CommandAccessViolationDelegates)
                            //    {
                            //        accessViolationDelegate(player, cmd);
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        foreach (var doesNotExistsDelegate in CommandDoesNotExistDelegates)
                        {
                            doesNotExistsDelegate(player, cmd);
                        }
                    }

                    return;
                }

                var argsArray = new string[argsLength - 1];
                Array.Copy(args, 1, argsArray, 0, argsLength - 1);
                if (commandDelegates.TryGetValue(cmd, out delegates) && delegates.Count > 0)
                {
                    foreach (var commandDelegate in delegates)
                    {
                        //if (commandDelegate.RequiredAccessLevel <= player.Account.AccessLevel)
                        //{
                        commandDelegate.Action(player, argsArray);
                        //}
                        //else
                        //{
                        //    foreach (var accessViolationDelegate in CommandAccessViolationDelegates)
                        //    {
                        //        accessViolationDelegate(player, cmd);
                        //    }
                        //}
                    }
                }
                else
                {
                    foreach (var doesNotExistsDelegate in CommandDoesNotExistDelegates)
                    {
                        doesNotExistsDelegate(player, cmd);
                    }
                }
            }
        }

        private void RegisterEvents(object target)
        {
            ModuleScriptMethodIndexer.Index(target, new[] { typeof(Command), typeof(CommandEvent) },
                (baseEvent, eventMethod, eventMethodDelegate) =>
                {
                    switch (baseEvent)
                    {
                        case Command command:
                            {
                                var commandName = command.Name ?? eventMethod.Name;

                                Handles.AddLast(GCHandle.Alloc(eventMethodDelegate));

                                var function = Function.Create(Alt.Core, eventMethodDelegate);

                                if (function == null)
                                {
                                    Alt.Log($"Unsupported Command method: {eventMethod}");
                                    return;
                                }

                                Functions.AddLast(function);

                                if (!commandDelegates.TryGetValue(commandName, out var delegates))
                                {
                                    delegates = new LinkedList<RestrictedAccessCommandDelegate>();
                                    commandDelegates[commandName] = delegates;
                                }

                                if (command.GreedyArg)
                                {
                                    delegates.AddLast(new RestrictedAccessCommandDelegate(
                                        (player, arguments) =>
                                        {
                                            function.Call(player, new[] { string.Join(" ", arguments) });
                                        },
                                        command.RequiredAccessLevel)
                                    );
                                }
                                else
                                {
                                    delegates.AddLast(new RestrictedAccessCommandDelegate(
                                        (player, arguments) =>
                                        {
                                            function.Call(player, arguments);
                                        },
                                        command.RequiredAccessLevel)
                                    );
                                }

                                var aliases = command.Aliases;
                                if (aliases != null)
                                {
                                    foreach (var alias in aliases)
                                    {
                                        if (!commandDelegates.TryGetValue(alias, out delegates))
                                        {
                                            delegates = new LinkedList<RestrictedAccessCommandDelegate>();
                                            commandDelegates[alias] = delegates;
                                        }

                                        if (command.GreedyArg)
                                        {
                                            delegates.AddLast(new RestrictedAccessCommandDelegate(
                                                (player, arguments) =>
                                                {
                                                    function.Call(player, new[] { string.Join(" ", arguments) });
                                                },
                                                command.RequiredAccessLevel)
                                            );
                                        }
                                        else
                                        {
                                            delegates.AddLast(new RestrictedAccessCommandDelegate(
                                                (player, arguments) =>
                                                {
                                                    function.Call(player, arguments);
                                                },
                                                command.RequiredAccessLevel)
                                            );
                                        }
                                    }
                                }

                                break;
                            }

                        case CommandEvent commandEvent:
                            {
                                var commandEventType = commandEvent.EventType;
                                ScriptFunction scriptFunction;

                                switch (commandEventType)
                                {
                                    case CommandEventType.NotFound:
#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
                                        scriptFunction = ScriptFunction.Create(eventMethodDelegate,
                                            new[] { typeof(PlanetPlayer), typeof(string) });
#pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.

                                        if (scriptFunction == null) return;

                                        OnCommandDoesNotExist += (player, commandName) =>
                                        {
                                            scriptFunction.Set(player);
                                            scriptFunction.Set(commandName);
                                            scriptFunction.Call();
                                        };

                                        break;

                                    case CommandEventType.AccessLevelViolation:
#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
                                        scriptFunction = ScriptFunction.Create(eventMethodDelegate,
                                            new[] { typeof(PlanetPlayer), typeof(string) });
#pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.

                                        if (scriptFunction == null) return;

                                        OnCommandAccessViolation += (player, commandName) =>
                                        {
                                            scriptFunction.Set(player);
                                            scriptFunction.Set(commandName);
                                            scriptFunction.Call();
                                        };

                                        break;
                                }

                                break;
                            }
                    }
                });
        }
    }
}
