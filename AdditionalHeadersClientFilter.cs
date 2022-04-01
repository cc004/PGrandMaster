using MagicOnion.Client;

public class AdditionalHeadersClientFilter : IClientFilter
{
    public async ValueTask<ResponseContext> SendAsync(RequestContext context, Func<RequestContext, ValueTask<ResponseContext>> next)
    {
        return await next(context);
    }
}