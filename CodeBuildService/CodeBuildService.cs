using Amazon.CodeBuild;
using Amazon.CodeBuild.Model;
using System.Threading.Tasks;

namespace AmazonService
{
    public class CodeBuildService : ServiceBase
    {
        public CodeBuildService()
        {
            Client = new AmazonCodeBuildClient(GetRegionEndpoint("CBS_REGION"));
        }

        public async Task<StartBuildResponse> StartBuildAsync(string projectName, string buildSpec)
        {
            var request = new StartBuildRequest
            {
                ProjectName = projectName,
                BuildspecOverride = buildSpec
            };

            return await (Client as AmazonCodeBuildClient).StartBuildAsync(request);
        }
    }
}
