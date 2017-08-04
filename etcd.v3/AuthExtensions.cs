using Etcdserverpb;
using Grpc.Core;

namespace ETCD.V3
{
    public static class AuthExtensions
    {
        #region Authebtucate

        public static AuthenticateRequest CreateAuthenticateRequest(this Client client, string name, string password)
        {
            return new AuthenticateRequest()
            {
                Name = name,
                Password = password
            };
        }

        public static AuthenticateResponse Authebtucate(this Client client, string name, string password)
        {
            var request = client.CreateAuthenticateRequest(name, password);
            return client.Auth.Authenticate(request, client.CallToken);
        }

        public static AsyncUnaryCall<AuthenticateResponse> AuthenticateAsync(this Client client, string name, string password)
        {
            var request = client.CreateAuthenticateRequest(name, password);
            return client.Auth.AuthenticateAsync(request, client.CallToken);
        }

        #endregion Authebtucate

        #region AuthEnable

        public static AuthEnableResponse AuthEnable(this Client client)
        {
            return client.Auth.AuthEnable(new AuthEnableRequest(), client.CallToken);
        }

        public static AsyncUnaryCall<AuthEnableResponse> AuthEnableAsync(this Client client)
        {
            return client.Auth.AuthEnableAsync(new AuthEnableRequest(), client.CallToken);
        }

        #endregion AuthEnable

        #region AuthDisable

        public static AuthDisableResponse AuthDisable(this Client client)
        {
            return client.Auth.AuthDisable(new AuthDisableRequest(), client.CallToken);
        }

        public static AsyncUnaryCall<AuthDisableResponse> AuthDisableAsync(this Client client)
        {
            return client.Auth.AuthDisableAsync(new AuthDisableRequest(), client.CallToken);
        }

        #endregion AuthDisable

        #region AuthDisable

        public static AuthUserAddRequest CreateAuthUserAddRequest(this Client client, string name, string password)
        {
            return new AuthUserAddRequest()
            {
                Name = name,
                Password = password
            };
        }

        public static AuthUserAddResponse UserAdd(this Client client, string name, string password)
        {
            var request = client.CreateAuthUserAddRequest(name, password);
            return client.Auth.UserAdd(request, client.CallToken);
        }

        public static AsyncUnaryCall<AuthUserAddResponse> UserAddAsync(this Client client, string name, string password)
        {
            var request = client.CreateAuthUserAddRequest(name, password);
            return client.Auth.UserAddAsync(request, client.CallToken);
        }

        #endregion AuthDisable

        #region UserGet

        public static AuthUserGetRequest CreateAuthUserGetRequest(this Client client, string name)
        {
            return new AuthUserGetRequest()
            {
                Name = name
            };
        }

        public static AuthUserGetResponse UserGet(this Client client, string name)
        {
            var request = client.CreateAuthUserGetRequest(name);
            return client.Auth.UserGet(request, client.CallToken);
        }

        public static AsyncUnaryCall<AuthUserGetResponse> UserGetAsync(this Client client, string name)
        {
            var request = client.CreateAuthUserGetRequest(name);
            return client.Auth.UserGetAsync(request, client.CallToken);
        }

        #endregion UserGet

        #region UserList

        public static AuthUserListResponse UserList(this Client client)
        {
            return client.Auth.UserList(new AuthUserListRequest(), client.CallToken);
        }

        public static AsyncUnaryCall<AuthUserListResponse> UserListAsync(this Client client)
        {
            return client.Auth.UserListAsync(new AuthUserListRequest(), client.CallToken);
        }

        #endregion UserList
    }
}