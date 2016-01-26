#region Using

using System;

#endregion

namespace C4rm4x.Tools.TestUtilities.Attributes
{
    /// <summary>
    /// Flags a test as Integration
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class IntegrationTestAttribute : TestCategoryAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public IntegrationTestAttribute()
            : base(TestCategoryType.Integration)
        {
        }
    }
}
