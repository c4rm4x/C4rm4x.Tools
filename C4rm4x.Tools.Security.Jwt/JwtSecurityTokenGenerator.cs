﻿#region Using

using C4rm4x.Tools.Utilities;
using System;
using System.IdentityModel.Tokens;
using System.Security.Claims;

#endregion

namespace C4rm4x.Tools.Security.Jwt
{
    /// <summary>
    /// Implementation of the Json web tokens generator
    /// </summary>
    public class JwtSecurityTokenGenerator
    {
        private Func<JwtSecurityTokenHandler> _securityTokenHandlerFactory =
            () => new JwtSecurityTokenHandler();

        private Func<DateTime> _dateTimeNowFactory =
            () => DateTime.UtcNow;

        /// <summary>
        /// Generates an instance of JWT as a JSON Compact serialized format string
        /// </summary>
        /// <param name="subject">The source of the claim(s) for this token</param>
        /// <param name="options">Options to generate the JWT</param>
        /// <returns>The JWT as a signed (if required) encoded string.</returns>
        public string Generate(
            ClaimsIdentity subject,
            JwtGenerationOptions options)
        {
            options.NotNull(nameof(options));

            var now = _dateTimeNowFactory();
            var tokenHandler = _securityTokenHandlerFactory();

            var securityToken = tokenHandler
                .CreateToken(
                    issuer: "default",
                    subject: subject,
                    notBefore: now,
                    expires: GetExpiresAt(now, options),
                    signingCredentials: GetSigningCredentials(options.SigningCredentials));

            return tokenHandler.WriteToken(securityToken);
        }

        private DateTime? GetExpiresAt(
            DateTime now,
            JwtGenerationOptions options)
        {
            return now.AddMinutes(options.TokenLifetimeInMinutes);
        }

        private SigningCredentials GetSigningCredentials(JwtSigningCredentials signingCredentials)
        {
            if (signingCredentials.IsNull() ||
                signingCredentials.SigningAlgorithm == JwtSigningAlgorithm.NONE)
                return null;

            return new HmacSigningCredentials(signingCredentials.Key);
        }

        /// <summary>
        /// Sets the security token handler factory
        /// </summary>
        /// <param name="securityTokenHandlerFactory">The factory</param>
        /// <remarks>USE THIS ONLY FOR UNIT TESTING</remarks>
        internal void SetSecurityTokenHandlerFactory(Func<JwtSecurityTokenHandler> securityTokenHandlerFactory)
        {
            securityTokenHandlerFactory.NotNull(nameof(securityTokenHandlerFactory));

            _securityTokenHandlerFactory = securityTokenHandlerFactory;
        }

        /// <summary>
        /// Sets the DateTime Now factory
        /// </summary>
        /// <param name="dateTimeNowFactory">The factory</param>
        /// <remarks>USE THIS ONLY FOR UNIT TESTING</remarks>
        internal void SetDateTimeNowFactory(Func<DateTime> dateTimeNowFactory)
        {
            dateTimeNowFactory.NotNull(nameof(dateTimeNowFactory));

            _dateTimeNowFactory = dateTimeNowFactory;
        }
    }
}
