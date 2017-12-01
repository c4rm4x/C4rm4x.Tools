#region Using

using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

#endregion

namespace C4rm4x.Tools.AzureQueue
{
    /// <summary>
    /// Azure Queues CloudQueueMessage utilities
    /// </summary>
    public static class CloudQueueMessageExtensions
    {
        private static JsonSerializerSettings Settings => new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        /// <summary>
        /// Generates a CloudQueueMessage from content
        /// </summary>
        /// <typeparam name="TContent">Type of the content to encapsulate into the instance of CloudQueueMessage</typeparam>
        /// <param name="content">The content</param>
        /// <returns>An instance of CloudQueueMessage</returns>
        public static CloudQueueMessage BuildCloudQueueMessage<TContent>(this TContent content)
        {
            return new CloudQueueMessage(JsonConvert.SerializeObject(content, Settings));
        }

        /// <summary>
        /// Extracts the original content out of a CloudQueueMessage body
        /// </summary>
        /// <param name="cloudQueueMessage">The cloud queue message</param>
        /// <returns>The content</returns>
        public static object ExtractContent(this CloudQueueMessage cloudQueueMessage)
        {
            return JsonConvert.DeserializeObject(cloudQueueMessage.AsString, Settings);
        }
    }
}
