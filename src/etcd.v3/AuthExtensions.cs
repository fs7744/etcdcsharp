using Authpb;
using Etcdserverpb;
using Google.Protobuf;
using Grpc.Core;

namespace ETCD.V3
{
    public static class AuthExtensions
    {
        #region Authebtucate

        /// <summary>
        /// Authenticate processes an authenticate request.
        /// </summary>
        /// <param name="name">User Name.</param>
        /// <param name="password">User Password.</param>
        /// <returns>The response received from the server.</returns>
        public static AuthenticateRequest CreateAuthenticateRequest(this Client client, string name, string password)
        {
            return new AuthenticateRequest()
            {
                Name = name,
                Password = password
            };
        }

        /// <summary>
        /// Authenticate processes an authenticate request.
        /// </summary>
        /// <param name="name">User Name.</param>
        /// <param name="password">User Password.</param>
        /// <returns>The response received from the server.</returns>
        public static AuthenticateResponse Authebtucate(this Client client, string name, string password)
        {
            var request = client.CreateAuthenticateRequest(name, password);
            return client.Auth.Authenticate(request);
        }

        /// <summary>
        /// Authenticate processes an authenticate request.
        /// </summary>
        /// <param name="name">User Name.</param>
        /// <param name="password">User Password.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthenticateResponse> AuthenticateAsync(this Client client, string name, string password)
        {
            var request = client.CreateAuthenticateRequest(name, password);
            return client.Auth.AuthenticateAsync(request);
        }

        #endregion Authebtucate

        #region AuthEnable

        /// <summary>
        /// AuthEnable enables authentication.
        /// </summary>
        /// <returns>The response received from the server.</returns>
        public static AuthEnableResponse AuthEnable(this Client client)
        {
            return client.Auth.AuthEnable(new AuthEnableRequest());
        }

        /// <summary>
        /// AuthEnable enables authentication.
        /// </summary>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthEnableResponse> AuthEnableAsync(this Client client)
        {
            return client.Auth.AuthEnableAsync(new AuthEnableRequest());
        }

        #endregion AuthEnable

        #region AuthDisable

        /// <summary>
        /// AuthDisable disables authentication.
        /// </summary>
        /// <returns>The response received from the server.</returns>
        public static AuthDisableResponse AuthDisable(this Client client)
        {
            return client.Auth.AuthDisable(new AuthDisableRequest());
        }

        /// <summary>
        /// AuthDisable disables authentication.
        /// </summary>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthDisableResponse> AuthDisableAsync(this Client client)
        {
            return client.Auth.AuthDisableAsync(new AuthDisableRequest());
        }

        #endregion AuthDisable

        #region AuthDisable

        /// <summary>
        /// Create AuthUserAddRequest.
        /// </summary>
        /// <param name="name">User Name.</param>
        /// <param name="password">User Password.</param>
        /// <returns>AuthUserAddRequest.</returns>
        public static AuthUserAddRequest CreateAuthUserAddRequest(this Client client, string name, string password)
        {
            return new AuthUserAddRequest()
            {
                Name = name,
                Password = password
            };
        }

        /// <summary>
        /// UserAdd adds a new user.
        /// </summary>
        /// <param name="name">User Name.</param>
        /// <param name="password">User Password.</param>
        /// <returns>The response received from the server.</returns>
        public static AuthUserAddResponse UserAdd(this Client client, string name, string password)
        {
            var request = client.CreateAuthUserAddRequest(name, password);
            return client.Auth.UserAdd(request);
        }

        /// <summary>
        /// UserAdd adds a new user.
        /// </summary>
        /// <param name="name">User Name.</param>
        /// <param name="password">User Password.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthUserAddResponse> UserAddAsync(this Client client, string name, string password)
        {
            var request = client.CreateAuthUserAddRequest(name, password);
            return client.Auth.UserAddAsync(request);
        }

        #endregion AuthDisable

        #region UserGet
        
        /// <summary>
        /// Create AuthUserGetRequest.
        /// </summary>
        /// <param name="name">User Name.</param>
        /// <returns>AuthUserGetRequest.</returns>
        public static AuthUserGetRequest CreateAuthUserGetRequest(this Client client, string name)
        {
            return new AuthUserGetRequest()
            {
                Name = name
            };
        }

        /// <summary>
        /// UserGet gets detailed user information.
        /// </summary>
        /// <param name="name">User Name.</param>
        /// <returns>The response received from the server.</returns>
        public static AuthUserGetResponse UserGet(this Client client, string name)
        {
            var request = client.CreateAuthUserGetRequest(name);
            return client.Auth.UserGet(request);
        }

