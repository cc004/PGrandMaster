using MagicOnion;

namespace Priari.ServerShared.Services
{
    public interface IUserAccountService : IService<IUserAccountService> // TypeDefIndex: 12020
    {
        public UnaryResult<ServiceResult<MasterDataResponse, MasterCacheResult>> GetMasterDataAsync(string hash);
        public UnaryResult<ServiceResult<CreateUserResponse>> CreateUserAsync(string userName);
        public UnaryResult<ServiceResult<InitialDataResponse>> GetInitialDataAsync();
    }
}