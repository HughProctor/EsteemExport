using ServiceModel.Models.BAM;

namespace ServiceModel.Services.Abstract
{
    public interface IBAM_UserService
    {
        BAM_User GetUser(string userName);
    }
}