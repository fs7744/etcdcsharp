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
    /// <summary>
    /// A simple wrapper client for etcd v3 client.
    /// </summary>
    public class Client
    {
        private Channel _Channel;
        private AuthCallInvoker authInvoker;

        /// <summary>
        /// Origin grpc etcd v3 Client for Auth
        /// </summary>
        public AuthClient Auth { get; private set; }

        /// <summary>
        /// Origin grpc etcd v3 Client for KV
        /// </summary>
        public KVClient KV { get; private set; }

        /// <summary>
        /// Origin grpc etcd v3 Client for Cluster
        /// </summary>
        public ClusterClient Cluster { get; private set; }

        /// <summary>
        /// Origin grpc etcd v3 Client for Maintenance
        /// </summary>
        public MaintenanceClient Maintenance { get; private set; }

        /// <summary>
        /// Origin grpc etcd v3 Client for Lease
        /// </summary>
        public LeaseClient Lease { get; private set; }

        /// <summary>
        /// Origin grpc etcd v3 Client for Watch
        /// </summary>
        public WatchClient Watch { get; private set; }

        /// <summary>
        /// new Client
        /// </summary>
        /// <param name="target">host:port</param>
        /// <param name="credentials">Client-side channel credentials. Used for creation of a secure channel. Defualt is ChannelCredentials.Insecure</param>
        public Client(string target, ChannelCredentials credentials = null)
        {
            _Channel = new Channel(target, credentials ?? ChannelCredentials.Insecure);
            authInvoker = new AuthCallInvoker(_Channel);
            InitClient();
        }

        /// <summary>
        /// Generate new auth token
        /// </summary>
        /// <param name="user">User which for auth</param>
        /// <param name="password">User password</param>
        /// <param name="headers">Headers to be sent with the call.</param>
        public void NewAuthToken(string user, string password, Metadata headers = null)
        {
            ProtoPreconditions.CheckNotNull(user, nameof(user));
            ProtoPreconditions.CheckNotNull(password, nameof(password));
            var res = this.Authebtucate(user, password);
            var metadata = headers ?? new Metadata();
            authInvoker.Token = new Metadata.Entry(Constants.Token, res.Token);
        }

        private void InitClient()
        {
            Auth = new AuthClient(authInvoker);
            KV = new KVClient(authInvoker);
            Cluster = new ClusterClient(authInvoker);
            Maintenance = new MaintenanceClient(authInvoker);
            Lease = new LeaseClient(authInvoker);
            Watch = new WatchClient(authInvoker);
        }

        /// <summary>
        /// Async close channel.
        /// </summary>
        public Task CloseAsync()
        {
            return _Channel.ShutdownAsync();
        }

        /// <summary>
        /// Close channel.
        /// </summary>
        public void Close()
        {
            CloseAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}