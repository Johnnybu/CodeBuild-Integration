using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Runtime;
using NightlyBuilds.Model;
using AmazonService;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace NightlyBuilds
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that starts a CodeBuild build of project name input.
        /// </summary>
        /// <param name="projectName">The name of the CodeBuild project to build.</param>
        /// <param name="buildSpec">The name of the buildspec the build should use.</param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<AmazonWebServiceResponse> NightlyBuildHandlerAsync(Input input, ILambdaContext context)
        {
            return await new CodeBuildService()
                .StartBuildAsync(input.ProjectName, input.BuildSpec);
        }
    }
}
