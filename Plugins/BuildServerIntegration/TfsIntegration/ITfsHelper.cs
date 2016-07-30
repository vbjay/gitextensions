using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TfsInterop.Interface
{
    public enum BuildStatus
    {
        Unknown,
        InProgress,
        Success,
        Failure,
        Unstable,
        Stopped
    }

    public interface IBuild
    {
        string Description { get; set; }
        string Id { get; set; }
        bool IsFinished { get; set; }
        string Label { get; set; }
        string Revision { get; set; }
        DateTime StartDate { get; set; }
        BuildStatus Status { get; set; }
        string Url { get; set; }
    }

    public interface ITfsHelper : IDisposable
    {
        void ConnectToTfsServer(string hostname, string teamCollection, string projectName, Regex buildDefinitionFilter = null);

        bool IsDependencyOk();

        IList<IBuild> QueryBuilds(DateTime? sinceDate, bool? running);
    }
}
