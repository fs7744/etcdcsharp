using ETCD.V3.Test;
using System;
using Etcd;
using System.Linq;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var factory = EtcdClientFactory.Create();
            var client = factory.CreateClient(new EtcdClientOptions() { Address = ["http://172.16.171.56:8068"] });
            //await client.WatchRangeBackendAsync("/ReverseProxy/", i =>
            //{
            //    if (i.Events.Count > 0)
            //    {
            //        foreach (var item in i.Events)
            //        {
            //            Console.WriteLine($"{item.Type} {item.Kv.Key.ToStrUtf8()}");
            //        }
            //        throw new NotImplementedException();
            //    }
            //    return Task.CompletedTask;
            //}, startRevision: 6, reWatchWhenException: true);
            client.WatchBackend("/ReverseProxy/listen/1", i =>
           {
               if (i.Events.Count > 0)
               {
                   foreach (var item in i.Events)
                   {
                       Console.WriteLine($"{item.Type} {item.Kv.Key.ToStrUtf8()}");
                   }
                   throw new NotImplementedException();
               }
           }, startRevision: 0, reWatchWhenException: true);
            Console.ReadLine();
            //using var watcher = await client.WatchRangeAsync("/ReverseProxy/", startRevision: 6);
            //watcher.ForAll(i =>
            //{
            //    foreach (var item in i.Events)
            //    {
            //        Console.WriteLine($"{item.Type} {item.Kv.Key.ToStrUtf8()}");
            //    }
            //});

            //var c = 0;
            //await foreach (var item in watcher.ReadAllAsync())
            //{
            //    foreach (var i in item.Events)
            //    {
            //        Console.WriteLine($"{i.Type} {i.Kv.Key.ToStrUtf8()}");
            //    }
            //    c++;

            //    if (c > 5)
            //    {
            //        await watcher.CancelAsync();
            //        break;
            //    }
            //}

            //Console.WriteLine("Begin!");
            //var test = new KVTest(new ClientFixture());
            //test.Put();
            //test.PutWithNotExistLease();
            //test.Get();
            //test.GetWithRev();
            //test.GetSortedPrefix();
            //test.GetAndDeleteWithPrefix();
            //test.Txn();
            //Console.WriteLine("End!");
            //test._Fixture.Client.GetAll("").Kvs.ToList().ForEach(i =>
            //{
            //    Console.WriteLine($"{i.Key.ToStringUtf8()} : {i.Value.ToStringUtf8()}");
            //});
            //Console.ReadKey();
        }
    }
}