using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System.Collections.Generic;

namespace BuildEmailNotification
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

        public SendEmailResponse SendEmail()
        {
            var request = new SendEmailRequest()
            {
                Destination = ConstructDestination(),
                Source = SsmService.GetParameterValues(GetEnvironmentVariableValue("SOURCE_EMAIL"))[0],
                Message = ConstructMessage()
        };

            try
            {
                var response = (Client as AmazonSimpleEmailServiceClient).SendEmailAsync(request);
                return response.Result;
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

        public Destination ConstructDestination()
        {
            return new Destination()
            {
                ToAddresses = SsmService.GetParameterValues(GetEnvironmentVariableValue("TO_EMAIL"))
            };
        }
    }
}
