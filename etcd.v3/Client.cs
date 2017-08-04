using Google.Protobuf;
using Grpc.Core;
using System;
using System.Threading;
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
        public CallOptions AuthToken { get; private set; }

        public Client(string target)
        {
            _Channel = new Channel(target, ChannelCredentials.Insecure);
            InitClient();
        }

        public void NewAuthToken(string user, string password, Metadata headers = null,
            DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken),
            WriteOptions writeOptions = null, ContextPropagationToken propagationToken = null,
            CallCredentials credentials = null)
        {
            ProtoPreconditions.CheckNotNull(user, nameof(user));
            ProtoPreconditions.CheckNotNull(password, nameof(password));
            var res = this.Authebtucate(user, password);
            var metadata = headers ?? new Metadata();
            metadata.Add(new Metadata.Entry(Constants.Token, res.Token));
            AuthToken = new CallOptions(metadata, deadline, cancellationToken, writeOptions, propagationToken, credentials);
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