        /// <summary>
        /// UserGet gets detailed user information.
        /// </summary>
        /// <param name="name">User Name.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthUserGetResponse> UserGetAsync(this Client client, string name)
        {
            var request = client.CreateAuthUserGetRequest(name);
            return client.Auth.UserGetAsync(request);
        }

        #endregion UserGet

        #region UserList

        /// <summary>
        /// UserList gets a list of all users.
        /// </summary>
        /// <returns>The response received from the server.</returns>
        public static AuthUserListResponse UserList(this Client client)
        {
            return client.Auth.UserList(new AuthUserListRequest());
        }

        /// <summary>
        /// UserList gets a list of all users.
        /// </summary>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthUserListResponse> UserListAsync(this Client client)
        {
            return client.Auth.UserListAsync(new AuthUserListRequest());
        }

        #endregion UserList

        #region UserDelete

        /// <summary>
        /// Create AuthUserDeleteRequest.
        /// </summary>
        /// <param name="name">User Name.</param>
        /// <returns>AuthUserDeleteRequest.</returns>
        public static AuthUserDeleteRequest CreateAuthUserDeleteRequest(this Client client, string name)
        {
            return new AuthUserDeleteRequest()
            {
                Name = name
            };
        }

        /// <summary>
        /// UserDelete deletes a specified user.
        /// </summary>
        /// <param name="name">User Name.</param>
        /// <returns>The response received from the server.</returns>
        public static AuthUserDeleteResponse UserDelete(this Client client, string name)
        {
            var request = client.CreateAuthUserDeleteRequest(name);
            return client.Auth.UserDelete(request);
        }

        /// <summary>
        /// UserDelete deletes a specified user.
        /// </summary>
        /// <param name="name">User Name.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthUserDeleteResponse> UserDeleteAsync(this Client client, string name)
        {
            var request = client.CreateAuthUserDeleteRequest(name);
            return client.Auth.UserDeleteAsync(request);
        }

        #endregion UserDelete

        #region UserChangePassword

        /// <summary>
        /// Create AuthUserChangePasswordRequest
        /// </summary>
        /// <param name="name">User Name.</param>
        /// <param name="password">User Password.</param>
        /// <returns>AuthUserChangePasswordRequest.</returns>
        public static AuthUserChangePasswordRequest CreateAuthUserChangePasswordRequest(this Client client,
            string name, string password)
        {
            return new AuthUserChangePasswordRequest()
            {
                Name = name,
                Password = password
            };
        }

        /// <summary>
        /// UserChangePassword changes the password of a specified user.
        /// </summary>
        /// <param name="name">User Name.</param>
        /// <param name="password">User Password.</param>
        /// <returns>The response received from the server.</returns>
        public static AuthUserChangePasswordResponse UserChangePassword(this Client client, string name, string password)
        {
            var request = client.CreateAuthUserChangePasswordRequest(name, password);
            return client.Auth.UserChangePassword(request);
        }

        /// <summary>
        /// UserChangePassword changes the password of a specified user.
        /// </summary>
        /// <param name="name">User Name.</param>
        /// <param name="password">User Password.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthUserChangePasswordResponse> UserChangePasswordAsync(
            this Client client, string name, string password)
        {
            var request = client.CreateAuthUserChangePasswordRequest(name, password);
            return client.Auth.UserChangePasswordAsync(request);
        }

        #endregion UserChangePassword

        #region UserGrantRole

        /// <summary>
        /// Create UserGrantRoleRequest.
        /// </summary>
        /// <param name="user">user is the name of the user which should be granted a given role.</param>
        /// <param name="role">role is the name of the role to grant to the user.</param>
        /// <returns>AuthUserGrantRoleRequest.</returns>
        public static AuthUserGrantRoleRequest CreateUserGrantRoleRequest(this Client client,
            string user, string role)
        {
            return new AuthUserGrantRoleRequest()
            {
                User = user,
                Role = role
            };
        }

        /// <summary>
        /// UserGrant grants a role to a specified user.
        /// </summary>
        /// <param name="user">user is the name of the user which should be granted a given role.</param>
        /// <param name="role">role is the name of the role to grant to the user.</param>
        /// <returns>The response received from the server.</returns>
        public static AuthUserGrantRoleResponse UserGrantRole(this Client client,
            string user, string role)
        {
            var request = client.CreateUserGrantRoleRequest(user, role);
            return client.Auth.UserGrantRole(request);
        }

