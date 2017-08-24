using Grpc.Core;

namespace ETCD.V3
{
    /// <summary>
    /// grpc CallInvoker to handler etcd v3 auth, base on Grpc.Core.DefaultCallInvoker
    /// </summary>
    public class AuthCallInvoker : DefaultCallInvoker
    {
        /// <summary>
        /// etcd v3 auth token
        /// </summary>
        public Metadata.Entry Token { get; set; }

        /// <summary>
        /// new instance of grpc CallInvoker to handler etcd v3 auth
        /// </summary>
        /// <param name="channel">Channel to use.</param>
        public AuthCallInvoker(Channel channel) : base(channel)
        {
        }

        /// <summary>
        /// Creates call invocation details for given method, will add etcd v3 auth token if have.
        /// </summary>
        /// <typeparam name="TRequest">Request type</typeparam>
        /// <typeparam name="TResponse">Response type</typeparam>
        /// <param name="method">A description of a remote method.</param>
        /// <param name="host">host</param>
        /// <param name="options">CallOptions</param>
        /// <returns></returns>
        protected override CallInvocationDetails<TRequest, TResponse> CreateCall<TRequest, TResponse>(Method<TRequest, TResponse> method, string host, CallOptions options)
        {
            if (Token == null)
            {
                return base.CreateCall(method, host, options);
            }
            else
            {
                var headers = options.Headers ?? new Metadata();
                headers.Add(Token);
                return base.CreateCall(method, host, options.WithHeaders(headers));
            }
        }
    }
}