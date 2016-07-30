using System;
using System.Reactive.Concurrency;

namespace GitUIPluginInterfaces.BuildServerIntegration
{
    public interface IBuildServerAdapter : IDisposable
    {
        /// <summary>
        /// Gets a unique key which identifies this build server.
        /// </summary>
        string UniqueKey { get; }

        IObservable<BuildInfo> GetFinishedBuildsSince(IScheduler scheduler, DateTime? sinceDate = null);

        IObservable<BuildInfo> GetRunningBuilds(IScheduler scheduler);

        void Initialize(IBuildServerWatcher buildServerWatcher, ISettingsSource config);
    }
}
