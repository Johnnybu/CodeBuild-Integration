using System;
using System.Collections;

namespace AmazonService
{
    public class ServiceBase
    {
        private IDictionary EnvironmentVariables { get; set; }

        protected ServiceBase()
        {
            EnvironmentVariables = Environment.GetEnvironmentVariables();
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
