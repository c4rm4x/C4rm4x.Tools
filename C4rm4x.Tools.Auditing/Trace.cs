#region Using

using C4rm4x.Tools.Utilities;
using Newtonsoft.Json;
using System;
using System.Reflection;

#endregion

namespace C4rm4x.Tools.Auditing
{
    /// <summary>
    /// Representation of a trace
    /// </summary>
    public class Trace
    {
        /// <summary>
        /// Gets the property name
        /// </summary>
        [JsonProperty(PropertyName = "Property")]
        public string PropertyName { get; private set; }

        /// <summary>
        /// Gets the property type
        /// </summary>
        [JsonIgnore]
        public Type PropertyType { get; private set; }

        /// <summary>
        /// Gets the original value
        /// </summary>
        [JsonProperty(PropertyName = "Before")]
        public object OriginalValue { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="propertyInfo">The property info</param>
        /// <param name="instance">The original instance</param>
        public Trace(
            PropertyInfo propertyInfo,
            object instance)
        {
            propertyInfo.NotNull(nameof(propertyInfo));
            instance.NotNull(nameof(instance));

            PropertyName = propertyInfo.Name;
            PropertyType = propertyInfo.PropertyType;
            OriginalValue = propertyInfo.GetValue(instance, null);
        }
    }
}
