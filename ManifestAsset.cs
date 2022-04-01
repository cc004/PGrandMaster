using MessagePack;

[MessagePackObject]
public class ManifestAsset
{
    [Key(0)]
    public string Name { get; set; }
    [Key(1)]
    public string Hash { get; set; }
    [Key(2)]
    public string[] Dependencies { get; set; }
    [Key(3)]
    public long Key { get; set; }
    [Key(4)]
    public int Size { get; set; }
    [Key(5)]
    public string Category { get; set; }
    [Key(6)]
    public int Group { get; set; }
    [Key(7)]
    public ulong CheckSum { get; set; }
}