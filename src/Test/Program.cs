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
            services.AddSingleton<Testt>();
            var p = services.BuildServiceProvider();
            //var factory = p.GetRequiredService<IEtcdClientFactory>();
            //var client = p.GetRequiredKeyedService<IEtcdClient>("test");
            var client = p.GetRequiredService<Testt>().client;

            // string v = (await client.RangeAsync(new RangeRequest() { Key = ByteString.CopyFromUtf8("/ReverseProxy/") })).Kvs?.First().Value.ToStrUtf8();

            //await client.PutAsync("/ReverseProxy/test", "1");

            //await client.PutAsync(new PutRequest() { Key = ByteString.CopyFromUtf8("/ReverseProxy/test2"), Value = ByteString.CopyFromUtf8("2") });

            //foreach (var i in await client.GetRangeValueUtf8Async("/ReverseProxy/"))
            //{
            //    Console.WriteLine($"{i.Key} : {i.Value}");
            //}

            //Console.WriteLine($"DeleteRangeAsync");
            //await client.DeleteRangeAsync("/ReverseProxy/test");

            //await client.DeleteRangeAsync(new DeleteRangeRequest() { Key = ByteString.CopyFromUtf8("/ReverseProxy/test"), RangeEnd = ByteString.CopyFromUtf8(client.GetRangeEnd("/ReverseProxy/test")) });

            //foreach (var i in await client.GetRangeValueUtf8Async("/ReverseProxy/"))
            //{
            //    Console.WriteLine($"{i.Key} : {i.Value}");
            //}

            //foreach (var i in (await client.GetRangeAsync("/ReverseProxy/")).Kvs)
            //{
            //    Console.WriteLine($"{i.Key.ToStrUtf8()} : {i.Value.ToStrUtf8()}");
            //}

            //foreach (var i in (await client.RangeAsync(new RangeRequest() { Key = ByteString.CopyFromUtf8("/ReverseProxy/"), RangeEnd = ByteString.CopyFromUtf8(client.GetRangeEnd("/ReverseProxy/")) })).Kvs)
            //{
            //    Console.WriteLine($"{i.Key.ToStrUtf8()} : {i.Value.ToStrUtf8()}");
            //}

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
            //    }
            //    return Task.CompletedTask;
            //}, startRevision: 6, reWatchWhenException: true);
            //foreach (var i in await client.GetRangeValueUtf8Async("/ReverseProxy/"))
            //{
            //    Console.WriteLine($"{i.Key} : {i.Value}");
            //}
            await Task.Factory.StartNew(async () =>
            {
                long startRevision = 6;
                while (true)
                {
                    try
                    {
                        using var watcher = await client.WatchRangeAsync("/ReverseProxy/", startRevision: startRevision);
                        await watcher.ForAllAsync(i =>
                        {
                            startRevision = i.FindRevision(startRevision);
                            foreach (var item in i.Events)
                            {
                                Console.WriteLine($"{item.Type} {item.Kv.Key.ToStrUtf8()}");
                            }
                            return Task.CompletedTask;
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception: {ex.Message}");
                    }
                }
            });
            Console.ReadLine();
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

    public class Testt
    {
        public readonly IEtcdClient client;

        public Testt([FromKeyedServices("test")] IEtcdClient client)
        {
            this.client = client;
        }
    }
}