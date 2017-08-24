using Etcdserverpb;
using Grpc.Core;

namespace ETCD.V3
{
    public static class LeaseExtensions
    {
        #region LeaseGrant

        /// <summary>
        /// Create LeaseGrantRequest.
        /// </summary>
        /// <param name="ttl">TTL is the advisory time-to-live in seconds.</param>
        /// <param name="id">ID is the requested ID for the lease. If ID is set to 0, the lessor chooses an ID.</param>
        /// <returns>LeaseGrantRequest.</returns>
        public static LeaseGrantRequest CreateLeaseGrantRequest(this Client client, long ttl, long id)
        {
            return new LeaseGrantRequest()
            {
                TTL = ttl,
                ID = id
            };
        }

        /// <summary>
        /// LeaseGrant creates a lease which expires if the server does not receive a keepAlive
        /// within a given time to live period. All keys attached to the lease will be expired and
        /// deleted if the lease expires. Each expired key generates a delete event in the event history.
        /// </summary>
        /// <param name="ttl">TTL is the advisory time-to-live in seconds.</param>
        /// <param name="id">ID is the requested ID for the lease. If ID is set to 0, the lessor chooses an ID.</param>
        /// <returns>The response received from the server.</returns>
        public static LeaseGrantResponse LeaseGrant(this Client client, long ttl, long id)
        {
            var request = client.CreateLeaseGrantRequest(ttl, id);
            return client.Lease.LeaseGrant(request);
        }

        /// <summary>
        /// LeaseGrant creates a lease which expires if the server does not receive a keepAlive
        /// within a given time to live period. All keys attached to the lease will be expired and
        /// deleted if the lease expires. Each expired key generates a delete event in the event history.
        /// </summary>
        /// <param name="ttl">TTL is the advisory time-to-live in seconds.</param>
        /// <param name="id">ID is the requested ID for the lease. If ID is set to 0, the lessor chooses an ID.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<LeaseGrantResponse> LeaseGrantAsync(this Client client, long ttl, long id)
        {
            var request = client.CreateLeaseGrantRequest(ttl, id);
            return client.Lease.LeaseGrantAsync(request);
        }

        #endregion LeaseGrant

        #region LeaseRevoke

        /// <summary>
        /// Create LeaseRevokeRequest.
        /// </summary>
        /// <param name="id">ID is the lease ID to revoke. When the ID is revoked, all associated keys will be deleted.</param>
        /// <returns>The response received from the server.</returns>
        public static LeaseRevokeRequest CreateLeaseRevokeRequest(this Client client, long id)
        {
            return new LeaseRevokeRequest()
            {
                ID = id
            };
        }

        /// <summary>
        /// LeaseRevoke revokes a lease. All keys attached to the lease will expire and be deleted.
        /// </summary>
        /// <param name="id">ID is the lease ID to revoke. When the ID is revoked, all associated keys will be deleted.</param>
        /// <returns>The response received from the server.</returns>
        public static LeaseRevokeResponse LeaseRevoke(this Client client, long id)
        {
            var request = client.CreateLeaseRevokeRequest(id);
            return client.Lease.LeaseRevoke(request);
        }

        /// <summary>
        /// LeaseRevoke revokes a lease. All keys attached to the lease will expire and be deleted.
        /// </summary>
        /// <param name="id">ID is the lease ID to revoke. When the ID is revoked, all associated keys will be deleted.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<LeaseRevokeResponse> LeaseRevokeAsync(this Client client, long id)
        {
            var request = client.CreateLeaseRevokeRequest(id);
            return client.Lease.LeaseRevokeAsync(request);
        }

        #endregion LeaseRevoke

        /// <summary>
        /// LeaseKeepAlive keeps the lease alive by streaming keep alive requests from the client
        /// to the server and streaming keep alive responses from the server to the client.
        /// </summary>
        /// <returns>The call object.</returns>
        public static AsyncDuplexStreamingCall<LeaseKeepAliveRequest, LeaseKeepAliveResponse> LeaseKeepAlive(this Client client)
        {
            return client.Lease.LeaseKeepAlive();
        }

        #region LeaseTimeToLive

        /// <summary>
        /// Create LeaseTimeToLiveRequest.
        /// </summary>
        /// <param name="id">ID is the lease ID for the lease.</param>
        /// <param name="keys">keys is true to query all the keys attached to this lease.</param>
        /// <returns>LeaseTimeToLiveRequest.</returns>
        public static LeaseTimeToLiveRequest CreateLeaseTimeToLiveRequest(this Client client, long id, bool keys = false)
        {
            return new LeaseTimeToLiveRequest()
            {
                ID = id,
                Keys = keys
            };
        }

        /// <summary>
        /// LeaseTimeToLive retrieves lease information.
        /// </summary>
        /// <param name="id">ID is the lease ID for the lease.</param>
        /// <param name="keys">keys is true to query all the keys attached to this lease.</param>
        /// <returns>The response received from the server.</returns>
        public static LeaseTimeToLiveResponse LeaseTimeToLive(this Client client, long id, bool keys = false)
        {
            var request = client.CreateLeaseTimeToLiveRequest(id, keys);
            return client.Lease.LeaseTimeToLive(request);
        }

        /// <summary>
        /// LeaseTimeToLive retrieves lease information.
        /// </summary>
        /// <param name="id">ID is the lease ID for the lease.</param>
        /// <param name="keys">keys is true to query all the keys attached to this lease.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<LeaseTimeToLiveResponse> LeaseTimeToLiveAsync(this Client client, long id, bool keys = false)
        {
            var request = client.CreateLeaseTimeToLiveRequest(id, keys);
            return client.Lease.LeaseTimeToLiveAsync(request);
        }

        #endregion LeaseTimeToLive
    }
}