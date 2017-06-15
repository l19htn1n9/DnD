using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using JWT;
using JWT.Serializers;
using JWT.Algorithms;

namespace DnD.CoreApi
{
    public class AppUserManager : UserManager<IdentityUser>
    {
        public AppUserManager(UserStore<IdentityUser> a_store) : base(a_store, null, null, null, null, null, null, null, null)
        {
            PasswordHasher = new CustomPasswordHasher();
        }
    }
}
