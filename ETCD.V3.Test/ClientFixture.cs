using System;

namespace ETCD.V3.Test
{
    public class ClientFixture : IDisposable
    {
        public ClientFixture()
        {
            Client = new Client(TestConstants.endpoints);
        }

        public void Dispose()
        {
            Client.Close();
        }

        public Client Client { get; private set; }
    }
}