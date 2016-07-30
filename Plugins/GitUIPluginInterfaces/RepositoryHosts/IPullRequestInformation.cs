using System;
using System.Collections.Generic;

namespace GitUIPluginInterfaces.RepositoryHosts
{
    public interface ICommitDiscussionEntry : IDiscussionEntry
    {
        string Sha { get; }
    }

    public interface IDiscussionEntry
    {
        string Author { get; }
        string Body { get; }
        DateTime Created { get; }
    }

    public interface IPullRequestDiscussion
    {
        List<IDiscussionEntry> Entries { get; }

        void ForceReload();

        void Post(string data);
    }

    public interface IPullRequestInformation
    {
        string BaseRef { get; }
        IHostedRepository BaseRepo { get; }
        string BaseSha { get; }
        string Body { get; }
        DateTime Created { get; }
        string DetailedInfo { get; }
        string DiffData { get; }
        IPullRequestDiscussion Discussion { get; }
        string HeadRef { get; }
        IHostedRepository HeadRepo { get; }
        string HeadSha { get; }
        string Id { get; }
        string Owner { get; }
        string Title { get; }

        void Close();
    }
}
