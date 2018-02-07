using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using System.Collections.Generic;
using System.Linq;

namespace BuildEmailNotification
{
    public class SimpleSystemsManagementService : ServiceBase
    {
        public SimpleSystemsManagementService()
        {
            Client = new AmazonSimpleSystemsManagementClient(GetRegionEndpoint("SSM_REGION"));
        }

        public List<string> GetParameterValues(string parameterName)
        {
            var request = new GetParameterRequest()
            {
                Name = parameterName
            };

            var response = (Client as AmazonSimpleSystemsManagementClient).GetParameterAsync(request);

            return response.Result.Parameter.Value.Split(',').ToList();
        }
    }
}
