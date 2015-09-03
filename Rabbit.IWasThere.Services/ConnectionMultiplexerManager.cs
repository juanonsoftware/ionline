using StackExchange.Redis;
using System.Net;

namespace Rabbit.IWasThere.Services
{
    public class ConnectionMultiplexerManager
    {
        private static ConnectionMultiplexer _connection;
        private static readonly object LockObj = new object();

        public static ConnectionMultiplexer GetCurrent(string endPoint, string password)
        {
            lock (LockObj)
            {
                if (_connection == null)
                {
                    InitializeConnection(endPoint, password);
                }
            }

            return _connection;
        }

        private static void InitializeConnection(string endPoint, string password)
        {
            if (_connection != null)
            {
                return;
            }

            var datas = endPoint.Split(':');

            var host = datas[0];
            var port = int.Parse(datas[1]);

            var cfg = new ConfigurationOptions()
            {
                Ssl = false,
                Password = password,
            };
            cfg.EndPoints.Add(new DnsEndPoint(host, port));

            _connection = ConnectionMultiplexer.Connect(cfg);
        }

    }
}