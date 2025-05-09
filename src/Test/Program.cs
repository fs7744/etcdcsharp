using System;
using Etcd;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Etcdserverpb;
using Google.Protobuf;

namespace Test
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //var b = new ConfigurationBuilder();
            //b.UseEtcd(new Etcd.Configuration.EtcdConfigurationOptions()
            //{
            //    Prefix = "/ReverseProxy/",
            //    RemovePrefix = true,
            //    EtcdClientOptions = new EtcdClientOptions() { Address = ["http://172.16.171.56:8068"] }
            //});
            //var c = b.Build();
            //Test(c);

            ServiceCollection services = new();
            services.UseEtcdClient();
            services.AddEtcdClient("test", new EtcdClientOptions() { Address = ["http://172.16.171.56:8068"] });
            var p = services.BuildServiceProvider();
            //var factory = p.GetRequiredService<IEtcdClientFactory>();
            var client = p.GetRequiredKeyedService<IEtcdClient>("test");

            string v = (await client.RangeAsync(new RangeRequest() { Key = ByteString.CopyFromUtf8("/ReverseProxy/") })).Kvs?.First().Value.ToStrUtf8();

            foreach (var i in await client.GetRangeValueUtf8Async("/ReverseProxy/"))
            {
                Console.WriteLine($"{i.Key} : {i.Value}");
            }

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

        private static void Test(IConfigurationRoot c)
        {
            foreach (var i in c.GetChildren())
            {
                Console.WriteLine($"{i.Key} : {i.Value}");
            }
            c.GetReloadToken().RegisterChangeCallback(i =>
            {
                Test(i as IConfigurationRoot);
            }, c);
        }
    }
}