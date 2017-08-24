using Etcdserverpb;
using Google.Protobuf;
using Grpc.Core;
using static Etcdserverpb.RangeRequest.Types;

namespace ETCD.V3
{
    public static class KVExtensions
    {
        /// <summary>
        /// range_end is the key following the last key to delete for the range [key, range_end).
        /// If range_end is not given, the range is defined to contain only the key argument.
        /// If range_end is one bit larger than the given key, then the range is all the keys
        /// with the prefix (the given key).
        /// If range_end is '\0', the range is all keys greater than or equal to the key argument.
        /// </summary>
        /// <param name="byteString">ByteString to range_end.</param>
        /// <returns>range_end.</returns>
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

            return Constants.NullKey;
        }

        #region Put

        /// <summary>
        /// Put puts the given key into the key-value store.
        /// A put request increments the revision of the key-value store
        /// and generates one event in the event history.
        /// </summary>
        /// <param name="key">key is the key, in Utf8 string, will, to put into the key-value store.</param>
        /// <param name="value">value is the value, in Utf8 string, to associate with the key in the key-value store.</param>
        /// <param name="lease">Defualt value is 0. lease is the lease ID to associate with the key in the key-value store. A lease value of 0 indicates no lease.</param>
        /// <param name="prevKv">Defualt value is false. If prev_kv is set, etcd gets the previous key-value pair before changing it. The previous key-value pair will be returned in the put response.</param>
        /// <param name="ignoreLease">Defualt value is false. If ignore_lease is set, etcd updates the key using its current lease. Returns an error if the key does not exist.</param>
        /// <param name="ignoreValue">Defualt value is false. If ignore_value is set, etcd updates the key using its current value. Returns an error if the key does not exist.</param>
        /// <returns>The response received from the server.</returns>
        public static PutResponse Put(this Client client, string key, string value, long lease = 0,
            bool prevKv = false, bool ignoreLease = false, bool ignoreValue = false)
        {
            return client.Put(ByteString.CopyFromUtf8(key), ByteString.CopyFromUtf8(value),
                lease, prevKv, ignoreLease, ignoreValue);
        }

        /// <summary>
        /// Put puts the given key into the key-value store.
        /// A put request increments the revision of the key-value store
        /// and generates one event in the event history.
        /// </summary>
        /// <param name="key">key is the key, in bytes, to put into the key-value store.</param>
        /// <param name="value">value is the value, in bytes, to associate with the key in the key-value store.</param>
        /// <param name="lease">Defualt value is 0. lease is the lease ID to associate with the key in the key-value store. A lease value of 0 indicates no lease.</param>
        /// <param name="prevKv">Defualt value is false. If prev_kv is set, etcd gets the previous key-value pair before changing it. The previous key-value pair will be returned in the put response.</param>
        /// <param name="ignoreLease">Defualt value is false. If ignore_lease is set, etcd updates the key using its current lease. Returns an error if the key does not exist.</param>
        /// <param name="ignoreValue">Defualt value is false. If ignore_value is set, etcd updates the key using its current value. Returns an error if the key does not exist.</param>
        /// <returns>The response received from the server.</returns>
        public static PutResponse Put(this Client client, ByteString key, ByteString value, long lease = 0,
            bool prevKv = false, bool ignoreLease = false, bool ignoreValue = false)
        {
            var request = client.CreatePutRequest(key, value, lease, prevKv, ignoreLease, ignoreValue);
            return client.KV.Put(request);
        }

        /// <summary>
        /// Create Put Request.
        /// </summary>
        /// <param name="key">key is the key, in bytes, to put into the key-value store.</param>
        /// <param name="value">value is the value, in bytes, to associate with the key in the key-value store.</param>
        /// <param name="lease">Defualt value is 0. lease is the lease ID to associate with the key in the key-value store. A lease value of 0 indicates no lease.</param>
        /// <param name="prevKv">Defualt value is false. If prev_kv is set, etcd gets the previous key-value pair before changing it. The previous key-value pair will be returned in the put response.</param>
        /// <param name="ignoreLease">Defualt value is false. If ignore_lease is set, etcd updates the key using its current lease. Returns an error if the key does not exist.</param>
        /// <param name="ignoreValue">Defualt value is false. If ignore_value is set, etcd updates the key using its current value. Returns an error if the key does not exist.</param>
        /// <returns>PutRequest</returns>
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