        /// <summary>
        /// UserGrant grants a role to a specified user.
        /// </summary>
        /// <param name="user">user is the name of the user which should be granted a given role.</param>
        /// <param name="role">role is the name of the role to grant to the user.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthUserGrantRoleResponse> UserGrantRoleAsync(
            this Client client, string user, string role)
        {
            var request = client.CreateUserGrantRoleRequest(user, role);
            return client.Auth.UserGrantRoleAsync(request);
        }

        #endregion UserGrantRole

        #region UserRevokeRole

        /// <summary>
        /// Create UserRevokeRoleRequest.
        /// </summary>
        /// <param name="user">user is the name of the user which should be Revoked a given role.</param>
        /// <param name="role">role is the name of the role to Revoked to the user.</param>
        /// <returns>AuthUserRevokeRoleRequest.</returns>
        public static AuthUserRevokeRoleRequest CreateUserRevokeRoleRequest(this Client client,
            string user, string role)
        {
            return new AuthUserRevokeRoleRequest()
            {
                Name = user,
                Role = role
            };
        }

        /// <summary>
        /// UserRevokeRole revokes a role of specified user.
        /// </summary>
        /// <param name="user">user is the name of the user which should be Revoked a given role.</param>
        /// <param name="role">role is the name of the role to Revoked to the user.</param>
        /// <returns>The response received from the server.</returns>
        public static AuthUserRevokeRoleResponse UserRevokeRole(this Client client,
            string user, string role)
        {
            var request = client.CreateUserRevokeRoleRequest(user, role);
            return client.Auth.UserRevokeRole(request);
        }

        /// <summary>
        /// UserRevokeRole revokes a role of specified user.
        /// </summary>
        /// <param name="user">user is the name of the user which should be Revoked a given role.</param>
        /// <param name="role">role is the name of the role to Revoked to the user.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthUserRevokeRoleResponse> UserRevokeRoleAsync(
            this Client client, string user, string role)
        {
            var request = client.CreateUserRevokeRoleRequest(user, role);
            return client.Auth.UserRevokeRoleAsync(request);
        }

        #endregion UserRevokeRole

        #region RoleAdd

        /// <summary>
        /// Create RoleGetRequest.
        /// </summary>
        /// <param name="role">name is the name of the role to add to the authentication system.</param>
        /// <returns>AuthRoleGetRequest.</returns>
        public static AuthRoleAddRequest CreateRoleAddRequest(this Client client, string role)
        {
            return new AuthRoleAddRequest()
            {
                Name = role
            };
        }

        /// <summary>
        /// RoleAdd adds a new role.
        /// </summary>
        /// <param name="role">name is the name of the role to add to the authentication system.</param>
        /// <returns>The response received from the server.</returns>
        public static AuthRoleAddResponse RoleAdd(this Client client, string role)
        {
            var request = client.CreateRoleAddRequest(role);
            return client.Auth.RoleAdd(request);
        }

        /// <summary>
        /// RoleAdd adds a new role.
        /// </summary>
        /// <param name="role">name is the name of the role to add to the authentication system.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthRoleAddResponse> RoleAddAsync(
            this Client client, string role)
        {
            var request = client.CreateRoleAddRequest(role);
            return client.Auth.RoleAddAsync(request);
        }

        #endregion RoleAdd

        #region RoleGet

        /// <summary>
        /// Create RoleGetRequest.
        /// </summary>
        /// <param name="role">The role name.</param>
        /// <returns>AuthRoleGetRequest.</returns>
        public static AuthRoleGetRequest CreateRoleGetRequest(this Client client, string role)
        {
            return new AuthRoleGetRequest()
            {
                Role = role
            };
        }

        /// <summary>
        /// RoleGet gets detailed role information.
        /// </summary>
        /// <param name="role">The role name.</param>
        /// <returns>The response received from the server.</returns>
        public static AuthRoleGetResponse RoleGet(this Client client, string role)
        {
            var request = client.CreateRoleGetRequest(role);
            return client.Auth.RoleGet(request);
        }

        /// <summary>
        /// RoleGet gets detailed role information.
        /// </summary>
        /// <param name="role">The role name.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthRoleGetResponse> RoleGetAsync(
            this Client client, string role)
        {
            var request = client.CreateRoleGetRequest(role);
            return client.Auth.RoleGetAsync(request);
        }

        #endregion RoleGet

        #region RoleList

        /// <summary>
        /// RoleList gets lists of all roles.
        /// </summary>
        /// <returns>The response received from the server.</returns>
        public static AuthRoleListResponse RoleList(this Client client)
        {
            return client.Auth.RoleList(new AuthRoleListRequest());
        }

        /// <summary>
        /// RoleList gets lists of all roles.
        /// </summary>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthRoleListResponse> RoleListAsync(this Client client)
        {
            return client.Auth.RoleListAsync(new AuthRoleListRequest());
        }

        #endregion RoleList

        #region RoleDelete

        /// <summary>
        /// Create RoleDeleteRequest.
        /// </summary>
        /// <param name="role">The role name.</param>
        /// <returns>AuthRoleDeleteRequest.</returns>
        public static AuthRoleDeleteRequest CreateRoleDeleteRequest(this Client client, string role)
        {
            return new AuthRoleDeleteRequest()
            {
                Role = role
            };
        }

        /// <summary>
        /// RoleDelete deletes a specified role.
        /// </summary>
        /// <param name="role">The role name.</param>
        /// <returns>The response received from the server.</returns>
        public static AuthRoleDeleteResponse RoleDelete(this Client client, string role)
        {
            var request = client.CreateRoleDeleteRequest(role);
            return client.Auth.RoleDelete(request);
        }

        /// <summary>
        /// RoleDelete deletes a specified role.
        /// </summary>
        /// <param name="role">The role name.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthRoleDeleteResponse> RoleDeleteAsync(
            this Client client, string role)
        {
            var request = client.CreateRoleDeleteRequest(role);
            return client.Auth.RoleDeleteAsync(request);
        }

        #endregion RoleDelete

        #region RoleGrantPermission

        /// <summary>
        /// Create RoleGrantPermissionRequest.
        /// </summary>
        /// <param name="name">name is the name of the role which will be granted the permission.</param>
        /// <param name="key">key is the first key to RoleGrantPermission.</param>
        /// <param name="rangeEnd">range_end is the key following the last key to delete for the range [key, range_end).
        /// If range_end is not given, the range is defined to contain only the key argument.
        /// If range_end is one bit larger than the given key, then the range is all the keys
        /// with the prefix (the given key).
        /// If range_end is '\0', the range is all keys greater than or equal to the key argument.</param>
        /// <param name="permType">Permission message type.</param>
        /// <returns>AuthRoleGrantPermissionRequest.</returns>
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

        /// <summary>
        /// RoleGrantPermission grants a permission of a specified key or range to a specified role.
        /// </summary>
        /// <param name="name">name is the name of the role which will be granted the permission.</param>
        /// <param name="key">key is the first key to RoleGrantPermission.</param>
        /// <param name="rangeEnd">range_end is the key following the last key to delete for the range [key, range_end).
        /// If range_end is not given, the range is defined to contain only the key argument.
        /// If range_end is one bit larger than the given key, then the range is all the keys
        /// with the prefix (the given key).
        /// If range_end is '\0', the range is all keys greater than or equal to the key argument.</param>
        /// <param name="permType">Permission message type.</param>
        /// <returns>The response received from the server.</returns>
        public static AuthRoleGrantPermissionResponse RoleGrantPermission(this Client client,
            string name, ByteString key, ByteString rangeEnd, Permission.Types.Type permType = Permission.Types.Type.Read)
        {
            var request = client.CreateRoleGrantPermissionRequest(name, key, rangeEnd, permType);
            return client.Auth.RoleGrantPermission(request);
        }

        /// <summary>
        /// RoleGrantPermission grants a permission of a specified key or range to a specified role.
        /// </summary>
        /// <param name="name">name is the name of the role which will be granted the permission.</param>
        /// <param name="key">key is the first key to RoleGrantPermission.</param>
        /// <param name="rangeEnd">range_end is the key following the last key to delete for the range [key, range_end).
        /// If range_end is not given, the range is defined to contain only the key argument.
        /// If range_end is one bit larger than the given key, then the range is all the keys
        /// with the prefix (the given key).
        /// If range_end is '\0', the range is all keys greater than or equal to the key argument.</param>
        /// <param name="permType">Permission message type.</param>
        /// <returns>The response received from the server.</returns>
        public static AuthRoleGrantPermissionResponse RoleGrantPermission(this Client client,
            string name, string key, string rangeEnd, Permission.Types.Type permType = Permission.Types.Type.Read)
        {
            return client.RoleGrantPermission(name, ByteString.CopyFromUtf8(key),
                rangeEnd == null ? null : ByteString.CopyFromUtf8(rangeEnd), permType);
        }

        /// <summary>
        /// RoleGrantPermission grants a permission of a specified key or range to a specified role.
        /// </summary>
        /// <param name="name">name is the name of the role which will be granted the permission.</param>
        /// <param name="key">key is the first key to RoleGrantPermission.</param>
        /// <param name="rangeEnd">range_end is the key following the last key to delete for the range [key, range_end).
        /// If range_end is not given, the range is defined to contain only the key argument.
        /// If range_end is one bit larger than the given key, then the range is all the keys
        /// with the prefix (the given key).
        /// If range_end is '\0', the range is all keys greater than or equal to the key argument.</param>
        /// <param name="permType">Permission message type.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthRoleGrantPermissionResponse> RoleGrantPermissionAsync(
            this Client client, string name, ByteString key, ByteString rangeEnd, Permission.Types.Type permType = Permission.Types.Type.Read)
        {
            var request = client.CreateRoleGrantPermissionRequest(name, key, rangeEnd, permType);
            return client.Auth.RoleGrantPermissionAsync(request);
        }

        /// <summary>
        /// RoleGrantPermission grants a permission of a specified key or range to a specified role.
        /// </summary>
        /// <param name="name">name is the name of the role which will be granted the permission.</param>
        /// <param name="key">key is the first key to RoleGrantPermission.</param>
        /// <param name="rangeEnd">range_end is the key following the last key to delete for the range [key, range_end).
        /// If range_end is not given, the range is defined to contain only the key argument.
        /// If range_end is one bit larger than the given key, then the range is all the keys
        /// with the prefix (the given key).
        /// If range_end is '\0', the range is all keys greater than or equal to the key argument.</param>
        /// <param name="permType">Permission message type.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthRoleGrantPermissionResponse> RoleGrantPermissionAsync(this Client client,
            string name, string key, string rangeEnd, Permission.Types.Type permType = Permission.Types.Type.Read)
        {
            return client.RoleGrantPermissionAsync(name, ByteString.CopyFromUtf8(key),
                rangeEnd == null ? null : ByteString.CopyFromUtf8(rangeEnd), permType);
        }

        #endregion RoleGrantPermission

        #region RoleRevokePermission

        /// <summary>
        /// Create RoleRevokePermissionRequest.
        /// </summary>
        /// <param name="role">The role name.</param>
        /// <param name="key">key is the first key to RoleGrantPermission.</param>
        /// <param name="rangeEnd">range_end is the key following the last key to delete for the range [key, range_end).
        /// If range_end is not given, the range is defined to contain only the key argument.
        /// If range_end is one bit larger than the given key, then the range is all the keys
        /// with the prefix (the given key).
        /// If range_end is '\0', the range is all keys greater than or equal to the key argument.</param>
        /// <returns>AuthRoleRevokePermissionRequest.</returns>
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

        /// <summary>
        /// RoleRevokePermission revokes a key or range permission of a specified role.
        /// </summary>
        /// <param name="role">The role name.</param>
        /// <param name="key">key is the first key to RoleGrantPermission.</param>
        /// <param name="rangeEnd">range_end is the key following the last key to delete for the range [key, range_end).
        /// If range_end is not given, the range is defined to contain only the key argument.
        /// If range_end is one bit larger than the given key, then the range is all the keys
        /// with the prefix (the given key).
        /// If range_end is '\0', the range is all keys greater than or equal to the key argument.</param>
        /// <returns>The response received from the server.</returns>
        public static AuthRoleRevokePermissionResponse RoleRevokePermission(this Client client,
            string role, string key, string rangeEnd)
        {
            var request = client.CreateRoleRevokePermissionRequest(role, key, rangeEnd);
            return client.Auth.RoleRevokePermission(request);
        }

        /// <summary>
        /// RoleRevokePermission revokes a key or range permission of a specified role.
        /// </summary>
        /// <param name="role">The role name.</param>
        /// <param name="key">key is the first key to RoleGrantPermission.</param>
        /// <param name="rangeEnd">range_end is the key following the last key to delete for the range [key, range_end).
        /// If range_end is not given, the range is defined to contain only the key argument.
        /// If range_end is one bit larger than the given key, then the range is all the keys
        /// with the prefix (the given key).
        /// If range_end is '\0', the range is all keys greater than or equal to the key argument.</param>
        /// <returns>The call object.</returns>
        public static AsyncUnaryCall<AuthRoleRevokePermissionResponse> RoleRevokePermissionAsync(
            this Client client, string role, string key, string rangeEnd)
        {
            var request = client.CreateRoleRevokePermissionRequest(role, key, rangeEnd);
            return client.Auth.RoleRevokePermissionAsync(request);
        }

        #endregion RoleRevokePermission
    }
}