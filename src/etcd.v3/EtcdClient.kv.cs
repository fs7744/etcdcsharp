using Etcdserverpb;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using System.Threading;
using Google.Protobuf;
using System.Linq;
using System.Collections.Generic;

namespace Etcd;

public partial interface IEtcdClient : IDisposable
{
    public KV.KVClient KVClient { get; }

    RangeResponse Range(RangeRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<RangeResponse> RangeAsync(RangeRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    RangeResponse Get(string key, Metadata headers = null, DateTime? deadline = null);

    Task<RangeResponse> GetAsync(string key, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    string GetValueUtf8(string key, Metadata headers = null, DateTime? deadline = null);

    RangeResponse GetRange(string prefix, Metadata headers = null, DateTime? deadline = null);

    Task<RangeResponse> GetRangeAsync(string prefix, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    Task<string> GetValueUtf8Async(string key, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    IDictionary<string, string> GetRangeValueUtf8(string prefix, Metadata headers = null, DateTime? deadline = null);

    Task<IDictionary<string, string>> GetRangeValueUtf8Async(string prefix, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    PutResponse Put(PutRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<PutResponse> PutAsync(PutRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    PutResponse Put(string key, string value, Metadata headers = null, DateTime? deadline = null);

    Task<PutResponse> PutAsync(string key, string value, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    DeleteRangeResponse DeleteRange(DeleteRangeRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<DeleteRangeResponse> DeleteRangeAsync(DeleteRangeRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    DeleteRangeResponse Delete(string key, Metadata headers = null, DateTime? deadline = null);

    Task<DeleteRangeResponse> DeleteAsync(string key, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    DeleteRangeResponse DeleteRange(string prefix, Metadata headers = null, DateTime? deadline = null);

    Task<DeleteRangeResponse> DeleteRangeAsync(string prefix, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    TxnResponse Transaction(TxnRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<TxnResponse> TransactionAsync(TxnRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    CompactionResponse Compact(CompactionRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<CompactionResponse> CompactAsync(CompactionRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);
}

public partial class EtcdClient : IEtcdClient
{
    private KV.KVClient kvClient;
    public KV.KVClient KVClient => kvClient ??= new KV.KVClient(callInvoker);

    public RangeResponse Range(RangeRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return KVClient.Range(request, headers, deadline);
    }

    public async Task<RangeResponse> RangeAsync(RangeRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await KVClient.RangeAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public RangeResponse Get(string key, Metadata headers = null, DateTime? deadline = null)
    {
        return Range(new RangeRequest() { Key = ByteString.CopyFromUtf8(key) }, headers, deadline);
    }

    public Task<RangeResponse> GetAsync(string key, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return RangeAsync(new RangeRequest() { Key = ByteString.CopyFromUtf8(key) }, headers, deadline, cancellationToken);
    }

    public string GetValueUtf8(string key, Metadata headers = null, DateTime? deadline = null)
    {
        return Get(key, headers, deadline)?.Kvs.FirstOrDefault()?.Value.ToStrUtf8();
    }

    public async Task<string> GetValueUtf8Async(string key, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return (await GetAsync(key, headers, deadline, cancellationToken))?.Kvs.FirstOrDefault()?.Value.ToStrUtf8();
    }

    public RangeResponse GetRange(string prefix, Metadata headers = null, DateTime? deadline = null)
    {
        return Range(new RangeRequest() { Key = ByteString.CopyFromUtf8(prefix), RangeEnd = ByteString.CopyFromUtf8(GetRangeEnd(prefix)) }, headers, deadline);
    }

    public Task<RangeResponse> GetRangeAsync(string prefix, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return RangeAsync(new RangeRequest() { Key = ByteString.CopyFromUtf8(prefix), RangeEnd = ByteString.CopyFromUtf8(GetRangeEnd(prefix)) }, headers, deadline, cancellationToken);
    }

    public IDictionary<string, string> GetRangeValueUtf8(string prefix, Metadata headers = null, DateTime? deadline = null)
    {
        return GetRange(prefix, headers, deadline)?.Kvs.ToDictionary(i => i.Key.ToStrUtf8(), i => i.Value.ToStrUtf8());
    }

    public async Task<IDictionary<string, string>> GetRangeValueUtf8Async(string prefix, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return (await GetRangeAsync(prefix, headers, deadline, cancellationToken))?.Kvs.ToDictionary(i => i.Key.ToStrUtf8(), i => i.Value.ToStrUtf8());
    }

    public PutResponse Put(PutRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return KVClient.Put(request, headers, deadline);
    }

    public async Task<PutResponse> PutAsync(PutRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await KVClient.PutAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public PutResponse Put(string key, string value, Metadata headers = null, DateTime? deadline = null)
    {
        return Put(new PutRequest() { Key = ByteString.CopyFromUtf8(key), Value = ByteString.CopyFromUtf8(value) }, headers, deadline);
    }

    public Task<PutResponse> PutAsync(string key, string value, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return PutAsync(new PutRequest() { Key = ByteString.CopyFromUtf8(key), Value = ByteString.CopyFromUtf8(value) }, headers, deadline, cancellationToken);
    }

    public DeleteRangeResponse DeleteRange(DeleteRangeRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return KVClient.DeleteRange(request, headers, deadline);
    }

    public async Task<DeleteRangeResponse> DeleteRangeAsync(DeleteRangeRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await KVClient.DeleteRangeAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public DeleteRangeResponse Delete(string key, Metadata headers = null, DateTime? deadline = null)
    {
        return DeleteRange(new DeleteRangeRequest() { Key = ByteString.CopyFromUtf8(key) }, headers, deadline);
    }

    public Task<DeleteRangeResponse> DeleteAsync(string key, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return DeleteRangeAsync(new DeleteRangeRequest() { Key = ByteString.CopyFromUtf8(key) }, headers, deadline, cancellationToken);
    }

    public DeleteRangeResponse DeleteRange(string prefix, Metadata headers = null, DateTime? deadline = null)
    {
        return DeleteRange(new DeleteRangeRequest() { Key = ByteString.CopyFromUtf8(prefix), RangeEnd = ByteString.CopyFromUtf8(GetRangeEnd(prefix)) }, headers, deadline);
    }

    public Task<DeleteRangeResponse> DeleteRangeAsync(string prefix, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return DeleteRangeAsync(new DeleteRangeRequest() { Key = ByteString.CopyFromUtf8(prefix), RangeEnd = ByteString.CopyFromUtf8(GetRangeEnd(prefix)) }, headers, deadline, cancellationToken);
    }

    public TxnResponse Transaction(TxnRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return KVClient.Txn(request, headers, deadline);
    }

    public async Task<TxnResponse> TransactionAsync(TxnRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await KVClient.TxnAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public CompactionResponse Compact(CompactionRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return KVClient.Compact(request, headers, deadline);
    }

    public async Task<CompactionResponse> CompactAsync(CompactionRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await KVClient.CompactAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }
}