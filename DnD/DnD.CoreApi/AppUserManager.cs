using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using JWT;
using JWT.Serializers;
using JWT.Algorithms;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace DnD.CoreApi
{
    public class AppUserManager : UserManager<IdentityUser>
    {
        //public AppUserManager(UserStore<IdentityUser> store, IOptions<IdentityOptions> options, IPasswordHasher<IdentityUser> passwordHasher, IEnumerable<IUserValidator<IdentityUser>> validator, IEnumerable<IPasswordValidator<IdentityUser>> passwordValidator, ILookupNormalizer lookupNormalizer, IdentityErrorDescriber errorDescriber, IServiceProvider serviceProvider, ILogger<UserManager<IdentityUser>> logger)
        //    : base(store, options, passwordHasher, validator, passwordValidator, lookupNormalizer, errorDescriber, serviceProvider, logger)
        //{
        //    PasswordHasher = new CustomPasswordHasher();
        //}
        public AppUserManager(IUserStore<IdentityUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<IdentityUser> passwordHasher, IEnumerable<IUserValidator<IdentityUser>> userValidators, IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<IdentityUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            PasswordHasher = new CustomPasswordHasher();
            PasswordHasher = new CustomPasswordHasher();
        }
    }
}
