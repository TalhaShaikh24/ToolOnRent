using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ModelClassLibrary
{
    public class TokenManager
    {
        public static Dictionary<string, string> GeneratedTokens = new Dictionary<string, string>();
        private static string Secret = "J8c20cxkPZCC/0e0ZUcjrGocsk95gOAqjzJ09apAklM=";
        private static double TokenExpireTime = 3600.0;
        public static string GenerateToken(LoginCredentials obj)
        {

            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {

                        new Claim("Id",Convert.ToString(obj.Id)),
                        new Claim("Firstname", Convert.ToString(obj.Firstname)??""),
                        new Claim("Lastname", Convert.ToString(obj.Lastname) ?? ""),
                        new Claim("Email", Convert.ToString(obj.Email) ?? ""),
                        new Claim("Role", Convert.ToString(obj.Role) ?? ""),

                }
                ),
                Issuer = "Finance",
                Expires = DateTime.Now.AddMinutes(TokenExpireTime),
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);

            string value = string.Empty;
            return handler.WriteToken(token);
        }
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                byte[] key = Convert.FromBase64String(Secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static LoginCredentials ValidateToken(string token)
        {
            LoginCredentials obj = new LoginCredentials();
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
                return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }


            Claim Id = identity.FindFirst("Id");
            Claim Firstname = identity.FindFirst("Firstname");
            Claim Lastname = identity.FindFirst("Lastname");
            Claim Email = identity.FindFirst("Email");
            Claim Role = identity.FindFirst("Role");


            obj.Id = Convert.ToInt32(Id.Value);
            obj.Firstname = Convert.ToString(Firstname.Value);
            obj.Lastname = Convert.ToString(Lastname.Value);
            obj.Email = Convert.ToString(Email.Value);
            obj.Role = Convert.ToString(Role.Value);

            return obj;
        }

        public static LoginCredentials GetValidateToken(HttpRequest httpRequest)
        {
            string value = string.Empty;
            if (!httpRequest.Headers.ContainsKey("Authorization"))
            {
                return null;
            }

            string authHeader = httpRequest.Headers["Authorization"];
            LoginCredentials claimDTO = null;
            string token = authHeader;

            if (token != null)
            {
                var item = GeneratedTokens.FirstOrDefault(kvp => kvp.Value == token);

                if (!item.Equals(default(KeyValuePair<string, string>)))
                {
                    GeneratedTokens.Remove(item.Key);
                }

            }

            claimDTO = TokenManager.ValidateToken(token);

            if (claimDTO == null)
            {
                return null;
            }

            return claimDTO;
        }

        public static string RemoveToken(string token)
        {
            var item = GeneratedTokens.FirstOrDefault(kvp => kvp.Value == token);
            if (!item.Equals(default(KeyValuePair<string, string>)))
            {
                GeneratedTokens.Remove(item.Key);
                return null;
            }
            else
            {
                return null;
            }

        }

    }
}
