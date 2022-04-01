using Grpc.Core;
using MagicOnion.Client;

public class PriariExceptionClientFilter : IClientFilter
{
    private static void AddOrReplaceHeaderValue(Metadata metadata, string key, string value)
    {
        var existing = metadata.Where(e => e.Key == key).ToArray();
        foreach (var entry in existing) metadata.Remove(entry);
        metadata.Add(key, value);
    }

    public async ValueTask<ResponseContext> SendAsync(RequestContext context, Func<RequestContext, ValueTask<ResponseContext>> next)
    {
        AddOrReplaceHeaderValue(context.CallOptions.Headers, "x-app-version", "1.0.0");
        AddOrReplaceHeaderValue(context.CallOptions.Headers, "x-app-resource-version", "10000300");
        AddOrReplaceHeaderValue(context.CallOptions.Headers, "x-app-endpoint-revision", "1");
        AddOrReplaceHeaderValue(context.CallOptions.Headers, "x-app-master-hash", "");
        AddOrReplaceHeaderValue(context.CallOptions.Headers, "x-app-unity-version", "2018.4.30f1");
        AddOrReplaceHeaderValue(context.CallOptions.Headers, "x-app-device-model", "OPPO PCRT00");
        AddOrReplaceHeaderValue(context.CallOptions.Headers, "x-app-device-os", "Android OS 5.1.1 / API-22 (LMY48Z/rel.se.infra.20200612.100533)");
        AddOrReplaceHeaderValue(context.CallOptions.Headers, "x-app-device-platform", "Android");
        return await next(context);
    }
}