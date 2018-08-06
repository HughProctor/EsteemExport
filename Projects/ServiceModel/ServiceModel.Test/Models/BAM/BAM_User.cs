using System.Collections.Generic;

namespace ServiceModel.Test.Models.BAM
{
    public class BAM_UserList
    {
        public List<User> Users { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
