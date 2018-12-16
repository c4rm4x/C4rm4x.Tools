using C4rm4x.Tools.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C4rm4x.Tools.Security.GoogleAuthenticator
{
    /// <summary>
    /// Service to validate Totp based on rfc6238
    /// </summary>
    public static class TotpValidator
    {
        private static DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static TimeSpan DefaultTtl = TimeSpan.FromMinutes(5);

        /// <summary>
        /// Validates the given code using the given account secret key
        /// </summary>
        /// <param name="accountSecretKey">Account Secret Key</param>
        /// <param name="code">The code to validate</param>
        /// <param name="timeTolerance">Time during the code is valid (default is 5m)</param>
        /// <returns></returns>
        public static bool Validate(
            string accountSecretKey,
            string code,
            TimeSpan? timeTolerance = null)
        {
            accountSecretKey.NotNullOrEmpty(nameof(accountSecretKey));
            code.NotNullOrEmpty(nameof(code));

            var codes = GeneratePins(accountSecretKey, timeTolerance ?? DefaultTtl).ToList();

            return codes.Any(c => c == code);
        }

        private static IEnumerable<string> GeneratePins(
            string accountSecretKey,
            TimeSpan timeTolerance)
        {
            const double Limit = 30;

            var iterationCounter = GetCurrentCounter();
            int iterationOffset = 0;

            if (timeTolerance.TotalSeconds > Limit)
            {
                iterationOffset = Convert.ToInt32(timeTolerance.TotalSeconds / Limit);
            }

            var iterationStart = iterationCounter - iterationOffset;
            var iterationEnd = iterationCounter + iterationOffset;

            for (var counter = iterationStart; counter <= iterationEnd; counter++)
                yield return GeneratePin(accountSecretKey, counter);
        }

        private static long GetCurrentCounter() => GetCurrentCounter(DateTime.UtcNow, 30);

        private static long GetCurrentCounter(DateTime now, int step)
        {
            return (long)(now - Epoch).TotalSeconds / step;
        }

        private static string GeneratePin(
            string accountSecretKey,
            long counter,
            int digits = 6)
        {
            return Encoding.UTF8.GetBytes(accountSecretKey)
                .GenerateHashedCode(counter, digits);
        }        
    }
}