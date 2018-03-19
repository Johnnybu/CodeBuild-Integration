using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmazonService
{
    public class SimpleEmailService : ServiceBase
    {
        public Message Message { get; }
        public List<string> ToAddresses { get; set; }
        public string Source { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public SimpleSystemsManagementService SsmService { get; set; }

        public SimpleEmailService()
        {
            Client = new AmazonSimpleEmailServiceClient(GetRegionEndpoint("SES_REGION"));
            SsmService = new SimpleSystemsManagementService();
        }

        public async Task<SendEmailResponse> SendEmailAsync()
        {
            var request = new SendEmailRequest()
            {
                Destination = await ConstructDestinationAsync(),
                Source = await SsmService.GetParameterValueAsync(GetEnvironmentVariableValue("SOURCE_EMAIL")),
                Message = ConstructMessage()
            };

            try
            {
                return await (Client as AmazonSimpleEmailServiceClient).SendEmailAsync(request);
            }
            catch (AmazonSimpleEmailServiceException e)
            {
                throw HandleServiceException(e, "SendEmailAsync");
            }
        }

        public Message ConstructMessage()
        {
            return new Message()
            {
                Subject = new Content()
                {
                    Charset = "UTF-8",
                    Data = Subject
                },
                Body = new Body()
                {
                    Text = new Content()
                    {
                        Charset = "UTF-8",
                        Data = Body
                    }
                }
            };
        }

        public async Task<Destination> ConstructDestinationAsync()
        {
            return new Destination()
            {
                ToAddresses = await SsmService.GetParameterValuesAsync(GetEnvironmentVariableValue("TO_EMAIL"))
            };
        }
    }
}
