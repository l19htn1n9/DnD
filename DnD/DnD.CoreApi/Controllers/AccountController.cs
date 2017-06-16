﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using JWT;
using JWT.Serializers;
using JWT.Algorithms;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DnD.Models.Entities;

namespace DnD.CoreApi.Controllers
{
    [Route("api/[controller]")]
	public class AccountController : Controller
	{
        private UserManager<IdentityUser> _userManager;
		private SignInManager<IdentityUser> _signInManager;
		private JWTSettings _options;

		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IOptions<JWTSettings> optionsAccessor)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_options = optionsAccessor.Value;
		}

		[HttpPost("sign-in")]
		public async Task<IActionResult> SignIn([FromBody] User Credentials)
		{
			if (ModelState.IsValid)
			{
                try
                {
					var result = await _signInManager.PasswordSignInAsync(Credentials.Username, Credentials.Password, false, false);
					if (result.Succeeded)
					{
						var user = await _userManager.FindByNameAsync(Credentials.Username);
						return new JsonResult(new Dictionary<string, object>
					{
						{ "access_token", GetAccessToken(Credentials.Username) },
						{ "id_token", GetIdToken(user) }
					});
					}
					return new JsonResult("Unable to sign in") { StatusCode = 401 };
                }
                catch(Exception e)
                {
                    return new JsonResult(e.Message) { StatusCode = 401 };
                }
			}
			return Error("Unexpected error");
		}

		[HttpGet]
		public string Get()
		{
			return "{'hello': 'world!'}";
		}

		private string GetIdToken(IdentityUser user)
		{
			var payload = new Dictionary<string, object>
			{
				{ "id", user.Id },
				{ "sub", user.Email },
				{ "email", user.Email },
				{ "emailConfirmed", user.EmailConfirmed },
			};
			return GetToken(payload);
		}

		private string GetAccessToken(string Email)
		{
			var payload = new Dictionary<string, object>
			{
				{ "sub", Email },
				{ "email", Email }
			};
			return GetToken(payload);
		}

		private string GetToken(Dictionary<string, object> payload)
		{
			var secret = _options.SecretKey;

			payload.Add("iss", _options.Issuer);
			payload.Add("aud", _options.Audience);
			payload.Add("nbf", ConvertToUnixTimestamp(DateTime.Now));
			payload.Add("iat", ConvertToUnixTimestamp(DateTime.Now));
			payload.Add("exp", ConvertToUnixTimestamp(DateTime.Now.AddDays(7)));
			IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
			IJsonSerializer serializer = new JsonNetSerializer();
			IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
			IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

			return encoder.Encode(payload, secret);
		}

		private JsonResult Errors(IdentityResult result)
		{
			var items = result.Errors.Select(x => x.Description).ToArray();
			return new JsonResult(items) { StatusCode = 400 };
		}

		private JsonResult Error(string message)
		{
			return new JsonResult(message) { StatusCode = 400 };
		}

		private static double ConvertToUnixTimestamp(DateTime date)
		{
			DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			TimeSpan diff = date.ToUniversalTime() - origin;
			return Math.Floor(diff.TotalSeconds);
		}
	}
}
