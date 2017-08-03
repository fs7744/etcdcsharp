using Etcdserverpb;
using Google.Protobuf;
using System;
using Xunit;
using static Etcdserverpb.Compare.Types;
using static Etcdserverpb.RangeRequest.Types;

namespace ETCD.V3.Test
{
    public class KVTest : IClassFixture<ClientFixture>
    {
        private static readonly ByteString SAMPLE_KEY = ByteString.CopyFromUtf8("sample_key");
        private static readonly ByteString SAMPLE_VALUE = ByteString.CopyFromUtf8("sample_value");
        private static readonly ByteString SAMPLE_KEY_2 = ByteString.CopyFromUtf8("sample_key2");
        private static readonly ByteString SAMPLE_VALUE_2 = ByteString.CopyFromUtf8("sample_value2");
        private static readonly ByteString SAMPLE_KEY_3 = ByteString.CopyFromUtf8("sample_key3");
        private ClientFixture _Fixture;

        public KVTest(ClientFixture fixture)
        {
            _Fixture = fixture;
        }

        [Fact]
        public void Put()
        {
            var res = _Fixture.Client.Put(SAMPLE_KEY, SAMPLE_VALUE);
            Assert.NotNull(res.Header);
            Assert.True(res.PrevKv == null);

            //Delete(SAMPLE_KEY);
        }

        private void Delete(ByteString key, long? number = null)
        {
            var res = _Fixture.Client.GetAll(key);
            if (number.HasValue)
                Assert.Equal(number.Value, res.Count);
            var delRes = _Fixture.Client.DeleteAll(key);
            Assert.Equal(res.Count, delRes.Deleted);
        }

        [Fact]
        public void PutWithNotExistLease()
        {
            Exception ex = null;
            try
            {
                var res = _Fixture.Client.Put(SAMPLE_KEY, SAMPLE_VALUE, 9999L);
            }
            catch (Exception e)
            {
                ex = e;
            }

            Assert.Equal("Status(StatusCode=NotFound, Detail=\"etcdserver: requested lease not found\")", ex.Message);
        }

        [Fact]
        public void Get()
        {
            _Fixture.Client.Put(SAMPLE_KEY_2, SAMPLE_VALUE_2);
            var res = _Fixture.Client.Range(SAMPLE_KEY_2);
            Assert.Equal(1, res.Count);
            Assert.Equal(1, res.Kvs.Count);
            Assert.Equal(SAMPLE_VALUE_2.ToStringUtf8(), res.Kvs[0].Value.ToStringUtf8());
            Assert.False(res.More);
        }

        [Fact]
        public void GetWithRev()
        {
            var putRes = _Fixture.Client.Put(SAMPLE_KEY_3, SAMPLE_VALUE);
            _Fixture.Client.Put(SAMPLE_KEY_3, SAMPLE_VALUE_2);
            var res = _Fixture.Client.Range(SAMPLE_KEY_3, revision: putRes.Header.Revision);
            Assert.Equal(1, res.Count);
            Assert.Equal(1, res.Kvs.Count);
            Assert.Equal(SAMPLE_VALUE.ToStringUtf8(), res.Kvs[0].Value.ToStringUtf8());
            Assert.False(res.More);
        }

        private void PutKeysWithPrefix(string prefix, int numPrefixes)
        {
            for (int i = 0; i < numPrefixes; i++)
            {
                var key = ByteString.CopyFromUtf8(prefix + i);
                var value = ByteString.CopyFromUtf8("" + i);
                _Fixture.Client.Put(key, value);
            }
        }

        [Fact]
        public void GetSortedPrefix()
        {
            string prefix = TestUtil.RandomString();
            int numPrefix = 3;
            PutKeysWithPrefix(prefix, numPrefix);
            var res = _Fixture.Client.GetAll(prefix, sortTarget: SortTarget.Key,
                sortOrder: SortOrder.Descend);
            Assert.Equal(numPrefix, res.Count);
            Assert.Equal(numPrefix, res.Kvs.Count);
            for (int i = 0; i < numPrefix; i++)
            {
                Assert.Equal(prefix + (numPrefix - i - 1), res.Kvs[i].Key.ToStringUtf8());
                Assert.Equal((numPrefix - i - 1).ToString(), res.Kvs[i].Value.ToStringUtf8());
            }
        }

        [Fact]
        public void GetAndDeleteWithPrefix()
        {
            string prefix = TestUtil.RandomString();
            int numPrefix = 10;
            PutKeysWithPrefix(prefix, numPrefix);
            Delete(ByteString.CopyFromUtf8(prefix), numPrefix);
        }

        [Fact]
        public void Txn()
        {
            var sampleKey = ByteString.CopyFromUtf8("txn_key");
            var sampleValue = ByteString.CopyFromUtf8("xyz");
            var cmpValue = ByteString.CopyFromUtf8("abc");
            var putValue = ByteString.CopyFromUtf8("XYZ");
            var putValueNew = ByteString.CopyFromUtf8("ABC");
            var putRes = _Fixture.Client.Put(sampleKey, sampleValue);
            var req = new TxnRequest();
            req.Compare.Add(new Compare()
            {
                Key = sampleKey,
                Target = CompareTarget.Value,
                Result = CompareResult.Greater,
                Value = cmpValue
            });
            req.Success.Add(new RequestOp()
            {
                RequestPut = new PutRequest()
                {
                    Key = sampleKey,
                    Value = putValue
                }
            });
            req.Failure.Add(new RequestOp()
            {
                RequestPut = new PutRequest()
                {
                    Key = sampleKey,
                    Value = putValueNew
                }
            });
            var txnRes = _Fixture.Client.KV.Txn(req);
            var res = _Fixture.Client.Range(sampleKey);
            Assert.Equal(1, res.Count);
            Assert.Equal(1, res.Kvs.Count);
            Assert.Equal(putValue.ToStringUtf8(), res.Kvs[0].Value.ToStringUtf8());
        }
    }
}