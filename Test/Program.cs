using ETCD.V3.Test;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin!");
            var test = new KVTest(new ClientFixture());
            test.Put();
            test.PutWithNotExistLease();
            test.Get();
            test.GetWithRev();
            test.GetSortedPrefix();
            test.GetAndDeleteWithPrefix();
            test.Txn();
            Console.WriteLine("End!");
        }
    }
}
