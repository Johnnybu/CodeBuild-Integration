using System;
using System.Linq;
using Amazon.Lambda.Core;
using BuildEmailNotification.Model;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace BuildEmailNotification
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public void FunctionHandler(Event input, ILambdaContext context)
        {
            var logs = new CloudWatchLogsService()
            {
                LogGroupName = @"/aws/codebuild/" + input.Detail.ProjectName,
                LogStreamName = input.Detail.BuildId.Split('/').Last().Split(':').Last(),
                LogRowLimit = 25
            };

            var email = new SimpleEmailService()
            {
                Subject = String.Format("Build {0} failed on phase {1} at {2}.", input.Detail.ProjectName,
                    input.Detail.CurrentPhase, input.Time),
                Body = String.Format("The build with id of {0} has failed. \n" +
                            "Here are the last 25 lines of the log: \n {1}", input.Detail.BuildId,
                            logs.GetCloudWatchLogEvents())
            };

            email.SendEmail();
        }
    }
}
