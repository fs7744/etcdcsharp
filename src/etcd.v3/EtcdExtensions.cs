using Etcdserverpb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etcd;

public static class EtcdExtensions
{
    private const string rangeEndString = "\x00";

    public static long FindRevision(this WatchResponse response, long startRevision)
    {
        if (response.CompactRevision >= startRevision)
        {
            startRevision = response.CompactRevision + 1;
        }

        if (response.Header.Revision >= startRevision)
        {
            startRevision = response.Header.Revision + 1;
        }

        return startRevision;
    }

    public static string GetRangeEnd(this string prefixKey)
    {
        if (prefixKey.Length == 0)
        {
            return rangeEndString;
        }

        var s = prefixKey.AsSpan();
        char cc = s[prefixKey.Length - 1];
        ++cc;
        Span<char> c = stackalloc char[1] { cc };
        return string.Concat(s.Slice(0, prefixKey.Length - 1), c);
    }
}