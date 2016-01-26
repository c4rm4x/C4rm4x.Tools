namespace C4rm4x.Tools.Utilities
{
    /// <summary>
    /// Utility methods related to the base class System.Object
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks whether the object is null
        /// </summary>
        /// <param name="theObject">The object</param>
        /// <returns>True whether the oject is null. False otherwise</returns>
        public static bool IsNull(this object theObject)
        {
            return theObject == null;
        }

        /// <summary>
        /// Checks whether the object is not null
        /// </summary>
        /// <param name="theObject">The object</param>
        /// <returns>True whether the oject is not null. False otherwise</returns>
        public static bool IsNotNull(this object theObject)
        {
            return !theObject.IsNull();
        }
    }
}
