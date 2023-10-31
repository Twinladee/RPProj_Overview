using AltV.Net.Elements.Entities;
using AltV.Net.Elements.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Core.Callbacks
{
    public class AsyncFunctionCallback<T> : IAsyncBaseObjectCallback<T> where T : IBaseObject
    {
        private readonly Func<T, Task> callback;

        public AsyncFunctionCallback(Func<T, Task> callback)
        {
            this.callback = callback;
        }

        public Task OnBaseObject(T baseObject)
        {
            return callback(baseObject);
        }
    }
}
