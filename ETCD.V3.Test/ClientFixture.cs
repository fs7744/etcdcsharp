using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ETCD.V3.Test
{
    public class ClientFixture : IDisposable
    {
        public ClientFixture()
        {
            Client = new Client(TestConstants.endpoints, "test", "testpwd");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Client Client { get; private set; }
    }
}
