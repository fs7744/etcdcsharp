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
        /// Options for calls made by client that auth for etcd v3 Client 
        /// </summary>
        public CallOptions AuthToken { get; private set; }

        /// <summary>
        /// new Client
        /// </summary>
        /// <param name="target">host:port</param>
        /// <param name="credentials">Client-side channel credentials. Used for creation of a secure channel. Defualt is ChannelCredentials.Insecure</param>
        public Client(string target, ChannelCredentials credentials = null)
        {
            _Channel = new Channel(target, credentials ?? ChannelCredentials.Insecure);
            InitClient();
        }

        /// <summary>
        /// Generate new auth token
        /// </summary>
        /// <param name="user">User which for auth</param>
        /// <param name="password">User password</param>
        /// <param name="headers">Headers to be sent with the call.</param>
        /// <param name="deadline">Deadline for the call to finish. null means no deadline.</param>
        /// <param name="cancellationToken">Can be used to request cancellation of the call.</param>
        /// <param name="writeOptions">Write options that will be used for this call.</param>
        /// <param name="propagationToken">Context propagation token obtained from Grpc.Core.ServerCallContext.</param>
        /// <param name="credentials">Credentials to use for this call.</param>
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