using Amazon;
using Amazon.Runtime;
using System;
using System.Collections;

namespace AmazonService
{
    public class ServiceBase
    {
        protected IAmazonService Client { get; set; }
        private IDictionary EnvironmentVariables { get; set; }

        protected ServiceBase()
        {
            EnvironmentVariables = Environment.GetEnvironmentVariables();
        }

        protected Exception HandleServiceException(AmazonServiceException exception, string serviceMethod)
        {
            return new Exception(String.Format("An error occured while calling the service {0}", serviceMethod), exception);
        }

        protected RegionEndpoint GetRegionEndpoint(string endpointParameter)
        {
            return RegionEndpoint.GetBySystemName(GetEnvironmentVariableValue(endpointParameter));
        }

        protected string GetEnvironmentVariableValue(string variableName)
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
