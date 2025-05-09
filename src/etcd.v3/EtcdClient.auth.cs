using Etcdserverpb;
using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Etcdserverpb.Auth;

namespace Etcd;

public partial interface IEtcdClient : IDisposable
{
    public AuthClient AuthClient { get; }

    AuthenticateResponse Authenticate(AuthenticateRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    AuthEnableResponse AuthEnable(AuthEnableRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthEnableResponse> AuthEnableAsync(AuthEnableRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    AuthDisableResponse AuthDisable(AuthDisableRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthDisableResponse> AuthDisableAsync(AuthDisableRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    AuthUserAddResponse UserAdd(AuthUserAddRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthUserAddResponse> UserAddAsync(AuthUserAddRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    AuthUserGetResponse UserGet(AuthUserGetRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthUserGetResponse> UserGetAsync(AuthUserGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    AuthUserListResponse UserList(AuthUserListRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthUserListResponse> UserListAsync(AuthUserListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    AuthUserDeleteResponse UserDelete(AuthUserDeleteRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthUserDeleteResponse> UserDeleteAsync(AuthUserDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    AuthUserChangePasswordResponse UserChangePassword(AuthUserChangePasswordRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthUserChangePasswordResponse> UserChangePasswordAsync(AuthUserChangePasswordRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    AuthUserGrantRoleResponse UserGrantRole(AuthUserGrantRoleRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthUserGrantRoleResponse> UserGrantRoleAsync(AuthUserGrantRoleRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    AuthUserRevokeRoleResponse UserRevokeRole(AuthUserRevokeRoleRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthUserRevokeRoleResponse> UserRevokeRoleAsync(AuthUserRevokeRoleRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    AuthRoleAddResponse RoleAdd(AuthRoleAddRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthRoleAddResponse> RoleAddAsync(AuthRoleAddRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    AuthRoleGetResponse RoleGet(AuthRoleGetRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthRoleGetResponse> RoleGetAsync(AuthRoleGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    AuthRoleListResponse RoleList(AuthRoleListRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthRoleListResponse> RoleListAsync(AuthRoleListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    AuthRoleDeleteResponse RoleDelete(AuthRoleDeleteRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthRoleDeleteResponse> RoleDeleteAsync(AuthRoleDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    AuthRoleGrantPermissionResponse RoleGrantPermission(AuthRoleGrantPermissionRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthRoleGrantPermissionResponse> RoleDeleteAsync(AuthRoleGrantPermissionRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    AuthRoleRevokePermissionResponse RoleRevokePermission(AuthRoleRevokePermissionRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AuthRoleRevokePermissionResponse> RoleRevokePermissionAsync(AuthRoleRevokePermissionRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
}

public partial class EtcdClient : IEtcdClient
{
    private AuthClient authClient;
    public AuthClient AuthClient => authClient ??= new AuthClient(callInvoker);

    public AuthenticateResponse Authenticate(AuthenticateRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.Authenticate(request, headers, deadline);
    }

    public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.AuthenticateAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AuthEnableResponse AuthEnable(AuthEnableRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.AuthEnable(request, headers, deadline);
    }

    public async Task<AuthEnableResponse> AuthEnableAsync(AuthEnableRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.AuthEnableAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AuthDisableResponse AuthDisable(AuthDisableRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.AuthDisable(request, headers, deadline);
    }

    public async Task<AuthDisableResponse> AuthDisableAsync(AuthDisableRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.AuthDisableAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AuthUserAddResponse UserAdd(AuthUserAddRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.UserAdd(request, headers, deadline);
    }

    public async Task<AuthUserAddResponse> UserAddAsync(AuthUserAddRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.UserAddAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AuthUserGetResponse UserGet(AuthUserGetRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.UserGet(request, headers, deadline);
    }

    public async Task<AuthUserGetResponse> UserGetAsync(AuthUserGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.UserGetAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AuthUserListResponse UserList(AuthUserListRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.UserList(request, headers, deadline);
    }

    public async Task<AuthUserListResponse> UserListAsync(AuthUserListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.UserListAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AuthUserDeleteResponse UserDelete(AuthUserDeleteRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.UserDelete(request, headers, deadline);
    }

    public async Task<AuthUserDeleteResponse> UserDeleteAsync(AuthUserDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.UserDeleteAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AuthUserChangePasswordResponse UserChangePassword(AuthUserChangePasswordRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.UserChangePassword(request, headers, deadline);
    }

    public async Task<AuthUserChangePasswordResponse> UserChangePasswordAsync(AuthUserChangePasswordRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.UserChangePasswordAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AuthUserGrantRoleResponse UserGrantRole(AuthUserGrantRoleRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.UserGrantRole(request, headers, deadline);
    }

    public async Task<AuthUserGrantRoleResponse> UserGrantRoleAsync(AuthUserGrantRoleRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.UserGrantRoleAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AuthUserRevokeRoleResponse UserRevokeRole(AuthUserRevokeRoleRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.UserRevokeRole(request, headers, deadline);
    }

    public async Task<AuthUserRevokeRoleResponse> UserRevokeRoleAsync(AuthUserRevokeRoleRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.UserRevokeRoleAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AuthRoleAddResponse RoleAdd(AuthRoleAddRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.RoleAdd(request, headers, deadline);
    }

    public async Task<AuthRoleAddResponse> RoleAddAsync(AuthRoleAddRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.RoleAddAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AuthRoleGetResponse RoleGet(AuthRoleGetRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.RoleGet(request, headers, deadline);
    }

    public async Task<AuthRoleGetResponse> RoleGetAsync(AuthRoleGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.RoleGetAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AuthRoleListResponse RoleList(AuthRoleListRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.RoleList(request, headers, deadline);
    }

    public async Task<AuthRoleListResponse> RoleListAsync(AuthRoleListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.RoleListAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AuthRoleDeleteResponse RoleDelete(AuthRoleDeleteRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.RoleDelete(request, headers, deadline);
    }

    public async Task<AuthRoleDeleteResponse> RoleDeleteAsync(AuthRoleDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.RoleDeleteAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AuthRoleGrantPermissionResponse RoleGrantPermission(AuthRoleGrantPermissionRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.RoleGrantPermission(request, headers, deadline);
    }

    public async Task<AuthRoleGrantPermissionResponse> RoleDeleteAsync(AuthRoleGrantPermissionRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.RoleGrantPermissionAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AuthRoleRevokePermissionResponse RoleRevokePermission(AuthRoleRevokePermissionRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return AuthClient.RoleRevokePermission(request, headers, deadline);
    }

    public async Task<AuthRoleRevokePermissionResponse> RoleRevokePermissionAsync(AuthRoleRevokePermissionRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await AuthClient.RoleRevokePermissionAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }
}