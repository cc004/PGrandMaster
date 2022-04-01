using MessagePack;

[MessagePackObject]
public struct GeneralError
{
    [Key(0)] public GeneralErrorCode Code { get; set; }
    [Key(1)] public string Reason { get; set; }
}