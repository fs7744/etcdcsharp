using Etcdserverpb;
using Grpc.Core;
using static Etcdserverpb.AlarmRequest.Types;

namespace ETCD.V3
{
    public static class MaintenanceExtensions
    {
        #region Alarm

        /// <summary>
        /// Create AlarmRequest.
        /// </summary>
        /// <param name="memberID">memberID is the ID of the member associated with the alarm. If memberID is 0, the
        /// alarm request covers all members.</param>
        /// <param name="action">action is the kind of alarm request to issue. The action
        /// may GET alarm statuses, ACTIVATE an alarm, or DEACTIVATE a
        /// raised alarm.</param>
        /// <param name="alarm">alarm is the type of alarm to consider for this request.</param>
        /// <returns>AlarmRequest.</returns>
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

        /// <summary>
        /// Alarm activates, deactivates, and queries alarms regarding cluster health.
        /// </summary>
        /// <param name="memberID">memberID is the ID of the member associated with the alarm. If memberID is 0, the
        /// alarm request covers all members.</param>
        /// <param name="action">action is the kind of alarm request to issue. The action
        /// may GET alarm statuses, ACTIVATE an alarm, or DEACTIVATE a
        /// raised alarm.</param>
        /// <param name="alarm">alarm is the type of alarm to consider for this request.</param>
        /// <returns>The response received from the server.</returns>
        public static AlarmResponse Alarm(this Client client, ulong memberID,
            AlarmAction action = AlarmAction.Get, AlarmType alarm = AlarmType.None)
        {
            var request = client.CreateAlarmRequest(memberID, action, alarm);
            return client.Maintenance.Alarm(request);
        }

        /// <summary>
        /// Alarm activates, deactivates, and queries alarms regarding cluster health.
        /// </summary>
        /// <param name="memberID">memberID is the ID of the member associated with the alarm. If memberID is 0, the
        /// alarm request covers all members.</param>
        /// <param name="action">action is the kind of alarm request to issue. The action
        /// may GET alarm statuses, ACTIVATE an alarm, or DEACTIVATE a
        /// raised alarm.</param>
        /// <param name="alarm">alarm is the type of alarm to consider for this request.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AlarmResponse> AlarmAsync(this Client client, ulong memberID,
            AlarmAction action = AlarmAction.Get, AlarmType alarm = AlarmType.None)
        {
            var request = client.CreateAlarmRequest(memberID, action, alarm);
            return client.Maintenance.AlarmAsync(request);
        }

        #endregion Alarm

        #region Status

        /// <summary>
        /// Status gets the status of the member.
        /// </summary>
        /// <returns>The response received from the server.</returns>
        public static StatusResponse Status(this Client client)
        {
            return client.Maintenance.Status(new StatusRequest());
        }

        /// <summary>
        /// Status gets the status of the member.
        /// </summary>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<StatusResponse> StatusAsync(this Client client)
        {
            return client.Maintenance.StatusAsync(new StatusRequest());
        }

        #endregion Status

        #region Defragment

        /// <summary>
        /// Defragment defragments a member's backend database to recover storage space.
        /// </summary>
        /// <returns>The response received from the server.</returns>
        public static DefragmentResponse Defragment(this Client client)
        {
            return client.Maintenance.Defragment(new DefragmentRequest());
        }

        /// <summary>
        /// Defragment defragments a member's backend database to recover storage space.
        /// </summary>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<DefragmentResponse> DefragmentAsync(this Client client)
        {
            return client.Maintenance.DefragmentAsync(new DefragmentRequest());
        }

        #endregion Defragment

        #region Hash

        /// <summary>
        /// Hash returns the hash of the local KV state for consistency checking purpose.
        /// This is designed for testing; do not use this in production when there
        /// are ongoing transactions.
        /// </summary>
        /// <returns>The response received from the server.</returns>
        public static HashResponse Hash(this Client client)
        {
            return client.Maintenance.Hash(new HashRequest());
        }

        /// <summary>
        /// Hash returns the hash of the local KV state for consistency checking purpose.
        /// This is designed for testing; do not use this in production when there
        /// are ongoing transactions.
        /// </summary>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<HashResponse> HashAsync(this Client client)
        {
            return client.Maintenance.HashAsync(new HashRequest());
        }

        #endregion Hash

        #region Snapshot

        /// <summary>
        /// Snapshot sends a snapshot of the entire backend from a member over a stream to a client.
        /// </summary>
        /// <returns>The call object.</returns>
        public static AsyncServerStreamingCall<SnapshotResponse> Snapshot(this Client client)
        {
            return client.Maintenance.Snapshot(new SnapshotRequest());
        }

        #endregion Snapshot
    }
}