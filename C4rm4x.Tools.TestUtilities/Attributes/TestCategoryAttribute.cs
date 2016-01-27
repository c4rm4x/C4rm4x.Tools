#region Using

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace C4rm4x.Tools.TestUtilities
{
    /// <summary>
    /// Flags a test as specified
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class TestCategoryAttribute : TestCategoryBaseAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="testCategory">The test category</param>
        public TestCategoryAttribute(TestCategoryType testCategory)
            : base()
        {
            TestCategory = testCategory;
        }

        /// <summary>
        /// Gets or set the test category
        /// </summary>
        public TestCategoryType TestCategory { get; set; }

        /// <summary>
        /// Gets the list of all test categories
        /// </summary>
        public override IList<string> TestCategories
        {
            get { return new string[] { TestCategory.ToString() }.ToList(); }
        }
    }
}
