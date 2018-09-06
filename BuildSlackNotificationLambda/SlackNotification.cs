using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using InputModel;
using CodeBuildIntegration;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace BuildSlackNotification
{
    public class SlackNotification
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<bool> SlackMessageHandlerAsync(Event input, ILambdaContext context)
        {
            var message = string.Format("The {2} project has reported a {3} state during the {0} phase. \n"  +
                    "<https://{1}.console.aws.amazon.com/codebuild/home?region={1}#/projects/{2}/view|" + 
                    "Click here> to view build history",
                    input.Detail.CurrentPhase, input.Region, input.Detail.ProjectName, input.Detail.BuildStatus);

            return await new SlackWebHookService()
                    .SendSlackMessageAsync(message, "#nrrc-app-support", 
                    "AWSCodeBuildBot", "RobotFace");
        }
    }
}
