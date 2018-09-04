using Amazon;
using Amazon.Runtime;
using System;
using System.Collections;

namespace AmazonService
{
    public class AmazonServiceBase<T> : ServiceBase where T : IAmazonService
    {
        protected T Client { get; set; }

        protected Exception HandleServiceException(AmazonServiceException exception, string serviceMethod)
        {
            return new Exception(String.Format("An error occured while calling the service {0}", serviceMethod), exception);
        }

        protected RegionEndpoint GetRegionEndpoint(string endpointParameter)
        {
            return RegionEndpoint.GetBySystemName(GetEnvironmentVariableValue(endpointParameter));
        }
    }
}
