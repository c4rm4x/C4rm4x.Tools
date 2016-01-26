#region Using

using System;

#endregion

namespace C4rm4x.Tools.TestUtilities.Attributes
{
    /// <summary>
    /// Flags a test as Unit
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UnitTestAttribute : TestCategoryAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UnitTestAttribute()
            : base(TestCategoryType.Unit)
        {
        }
    }
}
