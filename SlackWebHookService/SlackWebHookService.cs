using AmazonService;
using Slack.Webhooks;
using System.Threading.Tasks;

namespace CodeBuildIntegration
{
    public class SlackWebHookService
    {
        public async Task<bool> SendSlackMessageAsync()
        {
            var slackClient = new SlackClient(await new SimpleSystemsManagementService()
                .GetParameterValueAsync("SlackWebhookUri"));

            var slackMessage = new SlackMessage
            {
                Channel = "#general",
                Text = "Test Message",
                IconEmoji = Emoji.BlueBook,
                Username = "CodeBuild"
            };

            return await slackClient.PostAsync(slackMessage);
        }
        
    }
}
