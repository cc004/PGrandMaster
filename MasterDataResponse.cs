using MessagePack;

[MessagePackObject]
public class MasterDataResponse
{
    [Key(0)]
    public byte[] Data { get; set; }
    [Key(1)]
    public string Hash { get; set; }
}