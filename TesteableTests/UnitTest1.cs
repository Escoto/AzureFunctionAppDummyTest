using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testeable;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Diagnostics;
using Microsoft.Azure.NotificationHubs;

namespace TesteableTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var log = new TraceWriterTest(TraceLevel.Off);
            var req = new HttpRequestMessage() {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://localhost:7071/api/fnTesteable2/readingX/%7B%22X%22:654%7D")
            };
            var testValues = GetTestValues();

            var result = testValues.Select(v => fnTesteable2.Run(req, v, log).Content).ToList();

            Assert.AreEqual(testValues.Count, result.Count);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var log = new TraceWriterTest(TraceLevel.Off);
          
            var testValues = GetTestValues();

            IDictionary<string, string> notif;
            fnTesteable1.Run("Message A", out notif, log);
            

            Assert.AreEqual(notif["message"], "Message A");
        }

        private List<string> GetTestValues()
        {
            var testValues = new List<string>();
            testValues.Add("{\"X\":1}");
            testValues.Add("{\"X\":2}");
            testValues.Add("{\"X\":3}");
            testValues.Add("{\"X\":4}");

            return testValues;
        }
    }

    public class TraceWriterTest : TraceWriter
    {
        public TraceWriterTest(TraceLevel level) : base(level)
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override void Flush()
        {
            base.Flush();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void Trace(TraceEvent traceEvent)
        {

        }
    }

    public class NotificationTest : Notification
    {
        protected NotificationTest(IDictionary<string, string> additionalHeaders, string tag) : base(additionalHeaders, tag)
        {}

        protected override string PlatformType => this.PlatformType;

        protected override void OnValidateAndPopulateHeaders()
        {}
    }

}
