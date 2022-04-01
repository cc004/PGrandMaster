using MessagePack;

[MessagePackObject]
public class ManifestRawAsset
{
    [Key(0)]
    public string Name { get; set; }
    [Key(1)]
    public string Hash { get; set; }
    [Key(2)]
    public int Size { get; set; }
    [Key(3)]
    public string Category { get; set; }
    [Key(4)]
    public int Group { get; set; }
    [Key(5)]
    public ulong CheckSum { get; set; }
}