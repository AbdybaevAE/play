using System.Diagnostics;

namespace TracingExample.Config
{
    public static class DiagnosticsConfig
    {
        public const string ServiceName = "MyService";
        public const string BrokerServiceName = "BrokerService";
        public static ActivitySource ActivitySource = new(ServiceName);
        public static ActivitySource BrokerServiceActivitySource = new(BrokerServiceName);
    }
}