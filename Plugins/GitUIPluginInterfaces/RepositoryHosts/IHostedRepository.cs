using System.Collections.Generic;

namespace GitUIPluginInterfaces.RepositoryHosts
{
    public interface IHostedBranch
    {
        string Name { get; }
        string Sha { get; }
    }

    public interface IHostedRepository
    {
        //Slow op
        List<IHostedBranch> Branches { get; }

        string CloneReadOnlyUrl { get; }
        string CloneReadWriteUrl { get; }
        string Description { get; }
        int Forks { get; }
        string Homepage { get; }
        bool IsAFork { get; }
        bool IsMine { get; }
        bool IsPrivate { get; }
        string Name { get; }
        string Owner { get; }
        string ParentOwner { get; }
        string ParentReadOnlyUrl { get; }

        /// <returns>Pull request number</returns>
        int CreatePullRequest(string myBranch, string remoteBranch, string title, string body);

        /// <summary>
        /// Forks the repo owned by somebody else to "my" repos.
        /// </summary>
        /// <returns>The new repo, owne by me.</returns>
        IHostedRepository Fork();

        List<IPullRequestInformation> GetPullRequests();
    }
}
