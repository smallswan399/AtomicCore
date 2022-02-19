using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtomicCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.Tests
{
    [TestClass()]
    public class Base64HandlerTests
    {
        [TestMethod()]
        public void ConvertToOriginalTest()
        {
            string value = "eyJhdmF0YXIiOiJodHRwczovL3RsLnhpYW9saXVuY3AuY29tL2F2YXRhci91c2VyL2YwNjg3OGEyODlkODA2MGI3NTRiOTg2NTIyNDcyMzBmLmpwZyIsImhhcmR3YXJlaWQiOiIiLCJpc0JpbmRQaG9uZSI6MSwiaXNCaW5kV3giOjAsIm5pY2tuYW1lIjoib2JqZWN0Iiwib3BlbklkIjoiakhUWlBQbHkzSXdRd3JjeFMxWWZpZVZudHJrVy1fcTUiLCJwb2QiOiIwIiwic2V4IjoxLCJzaWduIjoiMGZmNGM4YmU4MWY1NzZhM2U2MzE3NzUzNTMwZTRmMjUiLCJ0aW1lIjoxNjQ1MjU3OTEwMjU5fQ";

            string orig = Base64Handler.ConvertToOriginal(value);

            Assert.IsTrue(!string.IsNullOrEmpty(orig));
        }
    }
}