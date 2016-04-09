#region Using

using C4rm4x.Tools.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;

#endregion

namespace C4rm4x.Tools.Auditing
{
    /// <summary>
    /// Extensions for audit trails results
    /// </summary>
    public static class AuditTrailsExtensions
    {
        /// <summary>
        /// Serializes all the traces given as a json string
        /// </summary>
        /// <param name="traces">The list of all traces</param>
        /// <returns>The representation as a json string</returns>
        public static string Serialize(
            this IEnumerable<Trace> traces)
        {
            traces.NotNull(nameof(traces));

            return JsonConvert.SerializeObject(traces);
        }
    }
}
