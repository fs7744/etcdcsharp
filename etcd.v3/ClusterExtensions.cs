using Etcdserverpb;
using Grpc.Core;

namespace ETCD.V3
{
    public static class ClusterExtensions
    {
        #region MemberAdd

        public static MemberAddRequest CreateMemberAddRequest(this Client client, params string[] peerURLs)
        {
            var request = new MemberAddRequest();
            request.PeerURLs.AddRange(peerURLs);
            return request;
        }

        public static MemberAddResponse MemberAdd(this Client client, params string[] peerURLs)
        {
            var request = client.CreateMemberAddRequest(peerURLs);
            return client.Cluster.MemberAdd(request, client.CallToken);
        }

        public static AsyncUnaryCall<MemberAddResponse> MemberAddAsync(this Client client, params string[] peerURLs)
        {
            var request = client.CreateMemberAddRequest(peerURLs);
            return client.Cluster.MemberAddAsync(request, client.CallToken);
        }

        #endregion MemberAdd

        #region MemberRemove

        public static MemberRemoveRequest CreateMemberRemoveRequest(this Client client, ulong id)
        {
            return new MemberRemoveRequest() { ID = id };
        }

        public static MemberRemoveResponse MemberRemove(this Client client, ulong id)
        {
            var request = client.CreateMemberRemoveRequest(id);
            return client.Cluster.MemberRemove(request, client.CallToken);
        }

        public static AsyncUnaryCall<MemberRemoveResponse> MemberRemoveAsync(this Client client, ulong id)
        {
            var request = client.CreateMemberRemoveRequest(id);
            return client.Cluster.MemberRemoveAsync(request, client.CallToken);
        }

        #endregion MemberRemove

        #region MemberRemove

        public static MemberUpdateRequest CreateMemberUpdateRequest(this Client client, ulong id, params string[] peerURLs)
        {
            var request = new MemberUpdateRequest() { ID = id };
            request.PeerURLs.AddRange(peerURLs);
            return request;
        }

        public static MemberUpdateResponse MemberUpdate(this Client client, ulong id, params string[] peerURLs)
        {
            var request = client.CreateMemberUpdateRequest(id, peerURLs);
            return client.Cluster.MemberUpdate(request, client.CallToken);
        }

        public static AsyncUnaryCall<MemberUpdateResponse> MemberUpdateAsync(this Client client, ulong id, params string[] peerURLs)
        {
            var request = client.CreateMemberUpdateRequest(id, peerURLs);
            return client.Cluster.MemberUpdateAsync(request, client.CallToken);
        }

        #endregion MemberRemove

        #region MemberList

        public static MemberListResponse MemberList(this Client client)
        {
            return client.Cluster.MemberList(new MemberListRequest(), client.CallToken);
        }

        public static AsyncUnaryCall<MemberListResponse> MemberListAsync(this Client client)
        {
            return client.Cluster.MemberListAsync(new MemberListRequest(), client.CallToken);
        }

        #endregion MemberList
    }
}