using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using System.Text;
using System.Threading.Tasks;

namespace AmazonService
{
    public class CloudWatchLogsService : AmazonServiceBase<AmazonCloudWatchLogsClient>
    {
        public string LogGroupName { get; set; }

        public string LogStreamName { get; set; }

        public int LogRowLimit { get; set; }

        public CloudWatchLogsService()
        {
            Client = new AmazonCloudWatchLogsClient(GetRegionEndpoint("CLOUDWATCH_REGION"));
        }

        public async Task<string> GetCloudWatchLogEventsAsync()
        {
            var events = new StringBuilder();

            var request = new GetLogEventsRequest()
            {
                LogGroupName = LogGroupName,
                LogStreamName = LogStreamName,
                Limit = LogRowLimit
            };

            var response = await Client.GetLogEventsAsync(request);

            foreach (OutputLogEvent logEvent in response.Events)
            {
                events.AppendLine(logEvent.Timestamp + ": " + logEvent.Message);
            }

            return events.ToString();
        }
    }
}
