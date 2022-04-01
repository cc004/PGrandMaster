using MessagePack;

[MessagePackObject]
public class AssetBundleLoadName
{
    [Key(0)]
    public string AssetName { get; set; }
    [Key(1)]
    public string Name { get; set; }
}