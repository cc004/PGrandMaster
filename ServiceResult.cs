using MessagePack;

[MessagePackObject]
public class ServiceResult<T> : ServiceResult<T, GeneralError>
{

}

[MessagePackObject]
public class ServiceResult<T, TError> : IServiceResult // TypeDefIndex: 12068
{
    [Key(0)] public T Result { get; set; }
    [Key(1)] public bool Succeeded { get; set; }
    [Key(2)] public TError ErrorDetail { get; set; }
}