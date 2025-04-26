using Yarp.ReverseProxy.Configuration;

namespace Yarp.Microservice.Gateway.Configurations
{
    public  static class ReverseProxyConfig
    {
        public static IReadOnlyList<RouteConfig> GetRoutes()
        {
            return new[]
            {
                new RouteConfig
                { 
                    RouteId = "customers-route",
                    ClusterId = "customers-cluster",
                    Match = new RouteMatch
                    {
                        Path ="/api/customers/{**catch-all}"
                    }
                },
                new RouteConfig
                {
                    RouteId = "orders-route",
                    ClusterId = "orders-cluster",
                    Match = new RouteMatch
                    {
                        Path ="/api/orders/{**catch-all}"
                    }
                },
                 new RouteConfig
                {
                    RouteId = "orchestrators-route",
                    ClusterId = "orchestrators-cluster",
                    Match = new RouteMatch
                    {
                        Path ="/api/orchestrators/{**catch-all}"
                    }
                }
            };
        }

        public static IReadOnlyList<ClusterConfig> GetClusters()
        {
            return new[]
            {
                new ClusterConfig
                {
                    ClusterId ="customers-cluster",
                    Destinations = new Dictionary<string, DestinationConfig>
                    {
                        {"customers-destination", new DestinationConfig{Address = "https://localhost:7164"} }
                    }
                },
                 new ClusterConfig
                {
                    ClusterId ="orders-cluster",
                    Destinations = new Dictionary<string, DestinationConfig>
                    {
                        {"orders-destination", new DestinationConfig{Address = "https://localhost:7035"} }
                    }
                },
                new ClusterConfig
                {
                    ClusterId ="orchestrators-cluster",
                    Destinations = new Dictionary<string, DestinationConfig>
                    {
                        {"orchestrators-destination", new DestinationConfig{Address = "https://localhost:7236"} }
                    }
                },
            };
        }
    }
}
