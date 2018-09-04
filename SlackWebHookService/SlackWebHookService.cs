using AmazonService;
using Slack.Webhooks;
using System;
using System.Threading.Tasks;

namespace CodeBuildIntegration
{
    public class SlackWebHookService : ServiceBase
    {
        public async Task<bool> SendSlackMessageAsync(string message, string channel, string userName, string icon)
        {
            var uri = await new SimpleSystemsManagementService()
                .GetParameterValueAsync(GetEnvironmentVariableValue("SLACK_URI"));

            var slackClient = new SlackClient(uri);


            if (!Enum.TryParse<Emoji>(icon, out Emoji emoji))
            {
                emoji = Emoji.RobotFace;
            }

            var slackMessage = new SlackMessage
            {
                Channel = channel,
                Text = message,
                IconEmoji = emoji,
                Username = userName
            };

            return await slackClient.PostAsync(slackMessage);
        }
        
    }
}
