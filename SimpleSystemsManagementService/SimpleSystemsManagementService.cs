using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonService
{
    public class SimpleSystemsManagementService : AmazonServiceBase<AmazonSimpleSystemsManagementClient>
    {
        public SimpleSystemsManagementService()
        {
            Client = new AmazonSimpleSystemsManagementClient(GetRegionEndpoint("SSM_REGION"));
        }

        public async Task<List<string>> GetParameterValuesAsync(string parameterName)
        {
            var response = await GetParameterResultAsync(parameterName);

            return response.Parameter.Value.Split(',').ToList();
        }

        public async Task<string> GetParameterValueAsync(string parameterName)
        {
            var response = await GetParameterResultAsync(parameterName);

            return response.Parameter.Value;
        }

        private async Task<GetParameterResponse> GetParameterResultAsync(string parameterName)
        {
            var request = new GetParameterRequest()
            {
                Name = parameterName,
                WithDecryption = true
            };

            return await Client.GetParameterAsync(request);
        }
    }
}
