using Google.Protobuf;
using Grpc.Net.Client;
using System;

namespace ETCD.V3.Test
{
    public static class TestUtil
    {
        public static string RandomString()
        {
            return Guid.NewGuid().ToString();
        }

        public static ByteString RandomByteString()
        {
            return ByteString.CopyFromUtf8(RandomString());
        }
    }
}