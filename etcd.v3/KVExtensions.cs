using Etcdserverpb;
using Google.Protobuf;
using Grpc.Core;
using static Etcdserverpb.RangeRequest.Types;

namespace ETCD.V3
{
    public static class KVExtensions
    {
        public static readonly ByteString NoPrefixEnd = ByteString.CopyFrom(new byte[1] { 0 });

        public static ByteString ToPrefixEnd(this ByteString byteString)
        {
            byte[] endKey = byteString.ToByteArray();
            for (int i = endKey.Length - 1; i >= 0; i--)
            {
                if (endKey[i] < 0xff)
                {
                    endKey[i] = (byte)(endKey[i] + 1);
                    return ByteString.CopyFrom(endKey);
                }
            }

            return NoPrefixEnd;
        }

        #region Put

        public static PutResponse Put(this Client client, string key, string value, long lease = 0,
            bool prevKv = false, bool ignoreLease = false, bool ignoreValue = false)
        {
            return client.Put(ByteString.CopyFromUtf8(key), ByteString.CopyFromUtf8(value),
                lease, prevKv, ignoreLease, ignoreValue);
        }

        public static PutResponse Put(this Client client, ByteString key, ByteString value, long lease = 0,
            bool prevKv = false, bool ignoreLease = false, bool ignoreValue = false)
        {
            var request = client.CreatePutRequest(key, value, lease, prevKv, ignoreLease, ignoreValue);
            return client.KV.Put(request, client.CallToken);
        }

        public static PutRequest CreatePutRequest(this Client client, ByteString key, ByteString value,
            long lease = 0, bool prevKv = false, bool ignoreLease = false, bool ignoreValue = false)
        {
            return new PutRequest()
            {
                Key = key,
                Value = value,
                Lease = lease,
                PrevKv = prevKv,
                IgnoreLease = ignoreLease,
                IgnoreValue = ignoreValue
            };
        }

        public static AsyncUnaryCall<PutResponse> PutAsync(this Client client, string key, string value,
            long lease = 0,
            bool prevKv = false, bool ignoreLease = false, bool ignoreValue = false)
        {
            return client.PutAsync(ByteString.CopyFromUtf8(key), ByteString.CopyFromUtf8(value),
                lease, prevKv, ignoreLease, ignoreValue);
        }

        public static AsyncUnaryCall<PutResponse> PutAsync(this Client client, ByteString key, ByteString value,
            long lease = 0,
            bool prevKv = false, bool ignoreLease = false, bool ignoreValue = false)
        {
            var request = client.CreatePutRequest(key, value, lease, prevKv, ignoreLease, ignoreValue);
            return client.KV.PutAsync(request, client.CallToken);
        }

        #endregion Put

        #region Range

        public static RangeRequest CreateRangeRequest(this Client client, ByteString key, ByteString rangeEnd = null,
           long limit = 0, long revision = 0, SortOrder sortOrder = SortOrder.None,
           SortTarget sortTarget = SortTarget.Key, bool serializable = false,
           bool keysOnly = false, bool countOnly = false, long minModRevision = 0, long maxModRevision = 0,
           long minCreateRevision = 0, long maxCreateRevision = 0)
        {
            return new RangeRequest()
            {
                Key = key,
                RangeEnd = rangeEnd == null ? key.ToPrefixEnd() : rangeEnd,
                Limit = limit,
                Revision = revision,
                SortOrder = sortOrder,
                SortTarget = sortTarget,
                Serializable = serializable,
                KeysOnly = keysOnly,
                CountOnly = countOnly,
                MinModRevision = minModRevision,
                MaxModRevision = maxModRevision,
                MinCreateRevision = minCreateRevision,
                MaxCreateRevision = maxCreateRevision,
            };
        }

        public static RangeResponse Range(this Client client, string key, string rangeEnd = null,
           long limit = 0, long revision = 0, SortOrder sortOrder = SortOrder.None,
           SortTarget sortTarget = SortTarget.Key, bool serializable = false,
           bool keysOnly = false, bool countOnly = false, long minModRevision = 0, long maxModRevision = 0,
           long minCreateRevision = 0, long maxCreateRevision = 0)
        {
            return client.Range(ByteString.CopyFromUtf8(key),
                rangeEnd == null ? null : ByteString.CopyFromUtf8(rangeEnd),
                limit, revision, sortOrder, sortTarget, serializable, keysOnly, countOnly, minModRevision,
                maxModRevision, minCreateRevision, maxCreateRevision);
        }

