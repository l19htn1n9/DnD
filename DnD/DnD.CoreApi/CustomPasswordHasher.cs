using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DnD.CoreApi
{
    public class CustomPasswordHasher : Microsoft.AspNetCore.Identity.IPasswordHasher<IdentityUser>
    {

        public string HashPassword(IdentityUser user, string password)
        {
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(IdentityUser user, string hashedPassword, string providedPassword)
        {
			if (hashedPassword.Equals(providedPassword))
				return PasswordVerificationResult.Success;
			else return PasswordVerificationResult.Failed;
        }
    }
}
