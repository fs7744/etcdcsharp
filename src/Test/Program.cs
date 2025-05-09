using System;
using Etcd;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Test
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            ServiceCollection services = new();
            services.UseEtcdClient();
            services.AddEtcdClient("test", new EtcdClientOptions() { Address = ["http://172.16.171.56:8068"] });
            var p = services.BuildServiceProvider();
            //var factory = p.GetRequiredService<IEtcdClientFactory>();
            var client = p.GetRequiredKeyedService<IEtcdClient>("test");
            //var factory = EtcdClientFactory.Create();
            //var client = factory.CreateClient(new EtcdClientOptions() { Address = ["http://172.16.171.56:8068"] });
            //foreach (var item in (await client.MemberListAsync(new Etcdserverpb.MemberListRequest() { Linearizable = false })).Members)
            //{
            //    Console.WriteLine($"{item.ID} {item.Name}");
            //}
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
            //foreach (var i in await client.GetRangeValueUtf8Async("/ReverseProxy/"))
            //{
            //    Console.WriteLine($"{i.Key} : {i.Value}");
            //}
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
        }
    }
}