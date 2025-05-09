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

#### KV

##### get one by key

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
```


### api doc

Main api doc please see 

[https://fs7744.github.io/etcdcsharp/api/Etcd.html](https://fs7744.github.io/etcdcsharp/api/Etcd.html)
[https://fs7744.github.io/etcdcsharp/api/Etcd.Configuration.html](https://fs7744.github.io/etcdcsharp/api/Etcd.Configuration.html)

All api doc ( include code generate by grpc tool ) please see 

[https://fs7744.github.io/etcdcsharp/api/index.html](https://fs7744.github.io/etcdcsharp/api/index.html)