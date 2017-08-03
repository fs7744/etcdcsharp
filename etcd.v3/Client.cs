using Etcdserverpb;
using Grpc.Core;
using System;
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

        public Client(string target, string user = null, string password = null)
        {
            _Channel = new Channel(target, ChannelCredentials.Insecure);
            _Channel.ConnectAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            Authebtucate(target, user, password);
            InitClient();
        }

        private void Authebtucate(string target, string user, string password)
        {
            if (!string.IsNullOrWhiteSpace(user))
            {
                Auth = new AuthClient(_Channel);
                var res = Auth.Authenticate(new AuthenticateRequest()
                {
                    Name = user,
                    Password = password
                });
                var aai = CallCredentials.FromInterceptor((context, metadata) =>
                {
                    metadata.Add(new Metadata.Entry(Constants.Token, res.Token));
                    return Task.CompletedTask;
                });
                var co = new CallOptions().WithCredentials(aai);
                var cc = ChannelCredentials.Create(ChannelCredentials.Insecure, aai);
                Close();
                _Channel = new Channel(target, cc);
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