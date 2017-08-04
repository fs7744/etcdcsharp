using Etcdserverpb;
using Grpc.Core;
using static Etcdserverpb.AlarmRequest.Types;

namespace ETCD.V3
{
    public static class MaintenanceExtensions
    {
        #region Alarm

        public static AlarmRequest CreateAlarmRequest(this Client client, ulong memberID,
            AlarmAction action = AlarmAction.Get, AlarmType alarm = AlarmType.None)
        {
            return new AlarmRequest()
            {
                MemberID = memberID,
                Alarm = alarm,
                Action = action
            };
        }

        public static AlarmResponse Alarm(this Client client, ulong memberID,
            AlarmAction action = AlarmAction.Get, AlarmType alarm = AlarmType.None)
        {
            var request = client.CreateAlarmRequest(memberID, action, alarm);
            return client.Maintenance.Alarm(request, client.CallToken);
        }

        public static AsyncUnaryCall<AlarmResponse> AlarmAsync(this Client client, ulong memberID,
            AlarmAction action = AlarmAction.Get, AlarmType alarm = AlarmType.None)
        {
            var request = client.CreateAlarmRequest(memberID, action, alarm);
            return client.Maintenance.AlarmAsync(request, client.CallToken);
        }

        #endregion Alarm

        #region Status

        public static StatusResponse Status(this Client client)
        {
            return client.Maintenance.Status(new StatusRequest(), client.CallToken);
        }

        public static AsyncUnaryCall<StatusResponse> StatusAsync(this Client client)
        {
            return client.Maintenance.StatusAsync(new StatusRequest());
        }

        #endregion Status

        #region Defragment

        public static DefragmentResponse Defragment(this Client client)
        {
            return client.Maintenance.Defragment(new DefragmentRequest(), client.CallToken);
        }

        public static AsyncUnaryCall<DefragmentResponse> DefragmentAsync(this Client client)
        {
            return client.Maintenance.DefragmentAsync(new DefragmentRequest(), client.CallToken);
        }

        #endregion Defragment

        #region Hash

        public static HashResponse Hash(this Client client)
        {
            return client.Maintenance.Hash(new HashRequest(), client.CallToken);
        }

        public static AsyncUnaryCall<HashResponse> HashAsync(this Client client)
        {
            return client.Maintenance.HashAsync(new HashRequest(), client.CallToken);
        }

        #endregion Hash

        #region Snapshot

        public static AsyncServerStreamingCall<SnapshotResponse> Snapshot(this Client client)
        {
            return client.Maintenance.Snapshot(new SnapshotRequest(), client.CallToken);
        }

        #endregion Snapshot
    }
}