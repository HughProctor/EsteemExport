using System.Collections.Generic;
using ServiceModel.Models.BAM;

namespace ServiceModel.Services.Abstract
{
    public interface IBAM_UserService
    {
        BAM_User GetUser(string userName);
        List<BAM_User> GetUserList();
        List<BAM_User> UserList { get; set; }

    }
}