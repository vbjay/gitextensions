using System;
using System.IO;
using System.Net;
using System.Text;
using Git.hub;
using GitUIPluginInterfaces.RepositoryHosts;

namespace Github3
{
    internal class GithubPullRequest : IPullRequestInformation
    {
        private IHostedRepository _BaseRepo;
        private string _diffData;
        private IPullRequestDiscussion _Discussion;
        private IHostedRepository _HeadRepo;
        private PullRequest pullrequest;

        public GithubPullRequest(PullRequest pullrequest)
        {
            this.pullrequest = pullrequest;
        }

        public string BaseRef
        {
            get { return pullrequest.Base.Ref; }
        }

        public IHostedRepository BaseRepo
        {
            get
            {
                if (_BaseRepo == null)
                    _BaseRepo = new GithubRepo(pullrequest.Base.Repo);

                return _BaseRepo;
            }
        }

        public string BaseSha
        {
            get { return pullrequest.Base.Sha; }
        }

        public string Body
        {
            get { return pullrequest.Body; }
        }

        public DateTime Created
        {
            get { return pullrequest.CreatedAt; }
        }

        public string DetailedInfo
        {
            get { return string.Format("Base repo owner: {0}\nHead repo owner: {1}", BaseRepo.Owner, HeadRepo.Owner); }
        }

        public string DiffData
        {
            get
            {
                if (_diffData == null)
                {
                    HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(pullrequest.DiffUrl);
                    using (var response = wr.GetResponse())
                    using (var respStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        _diffData = respStream.ReadToEnd();
                    }
                }
                return _diffData;
            }
        }

        public IPullRequestDiscussion Discussion
        {
            get
            {
                if (_Discussion == null)
                    _Discussion = new GithubPullRequestDiscussion(pullrequest);

                return _Discussion;
            }
        }

        public string HeadRef
        {
            get { return pullrequest.Head.Ref; }
        }

        public IHostedRepository HeadRepo
        {
            get
            {
                if (_HeadRepo == null)
                    _HeadRepo = new GithubRepo(pullrequest.Head.Repo);

                return _HeadRepo;
            }
        }

        public string HeadSha
        {
            get { return pullrequest.Head.Sha; }
        }

        public string Id
        {
            get { return pullrequest.Number.ToString(); }
        }

        public string Owner
        {
            get { return pullrequest.User.Login; }
        }

        public string Title
        {
            get { return pullrequest.Title; }
        }

        public void Close()
        {
            pullrequest.Close();
        }
    }
}