        public static RangeResponse GetAll(this Client client, string key,
           long limit = 0, long revision = 0, SortOrder sortOrder = SortOrder.None,
           SortTarget sortTarget = SortTarget.Key, bool serializable = false,
           bool keysOnly = false, bool countOnly = false, long minModRevision = 0, long maxModRevision = 0,
           long minCreateRevision = 0, long maxCreateRevision = 0)
        {
            return client.GetAll(ByteString.CopyFromUtf8(key),
                limit, revision, sortOrder, sortTarget, serializable, keysOnly, countOnly, minModRevision,
                maxModRevision, minCreateRevision, maxCreateRevision);
        }

        public static RangeResponse GetAll(this Client client, ByteString key,
           long limit = 0, long revision = 0, SortOrder sortOrder = SortOrder.None,
           SortTarget sortTarget = SortTarget.Key, bool serializable = false,
           bool keysOnly = false, bool countOnly = false, long minModRevision = 0, long maxModRevision = 0,
           long minCreateRevision = 0, long maxCreateRevision = 0)
        {
            return client.Range(key, Constants.NullKey,
                limit, revision, sortOrder, sortTarget, serializable, keysOnly, countOnly, minModRevision,
                maxModRevision, minCreateRevision, maxCreateRevision);
        }

        public static RangeResponse Range(this Client client, ByteString key, ByteString rangeEnd = null,
           long limit = 0, long revision = 0, SortOrder sortOrder = SortOrder.None,
           SortTarget sortTarget = SortTarget.Key, bool serializable = false,
           bool keysOnly = false, bool countOnly = false, long minModRevision = 0, long maxModRevision = 0,
           long minCreateRevision = 0, long maxCreateRevision = 0)
        {
            var request = client.CreateRangeRequest(key, rangeEnd,
                limit, revision, sortOrder, sortTarget, serializable, keysOnly, countOnly, minModRevision,
                maxModRevision, minCreateRevision, maxCreateRevision);
            return client.KV.Range(request, client.CallToken);
        }

        public static AsyncUnaryCall<RangeResponse> RangeAsync(this Client client, string key, string rangeEnd = null,
           long limit = 0, long revision = 0, SortOrder sortOrder = SortOrder.None,
           SortTarget sortTarget = SortTarget.Key, bool serializable = false,
           bool keysOnly = false, bool countOnly = false, long minModRevision = 0, long maxModRevision = 0,
           long minCreateRevision = 0, long maxCreateRevision = 0)
        {
            return client.RangeAsync(ByteString.CopyFromUtf8(key),
                rangeEnd == null ? null : ByteString.CopyFromUtf8(rangeEnd),
                limit, revision, sortOrder, sortTarget, serializable, keysOnly, countOnly, minModRevision,
                maxModRevision, minCreateRevision, maxCreateRevision);
        }

        public static AsyncUnaryCall<RangeResponse> RangeAsync(this Client client, ByteString key, ByteString rangeEnd = null,
           long limit = 0, long revision = 0, SortOrder sortOrder = SortOrder.None,
           SortTarget sortTarget = SortTarget.Key, bool serializable = false,
           bool keysOnly = false, bool countOnly = false, long minModRevision = 0, long maxModRevision = 0,
           long minCreateRevision = 0, long maxCreateRevision = 0)
        {
            var request = client.CreateRangeRequest(key, rangeEnd,
                limit, revision, sortOrder, sortTarget, serializable, keysOnly, countOnly, minModRevision,
                maxModRevision, minCreateRevision, maxCreateRevision);
            return client.KV.RangeAsync(request, client.CallToken);
        }

        public static AsyncUnaryCall<RangeResponse> GetAllAsync(this Client client, string key,
          long limit = 0, long revision = 0, SortOrder sortOrder = SortOrder.None,
          SortTarget sortTarget = SortTarget.Key, bool serializable = false,
          bool keysOnly = false, bool countOnly = false, long minModRevision = 0, long maxModRevision = 0,
          long minCreateRevision = 0, long maxCreateRevision = 0)
        {
            return client.GetAllAsync(ByteString.CopyFromUtf8(key),
                limit, revision, sortOrder, sortTarget, serializable, keysOnly, countOnly, minModRevision,
                maxModRevision, minCreateRevision, maxCreateRevision);
        }

