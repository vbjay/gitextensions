using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Stash
{
    internal class GetInBetweenCommitsRequest : StashRequestBase<List<Commit>>
    {
        private readonly Commit _sourceCommit;
        private readonly Repository _sourceRepo;
        private readonly Commit _targetCommit;
        private readonly Repository _targetRepo;

        public GetInBetweenCommitsRequest(Repository sourceRepo, Repository targetRepo,
            Commit sourceCommit, Commit targetCommit, Settings settings)
            : base(settings)
        {
            _sourceRepo = sourceRepo;
            _targetRepo = targetRepo;
            _sourceCommit = sourceCommit;
            _targetCommit = targetCommit;
        }

        protected override string ApiUrl
        {
            get
            {
                return string.Format(
                    "/projects/{0}/repos/{1}/commits?until={2}&since={3}&secondaryRepositoryId={4}&start=0&limit=10",
                    _sourceRepo.ProjectKey, _sourceRepo.RepoName,
                    _sourceCommit.Hash, _targetCommit.Hash, _targetRepo.Id);
            }
        }

        protected override object RequestBody
        {
            get { return null; }
        }

        protected override Method RequestMethod
        {
            get { return Method.GET; }
        }

        protected override List<Commit> ParseResponse(JObject json)
        {
            var result = new List<Commit>();
            foreach (JObject commit in json["values"])
            {
                result.Add(Commit.Parse(commit));
            }
            return result;
        }
    }
}
