using Authpb;
using Etcdserverpb;
using Google.Protobuf;
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
            return client.Auth.Authenticate(request, client.AuthToken);
        }

        public static AsyncUnaryCall<AuthenticateResponse> AuthenticateAsync(this Client client, string name, string password)
        {
            var request = client.CreateAuthenticateRequest(name, password);
            return client.Auth.AuthenticateAsync(request, client.AuthToken);
        }

        #endregion Authebtucate

        #region AuthEnable

        public static AuthEnableResponse AuthEnable(this Client client)
        {
            return client.Auth.AuthEnable(new AuthEnableRequest(), client.AuthToken);
        }

        public static AsyncUnaryCall<AuthEnableResponse> AuthEnableAsync(this Client client)
        {
            return client.Auth.AuthEnableAsync(new AuthEnableRequest(), client.AuthToken);
        }

        #endregion AuthEnable

        #region AuthDisable

        public static AuthDisableResponse AuthDisable(this Client client)
        {
            return client.Auth.AuthDisable(new AuthDisableRequest(), client.AuthToken);
        }

        public static AsyncUnaryCall<AuthDisableResponse> AuthDisableAsync(this Client client)
        {
            return client.Auth.AuthDisableAsync(new AuthDisableRequest(), client.AuthToken);
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
            return client.Auth.UserAdd(request, client.AuthToken);
        }

        public static AsyncUnaryCall<AuthUserAddResponse> UserAddAsync(this Client client, string name, string password)
        {
            var request = client.CreateAuthUserAddRequest(name, password);
            return client.Auth.UserAddAsync(request, client.AuthToken);
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
            return client.Auth.UserGet(request, client.AuthToken);
        }

        public static AsyncUnaryCall<AuthUserGetResponse> UserGetAsync(this Client client, string name)
        {
            var request = client.CreateAuthUserGetRequest(name);
            return client.Auth.UserGetAsync(request, client.AuthToken);
        }

        #endregion UserGet

        #region UserList

        public static AuthUserListResponse UserList(this Client client)
        {
            return client.Auth.UserList(new AuthUserListRequest(), client.AuthToken);
        }

        public static AsyncUnaryCall<AuthUserListResponse> UserListAsync(this Client client)
        {
            return client.Auth.UserListAsync(new AuthUserListRequest(), client.AuthToken);
        }

        #endregion UserList

        #region UserDelete

        public static AuthUserDeleteRequest CreateAuthUserDeleteRequest(this Client client, string name)
        {
            return new AuthUserDeleteRequest()
            {
                Name = name
            };
        }

        public static AuthUserDeleteResponse UserDelete(this Client client, string name)
        {
            var request = client.CreateAuthUserDeleteRequest(name);
            return client.Auth.UserDelete(request, client.AuthToken);
        }

        public static AsyncUnaryCall<AuthUserDeleteResponse> UserDeleteAsync(this Client client, string name)
        {
            var request = client.CreateAuthUserDeleteRequest(name);
            return client.Auth.UserDeleteAsync(request, client.AuthToken);
        }

        #endregion UserDelete

        #region UserChangePassword

        public static AuthUserChangePasswordRequest CreateAuthUserChangePasswordRequest(this Client client,
            string name, string password)
        {
            return new AuthUserChangePasswordRequest()
            {
                Name = name,
                Password = password
            };
        }

        public static AuthUserChangePasswordResponse UserChangePassword(this Client client, string name, string password)
        {
            var request = client.CreateAuthUserChangePasswordRequest(name, password);
            return client.Auth.UserChangePassword(request, client.AuthToken);
        }

        public static AsyncUnaryCall<AuthUserChangePasswordResponse> UserChangePasswordAsync(
            this Client client, string name, string password)
        {
            var request = client.CreateAuthUserChangePasswordRequest(name, password);
            return client.Auth.UserChangePasswordAsync(request, client.AuthToken);
        }

        #endregion UserChangePassword

        #region UserGrantRole

        public static AuthUserGrantRoleRequest CreateUserGrantRoleRequest(this Client client,
            string user, string role)
        {
            return new AuthUserGrantRoleRequest()
            {
                User = user,
                Role = role
            };
        }

        public static AuthUserGrantRoleResponse UserGrantRole(this Client client,
            string user, string role)
        {
            var request = client.CreateUserGrantRoleRequest(user, role);
            return client.Auth.UserGrantRole(request, client.AuthToken);
        }

        public static AsyncUnaryCall<AuthUserGrantRoleResponse> UserGrantRoleAsync(
            this Client client, string user, string role)
        {
            var request = client.CreateUserGrantRoleRequest(user, role);
            return client.Auth.UserGrantRoleAsync(request, client.AuthToken);
        }

        #endregion UserGrantRole

        #region UserRevokeRole

        public static AuthUserRevokeRoleRequest CreateUserRevokeRoleRequest(this Client client,
            string user, string role)
        {
            return new AuthUserRevokeRoleRequest()
            {
                Name = user,
                Role = role
            };
        }

        public static AuthUserRevokeRoleResponse UserRevokeRole(this Client client,
            string user, string role)
        {
            var request = client.CreateUserRevokeRoleRequest(user, role);
            return client.Auth.UserRevokeRole(request, client.AuthToken);
        }

        public static AsyncUnaryCall<AuthUserRevokeRoleResponse> UserRevokeRoleAsync(
            this Client client, string user, string role)
        {
            var request = client.CreateUserRevokeRoleRequest(user, role);
            return client.Auth.UserRevokeRoleAsync(request, client.AuthToken);
        }

        #endregion UserRevokeRole

        #region RoleAdd

        public static AuthRoleAddRequest CreateRoleAddRequest(this Client client, string role)
        {
            return new AuthRoleAddRequest()
            {
                Name = role
            };
        }

        public static AuthRoleAddResponse RoleAdd(this Client client, string role)
        {
            var request = client.CreateRoleAddRequest(role);
            return client.Auth.RoleAdd(request, client.AuthToken);
        }

        public static AsyncUnaryCall<AuthRoleAddResponse> RoleAddAsync(
            this Client client, string role)
        {
            var request = client.CreateRoleAddRequest(role);
            return client.Auth.RoleAddAsync(request, client.AuthToken);
        }

        #endregion RoleAdd

        #region RoleGet

        public static AuthRoleGetRequest CreateRoleGetRequest(this Client client, string role)
        {
            return new AuthRoleGetRequest()
            {
                Role = role
            };
        }

        public static AuthRoleGetResponse RoleGet(this Client client, string role)
        {
            var request = client.CreateRoleGetRequest(role);
            return client.Auth.RoleGet(request, client.AuthToken);
        }

        public static AsyncUnaryCall<AuthRoleGetResponse> RoleGetAsync(
            this Client client, string role)
        {
            var request = client.CreateRoleGetRequest(role);
            return client.Auth.RoleGetAsync(request, client.AuthToken);
        }

        #endregion RoleGet

        #region RoleList

        public static AuthRoleListResponse RoleList(this Client client)
        {
            return client.Auth.RoleList(new AuthRoleListRequest(), client.AuthToken);
        }

        public static AsyncUnaryCall<AuthRoleListResponse> RoleListAsync(this Client client)
        {
            return client.Auth.RoleListAsync(new AuthRoleListRequest(), client.AuthToken);
        }

        #endregion RoleList

        #region RoleDelete

        public static AuthRoleDeleteRequest CreateRoleDeleteRequest(this Client client, string role)
        {
            return new AuthRoleDeleteRequest()
            {
                Role = role
            };
        }

        public static AuthRoleDeleteResponse RoleDelete(this Client client, string role)
        {
            var request = client.CreateRoleDeleteRequest(role);
            return client.Auth.RoleDelete(request, client.AuthToken);
        }

        public static AsyncUnaryCall<AuthRoleDeleteResponse> RoleDeleteAsync(
            this Client client, string role)
        {
            var request = client.CreateRoleDeleteRequest(role);
            return client.Auth.RoleDeleteAsync(request, client.AuthToken);
        }

        #endregion RoleDelete

        #region RoleGrantPermission

        public static AuthRoleGrantPermissionRequest CreateRoleGrantPermissionRequest(this Client client,
            string name, ByteString key, ByteString rangeEnd, Permission.Types.Type permType = Permission.Types.Type.Read)
        {
            return new AuthRoleGrantPermissionRequest()
            {
                Name = name,
                Perm = new Permission()
                {
                    Key = key,
                    RangeEnd = rangeEnd ?? key.ToPrefixEnd(),
                    PermType = permType
                }
            };
        }

        public static AuthRoleGrantPermissionResponse RoleGrantPermission(this Client client,
            string name, ByteString key, ByteString rangeEnd, Permission.Types.Type permType = Permission.Types.Type.Read)
        {
            var request = client.CreateRoleGrantPermissionRequest(name, key, rangeEnd, permType);
            return client.Auth.RoleGrantPermission(request, client.AuthToken);
        }

        public static AuthRoleGrantPermissionResponse RoleGrantPermission(this Client client,
            string name, string key, string rangeEnd, Permission.Types.Type permType = Permission.Types.Type.Read)
        {
            return client.RoleGrantPermission(name, ByteString.CopyFromUtf8(key),
                rangeEnd == null ? null : ByteString.CopyFromUtf8(rangeEnd), permType);
        }

        public static AsyncUnaryCall<AuthRoleGrantPermissionResponse> RoleGrantPermissionAsync(
            this Client client, string name, ByteString key, ByteString rangeEnd, Permission.Types.Type permType = Permission.Types.Type.Read)
        {
            var request = client.CreateRoleGrantPermissionRequest(name, key, rangeEnd, permType);
            return client.Auth.RoleGrantPermissionAsync(request, client.AuthToken);
        }

        public static AsyncUnaryCall<AuthRoleGrantPermissionResponse> RoleGrantPermissionAsync(this Client client,
            string name, string key, string rangeEnd, Permission.Types.Type permType = Permission.Types.Type.Read)
        {
            return client.RoleGrantPermissionAsync(name, ByteString.CopyFromUtf8(key),
                rangeEnd == null ? null : ByteString.CopyFromUtf8(rangeEnd), permType);
        }

        #endregion RoleGrantPermission

        #region RoleRevokePermission

        public static AuthRoleRevokePermissionRequest CreateRoleRevokePermissionRequest(this Client client,
            string role, string key, string rangeEnd)
        {
            return new AuthRoleRevokePermissionRequest()
            {
                Role = role,
                Key = key,
                RangeEnd = rangeEnd ?? ByteString.CopyFromUtf8(key).ToPrefixEnd().ToString()
            };
        }

        public static AuthRoleRevokePermissionResponse RoleRevokePermission(this Client client,
            string role, string key, string rangeEnd)
        {
            var request = client.CreateRoleRevokePermissionRequest(role, key, rangeEnd);
            return client.Auth.RoleRevokePermission(request, client.AuthToken);
        }

        public static AsyncUnaryCall<AuthRoleRevokePermissionResponse> RoleRevokePermissionAsync(
            this Client client, string role, string key, string rangeEnd)
        {
            var request = client.CreateRoleRevokePermissionRequest(role, key, rangeEnd);
            return client.Auth.RoleRevokePermissionAsync(request, client.AuthToken);
        }

        #endregion RoleRevokePermission
    }
}