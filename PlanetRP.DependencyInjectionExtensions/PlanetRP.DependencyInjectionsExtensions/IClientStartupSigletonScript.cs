using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.DependencyInjectionsExtensions.PlanetRP.DependencyInjectionsExtensions
{
    /// <summary>
    /// Same as <see cref="ISingletonScript"/>, but additionally the script gets constructed as soon as the client starts.
    /// </summary>
    public interface IClientStartupSigletonScript
    {
        void InitialiazeService();
    }
}
