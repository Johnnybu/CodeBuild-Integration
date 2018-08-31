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
            var message = string.Format("A CodeBuild project named: {2} has failed while executing the phase: {0}. \n" +
                    "You may review the build history on the AWS console at the following link: " +
                    "<https://{1}.console.aws.amazon.com/codebuild/home?region={1}#/projects/{2}/view|Click Here>",
                    input.Detail.CurrentPhase, input.Region, input.Detail.ProjectName);

            return await new SlackWebHookService().SendSlackMessageAsync(message, "#general", "AWSCodeBuildBot", "RobotFace");
        }
    }
}
