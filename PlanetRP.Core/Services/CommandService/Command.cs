using PlanetRP.Core.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Core.Services.CommandService
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Command
        : Attribute
    {
        public string Name { get; }
        public bool GreedyArg { get; }
        public string[] Aliases { get; }
        public AccessLevel RequiredAccessLevel { get; }

#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
        public Command(string name, bool greedyArg = false, string[] aliases = null, AccessLevel requiredAccessLevel = AccessLevel.Player)
#pragma warning restore CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
        {
            Name = name;
            GreedyArg = greedyArg;
            Aliases = aliases;


            RequiredAccessLevel = requiredAccessLevel;
        }
    }
}