        /// <summary>
        /// Put puts the given key into the key-value store.
        /// A put request increments the revision of the key-value store
        /// and generates one event in the event history.
        /// </summary>
        /// <param name="key">key is the key, in Utf8 string, will, to put into the key-value store.</param>
        /// <param name="value">value is the value, in Utf8 string, to associate with the key in the key-value store.</param>
        /// <param name="lease">Defualt value is 0. lease is the lease ID to associate with the key in the key-value store. A lease value of 0 indicates no lease.</param>
        /// <param name="prevKv">Defualt value is false. If prev_kv is set, etcd gets the previous key-value pair before changing it. The previous key-value pair will be returned in the put response.</param>
        /// <param name="ignoreLease">Defualt value is false. If ignore_lease is set, etcd updates the key using its current lease. Returns an error if the key does not exist.</param>
        /// <param name="ignoreValue">Defualt value is false. If ignore_value is set, etcd updates the key using its current value. Returns an error if the key does not exist.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<PutResponse> PutAsync(this Client client, string key, string value,
            long lease = 0,
            bool prevKv = false, bool ignoreLease = false, bool ignoreValue = false)
        {
            return client.PutAsync(ByteString.CopyFromUtf8(key), ByteString.CopyFromUtf8(value),
                lease, prevKv, ignoreLease, ignoreValue);
        }

        /// <summary>
        /// Put puts the given key into the key-value store.
        /// A put request increments the revision of the key-value store
        /// and generates one event in the event history.
        /// </summary>
        /// <param name="key">key is the key, in bytes, to put into the key-value store.</param>
        /// <param name="value">value is the value, in bytes, to associate with the key in the key-value store.</param>
        /// <param name="lease">Defualt value is 0. lease is the lease ID to associate with the key in the key-value store. A lease value of 0 indicates no lease.</param>
        /// <param name="prevKv">Defualt value is false. If prev_kv is set, etcd gets the previous key-value pair before changing it. The previous key-value pair will be returned in the put response.</param>
        /// <param name="ignoreLease">Defualt value is false. If ignore_lease is set, etcd updates the key using its current lease. Returns an error if the key does not exist.</param>
        /// <param name="ignoreValue">Defualt value is false. If ignore_value is set, etcd updates the key using its current value. Returns an error if the key does not exist.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<PutResponse> PutAsync(this Client client, ByteString key, ByteString value,
            long lease = 0,
            bool prevKv = false, bool ignoreLease = false, bool ignoreValue = false)
        {
            var request = client.CreatePutRequest(key, value, lease, prevKv, ignoreLease, ignoreValue);
            return client.KV.PutAsync(request);
        }

        #endregion Put

        #region Range

        /// <summary>
        /// Create Range Request.
        /// </summary>
        /// <param name="key">key is the first key for the range. If range_end is not given, the request only looks up key.</param>
        /// <param name="rangeEnd">range_end is the upper bound on the requested range [key, range_end).
        /// If range_end is '\0', the range is all keys >= key.
        /// If range_end is key plus one (e.g., "aa"+1 == "ab", "a\xff"+1 == "b"),
        /// then the range request gets all keys prefixed with key.
        /// If both key and range_end are '\0', then the range request returns all keys.</param>
        /// <param name="limit">Defualt value is 0. limit is a limit on the number of keys returned for the request. When limit is set to 0,
        /// it is treated as no limit.</param>
        /// <param name="revision">Defualt value is 0. revision is the point-in-time of the key-value store to use for the range.
        /// If revision is less or equal to zero, the range is over the newest key-value store.
        /// If the revision has been compacted, ErrCompacted is returned as a response.</param>
        /// <param name="sortOrder">Defualt value is SortOrder.None. sort_order is the order for returned sorted results.</param>
        /// <param name="sortTarget">Defualt value is SortTarget.Key. sort_target is the key-value field to use for sorting.</param>
        /// <param name="serializable">Defualt value is false. serializable sets the range request to use serializable member-local reads.
        /// Range requests are linearizable by default; linearizable requests have higher
        /// latency and lower throughput than serializable requests but reflect the current
        /// consensus of the cluster. For better performance, in exchange for possible stale reads,
        /// a serializable range request is served locally without needing to reach consensus
        /// with other nodes in the cluster.</param>
        /// <param name="keysOnly">Defualt value is false. keys_only when set returns only the keys and not the values.</param>
        /// <param name="countOnly">Defualt value is false. count_only when set returns only the count of the keys in the range.</param>
        /// <param name="minModRevision">Defualt value is 0. min_mod_revision is the lower bound for returned key mod revisions; all keys with
        /// lesser mod revisions will be filtered away.</param>
        /// <param name="maxModRevision">Defualt value is 0. max_mod_revision is the upper bound for returned key mod revisions; all keys with
        /// greater mod revisions will be filtered away.</param>
        /// <param name="minCreateRevision">Defualt value is 0. min_create_revision is the lower bound for returned key create revisions; all keys with
        /// lesser create trevisions will be filtered away.</param>
        /// <param name="maxCreateRevision">Defualt value is 0. max_create_revision is the upper bound for returned key create revisions; all keys with
        /// greater create revisions will be filtered away.</param>
        /// <returns>RangeRequest</returns>
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

        /// <summary>
        /// Range gets the keys in the range from the key-value store.
        /// </summary>
        /// <param name="key">key is the first key for the range. If range_end is not given, the request only looks up key.</param>
        /// <param name="rangeEnd">range_end is the upper bound on the requested range [key, range_end).
        /// If range_end is '\0', the range is all keys >= key.
        /// If range_end is key plus one (e.g., "aa"+1 == "ab", "a\xff"+1 == "b"),
        /// then the range request gets all keys prefixed with key.
        /// If both key and range_end are '\0', then the range request returns all keys.</param>
        /// <param name="limit">Defualt value is 0. limit is a limit on the number of keys returned for the request. When limit is set to 0,
        /// it is treated as no limit.</param>
        /// <param name="revision">Defualt value is 0. revision is the point-in-time of the key-value store to use for the range.
        /// If revision is less or equal to zero, the range is over the newest key-value store.
        /// If the revision has been compacted, ErrCompacted is returned as a response.</param>
        /// <param name="sortOrder">Defualt value is SortOrder.None. sort_order is the order for returned sorted results.</param>
        /// <param name="sortTarget">Defualt value is SortTarget.Key. sort_target is the key-value field to use for sorting.</param>
        /// <param name="serializable">Defualt value is false. serializable sets the range request to use serializable member-local reads.
        /// Range requests are linearizable by default; linearizable requests have higher
        /// latency and lower throughput than serializable requests but reflect the current
        /// consensus of the cluster. For better performance, in exchange for possible stale reads,
        /// a serializable range request is served locally without needing to reach consensus
        /// with other nodes in the cluster.</param>
        /// <param name="keysOnly">Defualt value is false. keys_only when set returns only the keys and not the values.</param>
        /// <param name="countOnly">Defualt value is false. count_only when set returns only the count of the keys in the range.</param>
        /// <param name="minModRevision">Defualt value is 0. min_mod_revision is the lower bound for returned key mod revisions; all keys with
        /// lesser mod revisions will be filtered away.</param>
        /// <param name="maxModRevision">Defualt value is 0. max_mod_revision is the upper bound for returned key mod revisions; all keys with
        /// greater mod revisions will be filtered away.</param>
        /// <param name="minCreateRevision">Defualt value is 0. min_create_revision is the lower bound for returned key create revisions; all keys with
        /// lesser create trevisions will be filtered away.</param>
        /// <param name="maxCreateRevision">Defualt value is 0. max_create_revision is the upper bound for returned key create revisions; all keys with
        /// greater create revisions will be filtered away.</param>
        /// <returns>The response received from the server.</returns>
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

        /// <summary>
        /// Range gets the keys in the range from the key-value store.
        /// </summary>
        /// <param name="key">key is the first key for the range. If range_end is not given, the request only looks up key.</param>
        /// <param name="limit">Defualt value is 0. limit is a limit on the number of keys returned for the request. When limit is set to 0,
        /// it is treated as no limit.</param>
        /// <param name="revision">Defualt value is 0. revision is the point-in-time of the key-value store to use for the range.
        /// If revision is less or equal to zero, the range is over the newest key-value store.
        /// If the revision has been compacted, ErrCompacted is returned as a response.</param>
        /// <param name="sortOrder">Defualt value is SortOrder.None. sort_order is the order for returned sorted results.</param>
        /// <param name="sortTarget">Defualt value is SortTarget.Key. sort_target is the key-value field to use for sorting.</param>
        /// <param name="serializable">Defualt value is false. serializable sets the range request to use serializable member-local reads.
        /// Range requests are linearizable by default; linearizable requests have higher
        /// latency and lower throughput than serializable requests but reflect the current
        /// consensus of the cluster. For better performance, in exchange for possible stale reads,
        /// a serializable range request is served locally without needing to reach consensus
        /// with other nodes in the cluster.</param>
        /// <param name="keysOnly">Defualt value is false. keys_only when set returns only the keys and not the values.</param>
        /// <param name="countOnly">Defualt value is false. count_only when set returns only the count of the keys in the range.</param>
        /// <param name="minModRevision">Defualt value is 0. min_mod_revision is the lower bound for returned key mod revisions; all keys with
        /// lesser mod revisions will be filtered away.</param>
        /// <param name="maxModRevision">Defualt value is 0. max_mod_revision is the upper bound for returned key mod revisions; all keys with
        /// greater mod revisions will be filtered away.</param>
        /// <param name="minCreateRevision">Defualt value is 0. min_create_revision is the lower bound for returned key create revisions; all keys with
        /// lesser create trevisions will be filtered away.</param>
        /// <param name="maxCreateRevision">Defualt value is 0. max_create_revision is the upper bound for returned key create revisions; all keys with
        /// greater create revisions will be filtered away.</param>
        /// <returns>The response received from the server.</returns>
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

        /// <summary>
        /// Range gets the keys in the range from the key-value store.
        /// </summary>
        /// <param name="key">key is the first key for the range. If range_end is not given, the request only looks up key.</param>
        /// <param name="limit">Defualt value is 0. limit is a limit on the number of keys returned for the request. When limit is set to 0,
        /// it is treated as no limit.</param>
        /// <param name="revision">Defualt value is 0. revision is the point-in-time of the key-value store to use for the range.
        /// If revision is less or equal to zero, the range is over the newest key-value store.
        /// If the revision has been compacted, ErrCompacted is returned as a response.</param>
        /// <param name="sortOrder">Defualt value is SortOrder.None. sort_order is the order for returned sorted results.</param>
        /// <param name="sortTarget">Defualt value is SortTarget.Key. sort_target is the key-value field to use for sorting.</param>
        /// <param name="serializable">Defualt value is false. serializable sets the range request to use serializable member-local reads.
        /// Range requests are linearizable by default; linearizable requests have higher
        /// latency and lower throughput than serializable requests but reflect the current
        /// consensus of the cluster. For better performance, in exchange for possible stale reads,
        /// a serializable range request is served locally without needing to reach consensus
        /// with other nodes in the cluster.</param>
        /// <param name="keysOnly">Defualt value is false. keys_only when set returns only the keys and not the values.</param>
        /// <param name="countOnly">Defualt value is false. count_only when set returns only the count of the keys in the range.</param>
        /// <param name="minModRevision">Defualt value is 0. min_mod_revision is the lower bound for returned key mod revisions; all keys with
        /// lesser mod revisions will be filtered away.</param>
        /// <param name="maxModRevision">Defualt value is 0. max_mod_revision is the upper bound for returned key mod revisions; all keys with
        /// greater mod revisions will be filtered away.</param>
        /// <param name="minCreateRevision">Defualt value is 0. min_create_revision is the lower bound for returned key create revisions; all keys with
        /// lesser create trevisions will be filtered away.</param>
        /// <param name="maxCreateRevision">Defualt value is 0. max_create_revision is the upper bound for returned key create revisions; all keys with
        /// greater create revisions will be filtered away.</param>
        /// <returns>The response received from the server.</returns>
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

        /// <summary>
        /// Range gets the keys in the range from the key-value store.
        /// </summary>
        /// <param name="key">key is the first key for the range. If range_end is not given, the request only looks up key.</param>
        /// <param name="rangeEnd">range_end is the upper bound on the requested range [key, range_end).
        /// If range_end is '\0', the range is all keys >= key.
        /// If range_end is key plus one (e.g., "aa"+1 == "ab", "a\xff"+1 == "b"),
        /// then the range request gets all keys prefixed with key.
        /// If both key and range_end are '\0', then the range request returns all keys.</param>
        /// <param name="limit">Defualt value is 0. limit is a limit on the number of keys returned for the request. When limit is set to 0,
        /// it is treated as no limit.</param>
        /// <param name="revision">Defualt value is 0. revision is the point-in-time of the key-value store to use for the range.
        /// If revision is less or equal to zero, the range is over the newest key-value store.
        /// If the revision has been compacted, ErrCompacted is returned as a response.</param>
        /// <param name="sortOrder">Defualt value is SortOrder.None. sort_order is the order for returned sorted results.</param>
        /// <param name="sortTarget">Defualt value is SortTarget.Key. sort_target is the key-value field to use for sorting.</param>
        /// <param name="serializable">Defualt value is false. serializable sets the range request to use serializable member-local reads.
        /// Range requests are linearizable by default; linearizable requests have higher
        /// latency and lower throughput than serializable requests but reflect the current
        /// consensus of the cluster. For better performance, in exchange for possible stale reads,
        /// a serializable range request is served locally without needing to reach consensus
        /// with other nodes in the cluster.</param>
        /// <param name="keysOnly">Defualt value is false. keys_only when set returns only the keys and not the values.</param>
        /// <param name="countOnly">Defualt value is false. count_only when set returns only the count of the keys in the range.</param>
        /// <param name="minModRevision">Defualt value is 0. min_mod_revision is the lower bound for returned key mod revisions; all keys with
        /// lesser mod revisions will be filtered away.</param>
        /// <param name="maxModRevision">Defualt value is 0. max_mod_revision is the upper bound for returned key mod revisions; all keys with
        /// greater mod revisions will be filtered away.</param>
        /// <param name="minCreateRevision">Defualt value is 0. min_create_revision is the lower bound for returned key create revisions; all keys with
        /// lesser create trevisions will be filtered away.</param>
        /// <param name="maxCreateRevision">Defualt value is 0. max_create_revision is the upper bound for returned key create revisions; all keys with
        /// greater create revisions will be filtered away.</param>
        /// <returns>The response received from the server.</returns>
        public static RangeResponse Range(this Client client, ByteString key, ByteString rangeEnd = null,
           long limit = 0, long revision = 0, SortOrder sortOrder = SortOrder.None,
           SortTarget sortTarget = SortTarget.Key, bool serializable = false,
           bool keysOnly = false, bool countOnly = false, long minModRevision = 0, long maxModRevision = 0,
           long minCreateRevision = 0, long maxCreateRevision = 0)
        {
            var request = client.CreateRangeRequest(key, rangeEnd,
                limit, revision, sortOrder, sortTarget, serializable, keysOnly, countOnly, minModRevision,
                maxModRevision, minCreateRevision, maxCreateRevision);
            return client.KV.Range(request);
        }

        /// <summary>
        /// Range gets the keys in the range from the key-value store.
        /// </summary>
        /// <param name="key">key is the first key for the range. If range_end is not given, the request only looks up key.</param>
        /// <param name="rangeEnd">range_end is the upper bound on the requested range [key, range_end).
        /// If range_end is '\0', the range is all keys >= key.
        /// If range_end is key plus one (e.g., "aa"+1 == "ab", "a\xff"+1 == "b"),
        /// then the range request gets all keys prefixed with key.
        /// If both key and range_end are '\0', then the range request returns all keys.</param>
        /// <param name="limit">Defualt value is 0. limit is a limit on the number of keys returned for the request. When limit is set to 0,
        /// it is treated as no limit.</param>
        /// <param name="revision">Defualt value is 0. revision is the point-in-time of the key-value store to use for the range.
        /// If revision is less or equal to zero, the range is over the newest key-value store.
        /// If the revision has been compacted, ErrCompacted is returned as a response.</param>
        /// <param name="sortOrder">Defualt value is SortOrder.None. sort_order is the order for returned sorted results.</param>
        /// <param name="sortTarget">Defualt value is SortTarget.Key. sort_target is the key-value field to use for sorting.</param>
        /// <param name="serializable">Defualt value is false. serializable sets the range request to use serializable member-local reads.
        /// Range requests are linearizable by default; linearizable requests have higher
        /// latency and lower throughput than serializable requests but reflect the current
        /// consensus of the cluster. For better performance, in exchange for possible stale reads,
        /// a serializable range request is served locally without needing to reach consensus
        /// with other nodes in the cluster.</param>
        /// <param name="keysOnly">Defualt value is false. keys_only when set returns only the keys and not the values.</param>
        /// <param name="countOnly">Defualt value is false. count_only when set returns only the count of the keys in the range.</param>
        /// <param name="minModRevision">Defualt value is 0. min_mod_revision is the lower bound for returned key mod revisions; all keys with
        /// lesser mod revisions will be filtered away.</param>
        /// <param name="maxModRevision">Defualt value is 0. max_mod_revision is the upper bound for returned key mod revisions; all keys with
        /// greater mod revisions will be filtered away.</param>
        /// <param name="minCreateRevision">Defualt value is 0. min_create_revision is the lower bound for returned key create revisions; all keys with
        /// lesser create trevisions will be filtered away.</param>
        /// <param name="maxCreateRevision">Defualt value is 0. max_create_revision is the upper bound for returned key create revisions; all keys with
        /// greater create revisions will be filtered away.</param>
        /// <returns>The call object.</returns>
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

        /// <summary>
        /// Range gets the keys in the range from the key-value store.
        /// </summary>
        /// <param name="key">key is the first key for the range. If range_end is not given, the request only looks up key.</param>
        /// <param name="rangeEnd">range_end is the upper bound on the requested range [key, range_end).
        /// If range_end is '\0', the range is all keys >= key.
        /// If range_end is key plus one (e.g., "aa"+1 == "ab", "a\xff"+1 == "b"),
        /// then the range request gets all keys prefixed with key.
        /// If both key and range_end are '\0', then the range request returns all keys.</param>
        /// <param name="limit">Defualt value is 0. limit is a limit on the number of keys returned for the request. When limit is set to 0,
        /// it is treated as no limit.</param>
        /// <param name="revision">Defualt value is 0. revision is the point-in-time of the key-value store to use for the range.
        /// If revision is less or equal to zero, the range is over the newest key-value store.
        /// If the revision has been compacted, ErrCompacted is returned as a response.</param>
        /// <param name="sortOrder">Defualt value is SortOrder.None. sort_order is the order for returned sorted results.</param>
        /// <param name="sortTarget">Defualt value is SortTarget.Key. sort_target is the key-value field to use for sorting.</param>
        /// <param name="serializable">Defualt value is false. serializable sets the range request to use serializable member-local reads.
        /// Range requests are linearizable by default; linearizable requests have higher
        /// latency and lower throughput than serializable requests but reflect the current
        /// consensus of the cluster. For better performance, in exchange for possible stale reads,
        /// a serializable range request is served locally without needing to reach consensus
        /// with other nodes in the cluster.</param>
        /// <param name="keysOnly">Defualt value is false. keys_only when set returns only the keys and not the values.</param>
        /// <param name="countOnly">Defualt value is false. count_only when set returns only the count of the keys in the range.</param>
        /// <param name="minModRevision">Defualt value is 0. min_mod_revision is the lower bound for returned key mod revisions; all keys with
        /// lesser mod revisions will be filtered away.</param>
        /// <param name="maxModRevision">Defualt value is 0. max_mod_revision is the upper bound for returned key mod revisions; all keys with
        /// greater mod revisions will be filtered away.</param>
        /// <param name="minCreateRevision">Defualt value is 0. min_create_revision is the lower bound for returned key create revisions; all keys with
        /// lesser create trevisions will be filtered away.</param>
        /// <param name="maxCreateRevision">Defualt value is 0. max_create_revision is the upper bound for returned key create revisions; all keys with
        /// greater create revisions will be filtered away.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<RangeResponse> RangeAsync(this Client client, ByteString key, ByteString rangeEnd = null,
           long limit = 0, long revision = 0, SortOrder sortOrder = SortOrder.None,
           SortTarget sortTarget = SortTarget.Key, bool serializable = false,
           bool keysOnly = false, bool countOnly = false, long minModRevision = 0, long maxModRevision = 0,
           long minCreateRevision = 0, long maxCreateRevision = 0)
        {
            var request = client.CreateRangeRequest(key, rangeEnd,
                limit, revision, sortOrder, sortTarget, serializable, keysOnly, countOnly, minModRevision,
                maxModRevision, minCreateRevision, maxCreateRevision);
            return client.KV.RangeAsync(request);
        }

        /// <summary>
        /// Range gets the keys in the range from the key-value store.
        /// </summary>
        /// <param name="key">key is the first key for the range. If range_end is not given, the request only looks up key.</param>
        /// <param name="limit">Defualt value is 0. limit is a limit on the number of keys returned for the request. When limit is set to 0,
        /// it is treated as no limit.</param>
        /// <param name="revision">Defualt value is 0. revision is the point-in-time of the key-value store to use for the range.
        /// If revision is less or equal to zero, the range is over the newest key-value store.
        /// If the revision has been compacted, ErrCompacted is returned as a response.</param>
        /// <param name="sortOrder">Defualt value is SortOrder.None. sort_order is the order for returned sorted results.</param>
        /// <param name="sortTarget">Defualt value is SortTarget.Key. sort_target is the key-value field to use for sorting.</param>
        /// <param name="serializable">Defualt value is false. serializable sets the range request to use serializable member-local reads.
        /// Range requests are linearizable by default; linearizable requests have higher
        /// latency and lower throughput than serializable requests but reflect the current
        /// consensus of the cluster. For better performance, in exchange for possible stale reads,
        /// a serializable range request is served locally without needing to reach consensus
        /// with other nodes in the cluster.</param>
        /// <param name="keysOnly">Defualt value is false. keys_only when set returns only the keys and not the values.</param>
        /// <param name="countOnly">Defualt value is false. count_only when set returns only the count of the keys in the range.</param>
        /// <param name="minModRevision">Defualt value is 0. min_mod_revision is the lower bound for returned key mod revisions; all keys with
        /// lesser mod revisions will be filtered away.</param>
        /// <param name="maxModRevision">Defualt value is 0. max_mod_revision is the upper bound for returned key mod revisions; all keys with
        /// greater mod revisions will be filtered away.</param>
        /// <param name="minCreateRevision">Defualt value is 0. min_create_revision is the lower bound for returned key create revisions; all keys with
        /// lesser create trevisions will be filtered away.</param>
        /// <param name="maxCreateRevision">Defualt value is 0. max_create_revision is the upper bound for returned key create revisions; all keys with
        /// greater create revisions will be filtered away.</param>
        /// <returns>The call object.</returns>
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

        /// <summary>
        /// Range gets the keys in the range from the key-value store.
        /// </summary>
        /// <param name="key">key is the first key for the range. If range_end is not given, the request only looks up key.</param>
        /// <param name="limit">Defualt value is 0. limit is a limit on the number of keys returned for the request. When limit is set to 0,
        /// it is treated as no limit.</param>
        /// <param name="revision">Defualt value is 0. revision is the point-in-time of the key-value store to use for the range.
        /// If revision is less or equal to zero, the range is over the newest key-value store.
        /// If the revision has been compacted, ErrCompacted is returned as a response.</param>
        /// <param name="sortOrder">Defualt value is SortOrder.None. sort_order is the order for returned sorted results.</param>
        /// <param name="sortTarget">Defualt value is SortTarget.Key. sort_target is the key-value field to use for sorting.</param>
        /// <param name="serializable">Defualt value is false. serializable sets the range request to use serializable member-local reads.
        /// Range requests are linearizable by default; linearizable requests have higher
        /// latency and lower throughput than serializable requests but reflect the current
        /// consensus of the cluster. For better performance, in exchange for possible stale reads,
        /// a serializable range request is served locally without needing to reach consensus
        /// with other nodes in the cluster.</param>
        /// <param name="keysOnly">Defualt value is false. keys_only when set returns only the keys and not the values.</param>
        /// <param name="countOnly">Defualt value is false. count_only when set returns only the count of the keys in the range.</param>
        /// <param name="minModRevision">Defualt value is 0. min_mod_revision is the lower bound for returned key mod revisions; all keys with
        /// lesser mod revisions will be filtered away.</param>
        /// <param name="maxModRevision">Defualt value is 0. max_mod_revision is the upper bound for returned key mod revisions; all keys with
        /// greater mod revisions will be filtered away.</param>
        /// <param name="minCreateRevision">Defualt value is 0. min_create_revision is the lower bound for returned key create revisions; all keys with
        /// lesser create trevisions will be filtered away.</param>
        /// <param name="maxCreateRevision">Defualt value is 0. max_create_revision is the upper bound for returned key create revisions; all keys with
        /// greater create revisions will be filtered away.</param>
        /// <returns>The call object.</returns>
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

        /// <summary>
        /// Create DeleteRangeRequest.
        /// </summary>
        /// <param name="key">key is the first key to delete in the range.</param>
        /// <param name="rangeEnd">range_end is the key following the last key to delete for the range [key, range_end).
        /// If range_end is not given, the range is defined to contain only the key argument.
        /// If range_end is one bit larger than the given key, then the range is all the keys
        /// with the prefix (the given key).
        /// If range_end is '\0', the range is all keys greater than or equal to the key argument.</param>
        /// <param name="prevKv">If prev_kv is set, etcd gets the previous key-value pairs before deleting it.
        /// The previous key-value pairs will be returned in the delete response.</returns>
        /// <returns>The response received from the server.</returns>
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

        /// <summary>
        /// DeleteRange deletes the given range from the key-value store.
        /// A delete request increments the revision of the key-value store
        /// and generates a delete event in the event history for every deleted key.
        /// </summary>
        /// <param name="key">key is the first key to delete in the range.</param>
        /// <param name="rangeEnd">range_end is the key following the last key to delete for the range [key, range_end).
        /// If range_end is not given, the range is defined to contain only the key argument.
        /// If range_end is one bit larger than the given key, then the range is all the keys
        /// with the prefix (the given key).
        /// If range_end is '\0', the range is all keys greater than or equal to the key argument.</param>
        /// <param name="prevKv">If prev_kv is set, etcd gets the previous key-value pairs before deleting it.
        /// The previous key-value pairs will be returned in the delete response.</returns>
        /// <returns>The response received from the server.</returns>
        public static DeleteRangeResponse DeleteRange(this Client client, string key, string rangeEnd = null,
           bool prevKv = false)
        {
            return client.DeleteRange(ByteString.CopyFromUtf8(key),
                rangeEnd == null ? null : ByteString.CopyFromUtf8(rangeEnd), prevKv);
        }

        /// <summary>
        /// DeleteRange deletes the given range from the key-value store.
        /// A delete request increments the revision of the key-value store
        /// and generates a delete event in the event history for every deleted key.
        /// </summary>
        /// <param name="key">key is the first key to delete in the range.</param>
        /// <param name="rangeEnd">range_end is the key following the last key to delete for the range [key, range_end).
        /// If range_end is not given, the range is defined to contain only the key argument.
        /// If range_end is one bit larger than the given key, then the range is all the keys
        /// with the prefix (the given key).
        /// If range_end is '\0', the range is all keys greater than or equal to the key argument.</param>
        /// <param name="prevKv">If prev_kv is set, etcd gets the previous key-value pairs before deleting it.
        /// The previous key-value pairs will be returned in the delete response.</returns>
        /// <returns>The response received from the server.</returns>
        public static DeleteRangeResponse DeleteRange(this Client client, ByteString key, ByteString rangeEnd = null,
           bool prevKv = false)
        {
            var request = client.CreateDeleteRangeRequest(key, rangeEnd, prevKv);
            return client.KV.DeleteRange(request);
        }

        /// <summary>
        /// DeleteRange deletes the given range from the key-value store.
        /// A delete request increments the revision of the key-value store
        /// and generates a delete event in the event history for every deleted key.
        /// </summary>
        /// <param name="key">key is the first key to delete in the range.</param>
        /// <param name="prevKv">If prev_kv is set, etcd gets the previous key-value pairs before deleting it.
        /// The previous key-value pairs will be returned in the delete response.</returns>
        /// <returns>The response received from the server.</returns>
        public static DeleteRangeResponse DeleteAll(this Client client, string key, bool prevKv = false)
        {
            return client.DeleteAll(ByteString.CopyFromUtf8(key), prevKv);
        }

        /// <summary>
        /// DeleteRange deletes the given range from the key-value store.
        /// A delete request increments the revision of the key-value store
        /// and generates a delete event in the event history for every deleted key.
        /// </summary>
        /// <param name="key">key is the first key to delete in the range.</param>
        /// <param name="prevKv">If prev_kv is set, etcd gets the previous key-value pairs before deleting it.
        /// The previous key-value pairs will be returned in the delete response.</returns>
        /// <returns>The response received from the server.</returns>
        public static DeleteRangeResponse DeleteAll(this Client client, ByteString key, bool prevKv = false)
        {
            return client.DeleteRange(key, Constants.NullKey, prevKv);
        }

        /// <summary>
        /// DeleteRange deletes the given range from the key-value store.
        /// A delete request increments the revision of the key-value store
        /// and generates a delete event in the event history for every deleted key.
        /// </summary>
        /// <param name="key">key is the first key to delete in the range.</param>
        /// <param name="rangeEnd">range_end is the key following the last key to delete for the range [key, range_end).
        /// If range_end is not given, the range is defined to contain only the key argument.
        /// If range_end is one bit larger than the given key, then the range is all the keys
        /// with the prefix (the given key).
        /// If range_end is '\0', the range is all keys greater than or equal to the key argument.</param>
        /// <param name="prevKv">If prev_kv is set, etcd gets the previous key-value pairs before deleting it.
        /// The previous key-value pairs will be returned in the delete response.</returns>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<DeleteRangeResponse> DeleteRangeAsync(this Client client, string key, string rangeEnd = null,
           bool prevKv = false)
        {
            return client.DeleteRangeAsync(ByteString.CopyFromUtf8(key),
                rangeEnd == null ? null : ByteString.CopyFromUtf8(rangeEnd), prevKv);
        }

        /// <summary>
        /// DeleteRange deletes the given range from the key-value store.
        /// A delete request increments the revision of the key-value store
        /// and generates a delete event in the event history for every deleted key.
        /// </summary>
        /// <param name="key">key is the first key to delete in the range.</param>
        /// <param name="rangeEnd">range_end is the key following the last key to delete for the range [key, range_end).
        /// If range_end is not given, the range is defined to contain only the key argument.
        /// If range_end is one bit larger than the given key, then the range is all the keys
        /// with the prefix (the given key).
        /// If range_end is '\0', the range is all keys greater than or equal to the key argument.</param>
        /// <param name="prevKv">If prev_kv is set, etcd gets the previous key-value pairs before deleting it.
        /// The previous key-value pairs will be returned in the delete response.</returns>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<DeleteRangeResponse> DeleteRangeAsync(this Client client, ByteString key, ByteString rangeEnd = null,
           bool prevKv = false)
        {
            var request = client.CreateDeleteRangeRequest(key, rangeEnd, prevKv);
            return client.KV.DeleteRangeAsync(request);
        }

        /// <summary>
        /// DeleteRange deletes the given range from the key-value store.
        /// A delete request increments the revision of the key-value store
        /// and generates a delete event in the event history for every deleted key.
        /// </summary>
        /// <param name="key">key is the first key to delete in the range.</param>
        /// <param name="prevKv">If prev_kv is set, etcd gets the previous key-value pairs before deleting it.
        /// The previous key-value pairs will be returned in the delete response.</returns>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<DeleteRangeResponse> DeleteAllAsync(this Client client, string key, bool prevKv = false)
        {
            return client.DeleteAllAsync(ByteString.CopyFromUtf8(key), prevKv);
        }

        /// <summary>
        /// DeleteRange deletes the given range from the key-value store.
        /// A delete request increments the revision of the key-value store
        /// and generates a delete event in the event history for every deleted key.
        /// </summary>
        /// <param name="key">key is the first key to delete in the range.</param>
        /// <param name="prevKv">If prev_kv is set, etcd gets the previous key-value pairs before deleting it.
        /// The previous key-value pairs will be returned in the delete response.</returns>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<DeleteRangeResponse> DeleteAllAsync(this Client client, ByteString key, bool prevKv = false)
        {
            return client.DeleteRangeAsync(key, Constants.NullKey, prevKv);
        }

        #endregion DeleteRange

        #region Compact

        /// <summary>
        /// Create CompactionRequest
        /// </summary>
        /// <param name="revision">The request to send to the server.</param>
        /// <param name="physical">The initial metadata to send with the call. This parameter is optional.</param>
        /// <returns>The response received from the server.</returns>
        public static CompactionRequest CreateCompactionRequest(this Client client, long revision, bool physical = false)
        {
            return new CompactionRequest()
            {
                Revision = revision,
                Physical = physical
            };
        }

        /// <summary>
        /// Compact compacts the event history in the etcd key-value store. The key-value
        /// store should be periodically compacted or the event history will continue to grow
        /// indefinitely.
        /// </summary>
        /// <param name="revision">The request to send to the server.</param>
        /// <param name="physical">The initial metadata to send with the call. This parameter is optional.</param>
        /// <returns>The response received from the server.</returns>
        public static CompactionResponse Compact(this Client client, long revision, bool physical = false,
          bool prevKv = false)
        {
            var request = client.CreateCompactionRequest(revision, physical);
            return client.KV.Compact(request);
        }

        /// <summary>
        /// Compact compacts the event history in the etcd key-value store. The key-value
        /// store should be periodically compacted or the event history will continue to grow
        /// indefinitely.
        /// </summary>
        /// <param name="revision">The request to send to the server.</param>
        /// <param name="physical">The initial metadata to send with the call. This parameter is optional.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<CompactionResponse> CompactAsync(this Client client, long revision, bool physical = false,
          bool prevKv = false)
        {
            var request = client.CreateCompactionRequest(revision, physical);
            return client.KV.CompactAsync(request);
        }

        #endregion Compact

        #region Txn

        /// <summary>
        /// Txn processes multiple requests in a single transaction.
        /// A txn request increments the revision of the key-value store
        /// and generates events with the same revision for every completed request.
        /// It is not allowed to modify the same key several times within one txn.
        /// </summary>
        /// <param name="request">The request to send to the server.</param>
        /// <returns>The response received from the server.</returns>
        public static TxnResponse Txn(this Client client, TxnRequest request)
        {
            return client.KV.Txn(request);
        }

        /// <summary>
        /// Txn processes multiple requests in a single transaction.
        /// A txn request increments the revision of the key-value store
        /// and generates events with the same revision for every completed request.
        /// It is not allowed to modify the same key several times within one txn.
        /// </summary>
        /// <param name="request">The request to send to the server.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<TxnResponse> TxnAsync(this Client client, TxnRequest request)
        {
            return client.KV.TxnAsync(request);
        }

        #endregion Txn
    }
}