// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using MessagePack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Grpc.Core;
using Grpc.Net.Client;
using MagicOnion;
using MagicOnion.Client;
using MessagePack;
using Priari.ServerShared.Services;


var endpointApi = "https://api-cmxegy.prd.app.priconne-grandmasters.jp";
var uri = new Uri(endpointApi);
var channel = GrpcChannel.ForAddress(uri);
var filters = new IClientFilter[2];
filters[0] = new PriariExceptionClientFilter();
filters[1] = new AdditionalHeadersClientFilter();
var client = MagicOnionClient.Create<IUserAccountService>(channel, filters);

var result = await client.GetMasterDataAsync("-");

var file = result.Result.Data;

static string ToJson(byte[] file)
{
    var obj = MessagePackSerializer.Deserialize<Dictionary<string, Tuple<int, int>>>(
        file, out var read);

    var result0 = new JObject();

    void Deserialize<T>(string name)
    {
        var (start, len) = obj[name];
        var data = file[(read + start)..(read + start + len)];
        result0.Add(name, JToken.FromObject(MessagePackSerializer.Deserialize<T[]>(data, MessagePackSerializerOptions.Standard
            .WithCompression(MessagePackCompression.Lz4Block))));
    }

    foreach (var key in obj.Keys)
        Deserialize<object>(key);

    return result0.ToString(Formatting.Indented);
}

File.WriteAllText("master.json", ToJson(file));

static string ManifestToJson(byte[] file)
{
    var obj = MessagePackSerializer.Deserialize<Dictionary<string, Tuple<int, int>>>(
        file, out var read);

    var result0 = new JObject();

    void Deserialize<T>(string name)
    {
        var (start, len) = obj[name];
        var data = file[(read + start)..(read + start + len)];
        result0.Add(name, JToken.FromObject(MessagePackSerializer.Deserialize<T[]>(data, MessagePackSerializerOptions.Standard
            .WithCompression(MessagePackCompression.Lz4Block))));
    }

    Deserialize<AssetBundleLoadName>("assetname");
    Deserialize<ManifestAsset>("asset");
    Deserialize<ManifestRawAsset>("raw_asset");

    return result0.ToString(Formatting.Indented);
}

var hc = new HttpClient();
var manifest0 =
    await hc.GetByteArrayAsync(
        "https://prd-priconne-grandmasters-hq6jkeih.akamaized.net/10001300/Jpn/Android/manifests/assetbundle.manifest");

File.WriteAllText("manifest.json", ManifestToJson(manifest0));

var manifest = JObject.Parse(File.ReadAllText("manifest.json"));

manifest["asset"].ToObject<ManifestAsset[]>().Select(a => new
         {
             a.Name, a.Hash
         }).Concat(manifest["raw_asset"].ToObject<ManifestRawAsset[]>().Select(a => new
         {
             a.Name, a.Hash
         })).AsParallel().WithDegreeOfParallelism(32).ForAll(asset =>
{
    if (File.Exists(asset.Name)) return;
        Directory.CreateDirectory(Path.GetDirectoryName("./" + asset.Name));
    File.WriteAllBytes(Directory.Exists(asset.Name) ? asset.Name + ".asset" : asset.Name,
        hc.GetByteArrayAsync(
            $"https://prd-priconne-grandmasters-hq6jkeih.akamaized.net/10001300/Jpn/Android/assetbundles/{asset.Hash[..2]}/{asset.Hash}").Result);
});