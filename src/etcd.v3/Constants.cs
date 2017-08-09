using Google.Protobuf;

namespace ETCD.V3
{
    public static class Constants
    {
        public const string Token = "token";
        public static readonly ByteString NullKey = ByteString.CopyFrom(new byte[1] { 0 });
    }
}