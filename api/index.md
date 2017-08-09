# ETCD V3 Client
base code all Generate by grpc tools.
## Quick start

### Install package

* Package Manager

```
Install-Package etcd.v3 -Version 0.0.1.3
```
* .NET CLI

```
dotnet add package etcd.v3 --version 0.0.1.3
```

### Simple use example

``` csharp
var client = new Client("127.0.0.1:2379");
//client.NewAuthToken("root", "123"); // if etcd has auth enable, please use user and pwd to get auth
client.Put("key", "value");  // put key/vale to etcd

// get value 
var res = client.Range("key");
Assert.Equal(1, res.Count);
Assert.Equal(1, res.Kvs.Count);
Assert.Equal("value", res.Kvs[0].Value.ToStringUtf8());
Assert.False(res.More);
```

### api doc

Main api doc please see 

[https://fs7744.github.io/etcdcsharp/api/ETCD.V3.html](https://fs7744.github.io/etcdcsharp/api/ETCD.V3.html)

All api doc ( include code generate by grpc tool ) please see 

[https://fs7744.github.io/etcdcsharp/api/index.html](https://fs7744.github.io/etcdcsharp/api/index.html)