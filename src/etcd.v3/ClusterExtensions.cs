using Etcdserverpb;
using Grpc.Core;

namespace ETCD.V3
{
    public static class ClusterExtensions
    {
        #region MemberAdd

        /// <summary>
        /// Create MemberAddRequest.
        /// </summary>
        /// <param name="peerURLs">peerURLs is the list of URLs the added member will use to communicate with the cluster.</param>
        /// <returns>MemberAddRequest.</returns>
        public static MemberAddRequest CreateMemberAddRequest(this Client client, params string[] peerURLs)
        {
            var request = new MemberAddRequest();
            request.PeerURLs.AddRange(peerURLs);
            return request;
        }

        /// <summary>
        /// MemberAdd adds a member into the cluster.
        /// </summary>
        /// <param name="peerURLs">peerURLs is the list of URLs the added member will use to communicate with the cluster.</param>
        /// <returns>The response received from the server.</returns>
        public static MemberAddResponse MemberAdd(this Client client, params string[] peerURLs)
        {
            var request = client.CreateMemberAddRequest(peerURLs);
            return client.Cluster.MemberAdd(request, client.AuthToken);
        }

        /// <summary>
        /// MemberAdd adds a member into the cluster.
        /// </summary>
        /// <param name="peerURLs">peerURLs is the list of URLs the added member will use to communicate with the cluster.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<MemberAddResponse> MemberAddAsync(this Client client, params string[] peerURLs)
        {
            var request = client.CreateMemberAddRequest(peerURLs);
            return client.Cluster.MemberAddAsync(request, client.AuthToken);
        }

        #endregion MemberAdd

        #region MemberRemove

        /// <summary>
        /// Create MemberRemoveRequest.
        /// </summary>
        /// <param name="id">ID is the member ID of the member to remove.</param>
        /// <returns>MemberRemoveRequest.</returns>
        public static MemberRemoveRequest CreateMemberRemoveRequest(this Client client, ulong id)
        {
            return new MemberRemoveRequest() { ID = id };
        }

        /// <summary>
        /// MemberRemove removes an existing member from the cluster.
        /// </summary>
        /// <param name="id">ID is the member ID of the member to remove.</param>
        /// <returns>The response received from the server.</returns>
        public static MemberRemoveResponse MemberRemove(this Client client, ulong id)
        {
            var request = client.CreateMemberRemoveRequest(id);
            return client.Cluster.MemberRemove(request, client.AuthToken);
        }

        /// <summary>
        /// MemberRemove removes an existing member from the cluster.
        /// </summary>
        /// <param name="id">ID is the member ID of the member to remove.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<MemberRemoveResponse> MemberRemoveAsync(this Client client, ulong id)
        {
            var request = client.CreateMemberRemoveRequest(id);
            return client.Cluster.MemberRemoveAsync(request, client.AuthToken);
        }

        #endregion MemberRemove

        #region MemberRemove

        /// <summary>
        /// Create MemberUpdateRequest.
        /// </summary>
        /// <param name="id">ID is the member ID of the member to update.</param>
        /// <param name="peerURLs">peerURLs is the new list of URLs the member will use to communicate with the cluster.</param>
        /// <returns>MemberUpdateRequest.</returns>
        public static MemberUpdateRequest CreateMemberUpdateRequest(this Client client, ulong id, params string[] peerURLs)
        {
            var request = new MemberUpdateRequest() { ID = id };
            request.PeerURLs.AddRange(peerURLs);
            return request;
        }

        /// <summary>
        /// MemberUpdate updates the member configuration.
        /// </summary>
        /// <param name="id">ID is the member ID of the member to update.</param>
        /// <param name="peerURLs">peerURLs is the new list of URLs the member will use to communicate with the cluster.</param>
        /// <returns>The response received from the server.</returns>
        public static MemberUpdateResponse MemberUpdate(this Client client, ulong id, params string[] peerURLs)
        {
            var request = client.CreateMemberUpdateRequest(id, peerURLs);
            return client.Cluster.MemberUpdate(request, client.AuthToken);
        }

        /// <summary>
        /// MemberUpdate updates the member configuration.
        /// </summary>
        /// <param name="id">ID is the member ID of the member to update.</param>
        /// <param name="peerURLs">peerURLs is the new list of URLs the member will use to communicate with the cluster.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<MemberUpdateResponse> MemberUpdateAsync(this Client client, ulong id, params string[] peerURLs)
        {
            var request = client.CreateMemberUpdateRequest(id, peerURLs);
            return client.Cluster.MemberUpdateAsync(request, client.AuthToken);
        }

        #endregion MemberRemove

        #region MemberList

        /// <summary>
        /// MemberList lists all the members in the cluster.
        /// </summary>
        /// <returns>The response received from the server.</returns>
        public static MemberListResponse MemberList(this Client client)
        {
            return client.Cluster.MemberList(new MemberListRequest(), client.AuthToken);
        }

        /// <summary>
        /// MemberList lists all the members in the cluster.
        /// </summary>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<MemberListResponse> MemberListAsync(this Client client)
        {
            return client.Cluster.MemberListAsync(new MemberListRequest(), client.AuthToken);
        }

        #endregion MemberList
    }
}