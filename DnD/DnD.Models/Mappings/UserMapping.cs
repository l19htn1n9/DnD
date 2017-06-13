using System;
using Dapper.FluentMap.Mapping;
using DnD.Models.Entities;

namespace DnD.Models.Mappings
{
    public class UserMapping : EntityMap<User>
    {
        public UserMapping()
        {
            Map(x => x.Id).ToColumn("Id");
            Map(x => x.Username).ToColumn("Username");
            Map(x => x.Password).ToColumn("Password");
        }
    }
}
