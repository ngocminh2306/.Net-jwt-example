using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTDemo
{
    public class JwtService
    {
        public static byte[] SecretKey = Encoding.ASCII.GetBytes("123@123@123@123@123@123@123@123@123@123@123@123@123@123@123@123@123@123@123@123@123@123@123@123@123@123@123");
        //public JwtService()
        //{
        //    SecretKey = Encoding.ASCII.GetBytes("123@123@123");
        //}
        public static string GenerateSecurityToken()
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                //ClaimsIdentity
                var claims = new ClaimsIdentity(new[] { new Claim("id", "1"), new Claim("user", "test") });
                //gen Token
                var securityTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKey), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenHandler.CreateToken(securityTokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch(Exception ex)
            {
                throw new Exception("Create token fail!");
            }

        }
        //public static int? ValidateJwtToken(string token)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    try
        //    {
        //        tokenHandler.ValidateToken(token, new TokenValidationParameters
        //        {
        //            IssuerSigningKey = new SymmetricSecurityKey(SecretKey)
        //        }, out SecurityToken securityToken);
        //        var jwtToken = (JwtSecurityToken)securityToken;

        //        var id = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
        //        return id;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception("Can not vaid token!");
        //    }
        //}
    }
}
