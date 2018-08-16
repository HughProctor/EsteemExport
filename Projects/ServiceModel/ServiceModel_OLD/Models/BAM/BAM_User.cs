using System.Collections.Generic;

namespace ServiceModel.Models.BAM
{
    public class BAM_UserList
    {
        public List<BAM_User> BAM_Users { get; set; }
    }

    public class BAM_User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
