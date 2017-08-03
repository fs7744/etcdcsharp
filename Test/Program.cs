using ETCD.V3.Test;
using System;
using ETCD.V3;
using System.Linq;

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
            //test._Fixture.Client.GetAll("").Kvs.ToList().ForEach(i =>
            //{
            //    Console.WriteLine($"{i.Key.ToStringUtf8()} : {i.Value.ToStringUtf8()}");
            //});
            //Console.ReadKey();
        }
    }
}
