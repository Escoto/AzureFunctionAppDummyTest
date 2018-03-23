using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;
using System.Collections.Generic;

namespace Testeable
{
    public static class fnTesteable1
    {
        [FunctionName("fnTesteable1")]
        public static void Run(
            [ServiceBusTrigger("testqueue", AccessRights.Manage, Connection = "conn")]
            string myQueueItem,
            //out Notification notification,
            out IDictionary<string, string> notification,
            TraceWriter log)
        {
            log.Info($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            notification = GetTemplateProperties(myQueueItem);
        }

        private static IDictionary<string, string> GetTemplateProperties(string message)
        {
            Dictionary<string, string> templateProperties = new Dictionary<string, string>();
            templateProperties["message"] = message;
            return templateProperties;
        }

        private static TemplateNotification GetTemplateNotification(string message)
        {
            Dictionary<string, string> templateProperties = new Dictionary<string, string>();
            templateProperties["message"] = message;
            return new TemplateNotification(templateProperties);
        }
    }
}
