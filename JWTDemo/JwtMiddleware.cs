using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace JWTDemo
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
            //_appSettings = appSettings.Value;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                MiddlewareValidateJwtToken(context, token);

            await _next(context);
        }
        private void MiddlewareValidateJwtToken(HttpContext context, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(JwtService.SecretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken securityToken);
                var jwtToken = (JwtSecurityToken)securityToken;

                var id = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                context.Items["Id"] = id;
            }
            catch(Exception ex)
            {
                //throw new Exception("Can not vaid token!");
            }
        }
    }
}
