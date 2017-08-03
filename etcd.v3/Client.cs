using Etcdserverpb;
using Grpc.Core;
using System.Threading.Tasks;
using static Etcdserverpb.Auth;
using static Etcdserverpb.Cluster;
using static Etcdserverpb.KV;
using static Etcdserverpb.Lease;
using static Etcdserverpb.Maintenance;
using static Etcdserverpb.Watch;

namespace ETCD.V3
{
    public class Client
    {
        private Channel _Channel;
        public AuthClient Auth { get; private set; }
        public KVClient KV { get; private set; }
        public ClusterClient Cluster { get; private set; }
        public MaintenanceClient Maintenance { get; private set; }
        public LeaseClient Lease { get; private set; }
        public WatchClient Watch { get; private set; }
        public CallOptions CallToken { get; private set; }

        public Client(string target, string user = null, string password = null)
        {
            _Channel = new Channel(target, ChannelCredentials.Insecure);
            Authebtucate(user, password);
            InitClient();
        }

        public void Authebtucate(string user, string password)
        {
            if (!string.IsNullOrWhiteSpace(user))
            {
                Auth = new AuthClient(_Channel);
                var res = Auth.Authenticate(new AuthenticateRequest()
                {
                    Name = user,
                    Password = password
                });
                var metadata = new Metadata
                {
                    new Metadata.Entry(Constants.Token, res.Token)
                };
                CallToken = new CallOptions(metadata);
            }
        }

        private void InitClient()
        {
            Auth = new AuthClient(_Channel);
            KV = new KVClient(_Channel);
            Cluster = new ClusterClient(_Channel);
            Maintenance = new MaintenanceClient(_Channel);
            Lease = new LeaseClient(_Channel);
            Watch = new WatchClient(_Channel);
        }

        public Task CloseAsync()
        {
            return _Channel.ShutdownAsync();
        }

        public void Close()
        {
            CloseAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}