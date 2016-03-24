#region Using

using System;

#endregion

namespace C4rm4x.Tools.TestUtilities
{
    /// <summary>
    /// Flags a test as Acceptance
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AcceptanceTestAttribute : TestCategoryAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AcceptanceTestAttribute()
            : base(TestCategoryType.Acceptance)
        {
        }
    }
}