        public static AsyncUnaryCall<RangeResponse> GetAllAsync(this Client client, ByteString key,
           long limit = 0, long revision = 0, SortOrder sortOrder = SortOrder.None,
           SortTarget sortTarget = SortTarget.Key, bool serializable = false,
           bool keysOnly = false, bool countOnly = false, long minModRevision = 0, long maxModRevision = 0,
           long minCreateRevision = 0, long maxCreateRevision = 0)
        {
            return client.RangeAsync(key, Constants.NullKey,
                limit, revision, sortOrder, sortTarget, serializable, keysOnly, countOnly, minModRevision,
                maxModRevision, minCreateRevision, maxCreateRevision);
        }

        #endregion Range

        #region DeleteRange

        public static DeleteRangeRequest CreateDeleteRangeRequest(this Client client, ByteString key, ByteString rangeEnd = null,
            bool prevKv = false)
        {
            return new DeleteRangeRequest()
            {
                Key = key,
                RangeEnd = rangeEnd == null ? key.ToPrefixEnd() : rangeEnd,
                PrevKv = prevKv
            };
        }

        public static DeleteRangeResponse DeleteRange(this Client client, string key, string rangeEnd = null,
           bool prevKv = false)
        {
            return client.DeleteRange(ByteString.CopyFromUtf8(key),
                rangeEnd == null ? null : ByteString.CopyFromUtf8(rangeEnd), prevKv);
        }

        public static DeleteRangeResponse DeleteRange(this Client client, ByteString key, ByteString rangeEnd = null,
           bool prevKv = false)
        {
            var request = client.CreateDeleteRangeRequest(key, rangeEnd, prevKv);
            return client.KV.DeleteRange(request, client.CallToken);
        }

        public static DeleteRangeResponse DeleteAll(this Client client, string key, bool prevKv = false)
        {
            return client.DeleteAll(ByteString.CopyFromUtf8(key), prevKv);
        }

        public static DeleteRangeResponse DeleteAll(this Client client, ByteString key, bool prevKv = false)
        {
            return client.DeleteRange(key, Constants.NullKey, prevKv);
        }

        public static AsyncUnaryCall<DeleteRangeResponse> DeleteRangeAsync(this Client client, string key, string rangeEnd = null,
           bool prevKv = false)
        {
            return client.DeleteRangeAsync(ByteString.CopyFromUtf8(key),
                rangeEnd == null ? null : ByteString.CopyFromUtf8(rangeEnd), prevKv);
        }

        public static AsyncUnaryCall<DeleteRangeResponse> DeleteRangeAsync(this Client client, ByteString key, ByteString rangeEnd = null,
           bool prevKv = false)
        {
            var request = client.CreateDeleteRangeRequest(key, rangeEnd, prevKv);
            return client.KV.DeleteRangeAsync(request, client.CallToken);
        }

        public static AsyncUnaryCall<DeleteRangeResponse> DeleteAllAsync(this Client client, string key, bool prevKv = false)
        {
            return client.DeleteAllAsync(ByteString.CopyFromUtf8(key), prevKv);
        }

        public static AsyncUnaryCall<DeleteRangeResponse> DeleteAllAsync(this Client client, ByteString key, bool prevKv = false)
        {
            return client.DeleteRangeAsync(key, Constants.NullKey, prevKv);
        }

        #endregion DeleteRange

        #region Compact

        public static CompactionRequest CreateCompactionRequest(this Client client, long revision, bool physical = false)
        {
            return new CompactionRequest()
            {
                Revision = revision,
                Physical = physical
            };
        }

        public static CompactionResponse Compact(this Client client, long revision, bool physical = false,
          bool prevKv = false)
        {
            var request = client.CreateCompactionRequest(revision, physical);
            return client.KV.Compact(request, client.CallToken);
        }

        public static AsyncUnaryCall<CompactionResponse> CompactAsync(this Client client, long revision, bool physical = false,
          bool prevKv = false)
        {
            var request = client.CreateCompactionRequest(revision, physical);
            return client.KV.CompactAsync(request, client.CallToken);
        }

        #endregion Compact

        #region Txn

        public static TxnResponse Txn(this Client client, TxnRequest request)
        {
            return client.KV.Txn(request, client.CallToken);
        }

        public static AsyncUnaryCall<TxnResponse> TxnAsync(this Client client, TxnRequest request)
        {
            return client.KV.TxnAsync(request, client.CallToken);
        }

        #endregion Txn
    }
}