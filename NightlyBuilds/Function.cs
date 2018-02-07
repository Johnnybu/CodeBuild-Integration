using System;
using System.Net;
using System.Net.Http;
using Amazon;
using Amazon.CodeBuild;
using Amazon.CodeBuild.Model;
using Amazon.Lambda.Core;

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
        public string FunctionHandler(Input input, ILambdaContext context)
        {
            var client = new AmazonCodeBuildClient(RegionEndpoint.USEast2);

            var request = new StartBuildRequest
            {
                ProjectName = input.ProjectName,
                BuildspecOverride = input.BuildSpec
            };

            try
            {
                var response = client.StartBuildAsync(request);
                if (response.Result.HttpStatusCode == HttpStatusCode.OK)
                {
                    return response.Result.HttpStatusCode.ToString();
                }
                else
                {
                    throw new HttpRequestException("An error occured while attempting to start the specified build");
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }

    public class Input
    {
        public Input()
        {
            BuildSpec = "buildspec.yml";
        }

        public string ProjectName { get; set; }

        public string BuildSpec { get; set; }
    }
}
