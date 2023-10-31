namespace PlanetRP.DependencyInjectionsExtensions
{
    public interface IServerJob
    {
        Task OnStartup();

        Task OnSave();

        Task OnShutdown();
    }
}
