#region Using

using C4rm4x.Tools.Utilities;
using C4rM4x.Tools.Messaging;
using Microsoft.ServiceBus.Messaging;
using System;

#endregion

namespace C4rm4x.Tools.ServiceBus
{
    /// <summary>
    /// ServiceBus BrokeredMessage utilities
    /// </summary>
    public static class BrokeredMessagesExtensions
    {
        /// <summary>
        /// Generates a BrokeredMessage from content
        /// </summary>
        /// <typeparam name="TContent">Type of the content to encapsulate into the instance of BrokeredMessage</typeparam>
        /// <param name="content">The content</param>
        /// <returns>An instance of BrokeredMessage</returns>
        public static BrokeredMessage BuildBrokeredMessage<TContent>(
            this TContent content)
        {
            var brokeredMessage = new BrokeredMessage(content);

            brokeredMessage.Label = "{0}_{1}".AsFormat(typeof(TContent).Name, DateTime.UtcNow.ToString("yyyyMMddHHmmss"));
            brokeredMessage.MessageId = GetMessageId(content);
            brokeredMessage.ContentType = typeof(TContent).AssemblyQualifiedName;

            return brokeredMessage;
        }

        private static string GetMessageId<TContent>(TContent content)
        {
            var contentAsMessageDescriptor = content as IMessageDescriptor;

            return contentAsMessageDescriptor.IsNotNull()
                ? contentAsMessageDescriptor.GetMessageId()
                : Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Extracts the original content out of a BrokeredMessage body
        /// </summary>
        /// <param name="brokeredMessage">The brokered message</param>
        /// <returns>The content</returns>
        public static object ExtractContent(
            this BrokeredMessage brokeredMessage)
        {
            var contentType = Type.GetType(brokeredMessage.ContentType);
            var method = typeof(BrokeredMessage).GetMethod("GetBody", new Type[] { });
            var generic = method.MakeGenericMethod(contentType);

            return generic.Invoke(brokeredMessage, null);
        }
    }
}
