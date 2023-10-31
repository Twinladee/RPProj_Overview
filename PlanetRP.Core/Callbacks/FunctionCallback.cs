using AltV.Net.Elements.Entities;
using AltV.Net.Elements.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Core.Callbacks
{
    public class FunctionCallback<T> : IBaseObjectCallback<T> where T : IBaseObject
    {
        private readonly Action<T> callback;

        public FunctionCallback(Action<T> callback)
        {
            this.callback = callback;
        }

        public void OnBaseObject(T baseObject)
        {
            callback(baseObject);
        }
    }
}
