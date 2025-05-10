# ETCD V3 Client
base code all Generate by grpc tools.
## Quick start

### Install package

* Package Manager

```
Install-Package etcd.v3 -Version 0.0.2
Install-Package etcd.v3.Configuration -Version 0.0.2
```
* .NET CLI

```
dotnet add package etcd.v3 --version 0.0.2
dotnet add package etcd.v3.Configuration --version 0.0.2
```

### new client

#### new client with DI

``` csharp
 ServiceCollection services = new();
 services.UseEtcdClient();
 services.AddEtcdClient("test", new EtcdClientOptions() { Address = ["http://xxx:2379"] });
 var p = services.BuildServiceProvider();

 var client = p.GetRequiredKeyedService<IEtcdClient>("test");

 // you also can create client by factory
 var factory = p.GetRequiredService<IEtcdClientFactory>();
var client2 = factory.CreateClient(new EtcdClientOptions() { Address = ["http://xxx:2379"] });


// get all config
foreach (var i in await client.GetRangeValueUtf8Async("/ReverseProxy/"))
{
   Console.WriteLine($"{i.Key} : {i.Value}");
}

// OR get client in ctor
public class Testt
{
    private readonly IEtcdClient client;

    public Testt([FromKeyedServices("test")] IEtcdClient client)
    {
        this.client = client;
    }
}


```

#### new client without DI

``` csharp
 var factory = EtcdClientFactory.Create();
var client = factory.CreateClient(new EtcdClientOptions() { Address = ["http://xxx:2379"] });

// get all config
foreach (var i in await client.GetRangeValueUtf8Async("/ReverseProxy/"))
{
   Console.WriteLine($"{i.Key} : {i.Value}");
}
```

#### use with Configuration

``` csharp
var b = new ConfigurationBuilder();
b.UseEtcd(new Etcd.Configuration.EtcdConfigurationOptions()
{
    Prefix = "/ReverseProxy/",
    RemovePrefix = true,
    EtcdClientOptions = new EtcdClientOptions() { Address = ["http://xxx:2379"] }
});
var c = b.Build();

// test watch change
Test(c);

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
```

#### Address

Address just parse by `GrpcChannel.ForAddress`, so support

- http://xxx:port
- https://xxx:port
- dns://xxx:port

### KV

#### get one by key

``` csharp
string v = await client.GetValueUtf8Async("/ReverseProxy/");
//or 
string v = (await client.GetAsync("/ReverseProxy/")).Kvs?.First().Value.ToStrUtf8();
//or
string v = (await client.RangeAsync(new RangeRequest() { Key = ByteString.CopyFromUtf8("/ReverseProxy/") })).Kvs?.First().Value.ToStrUtf8();
```

##### get all IDictionary<string, string>

``` csharp
foreach (var i in await client.GetRangeValueUtf8Async("/ReverseProxy/"))
{
    Console.WriteLine($"{i.Key} : {i.Value}");
}
//or
foreach (var i in (await client.GetRangeAsync("/ReverseProxy/")).Kvs)
{
    Console.WriteLine($"{i.Key.ToStrUtf8()} : {i.Value.ToStrUtf8()}");
}
//or
foreach (var i in (await client.RangeAsync(new RangeRequest() { Key = ByteString.CopyFromUtf8("/ReverseProxy/"), RangeEnd = ByteString.CopyFromUtf8("/ReverseProxy/".GetRangeEnd()) })).Kvs)
{
    Console.WriteLine($"{i.Key.ToStrUtf8()} : {i.Value.ToStrUtf8()}");
}
```

#### Put


``` csharp
await client.PutAsync("/ReverseProxy/test", "1");
//or
await client.PutAsync(new PutRequest() { Key = ByteString.CopyFromUtf8("/ReverseProxy/test"), Value = ByteString.CopyFromUtf8("1") });
```

#### Delete one


``` csharp
await client.DeleteAsync("/ReverseProxy/test");
//or
await client.DeleteRangeAsync(new DeleteRangeRequest() { Key = ByteString.CopyFromUtf8("/ReverseProxy/test") });
```

#### Delete all


``` csharp
await client.DeleteRangeAsync("/ReverseProxy/test");
//or
await client.DeleteRangeAsync(new DeleteRangeRequest() { Key = ByteString.CopyFromUtf8("/ReverseProxy/test"), RangeEnd = ByteString.CopyFromUtf8("/ReverseProxy/test".GetRangeEnd())) });
```

#### Watch


``` csharp
 await client.WatchRangeBackendAsync("/ReverseProxy/", i =>
 {
     if (i.Events.Count > 0)
     {
         foreach (var item in i.Events)
         {
             Console.WriteLine($"{item.Type} {item.Kv.Key.ToStrUtf8()}");
         }
     }
     return Task.CompletedTask;
 }, startRevision: 6, reWatchWhenException: true);

 // or
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
```

#### all grpc client

if IEtcdClient Missing some grpc method , you can just use grpc client to do

``` csharp
public partial interface IEtcdClient
{
    public AuthClient AuthClient { get; }
    public Cluster.ClusterClient ClusterClient { get; }
    public ElectionClient ElectionClient { get; }
    public KV.KVClient KVClient { get; }
    public LeaseClient LeaseClient { get; }
    public LockClient LockClient { get; }
    public MaintenanceClient MaintenanceClient { get; }
    public Watch.WatchClient WatchClient { get; }
}
```

### api doc

Main api doc please see 

[https://fs7744.github.io/etcdcsharp/api/Etcd.html](https://fs7744.github.io/etcdcsharp/api/Etcd.html)
[https://fs7744.github.io/etcdcsharp/api/Microsoft.Extensions.Configuration.EtcdConfigurationExtensions.html](https://fs7744.github.io/etcdcsharp/api/Microsoft.Extensions.Configuration.EtcdConfigurationExtensions.html)

All api doc ( include code generate by grpc tool ) please see 

[https://fs7744.github.io/etcdcsharp/api/index.html](https://fs7744.github.io/etcdcsharp/api/index.html)