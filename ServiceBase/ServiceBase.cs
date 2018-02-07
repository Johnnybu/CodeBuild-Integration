using Amazon;
using Amazon.Runtime;
using System;
using System.Collections;

namespace BuildEmailNotification
{
    public class ServiceBase
    {
        public IAmazonService Client { get; set; }
        public IDictionary EnvironmentVariables { get; set; }

        public ServiceBase()
        {
            EnvironmentVariables = Environment.GetEnvironmentVariables();
        }

        public Exception HandleServiceException(AmazonServiceException exception, string serviceMethod)
        {
            return new Exception(String.Format("An error occured while calling the service {0}", serviceMethod), exception);
        }

        public RegionEndpoint GetRegionEndpoint(string endpointParameter)
        {
            return RegionEndpoint.GetBySystemName(GetEnvironmentVariableValue(endpointParameter));
        }

        public string GetEnvironmentVariableValue(string variableName)
        {
            if (EnvironmentVariables.Contains(variableName))
            {
                return EnvironmentVariables[variableName].ToString();
            }
            else
            {
                throw new NullReferenceException(String.Format("Unable to retrieve a value for a system variable with the key: {0}", variableName));
            }
        }
    }
}
