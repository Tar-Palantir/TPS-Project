using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPS.Common.Event;

namespace TPS.Test.CommonTest
{
    [TestClass]
    public class EventTest
    {
        [TestMethod]
        public void TestEvent()
        {
            var name = "TestEvent";
            var results = GetEventListener(name);

            Assert.IsTrue(results != null && results.Any());
        }


        private IList<Type> GetEventListener(string name)
        {
            var _section = (EventConfigurationSection)ConfigurationManager.GetSection("eventlistener");

            //var events = _section.Events.GetEventElements(name);
            var events = _section.Events;

            IList<Type> results = new List<Type>();
            foreach (var e in events)
            {
                try
                {
                    var type = Assembly.Load(e.Assembly).GetType(e.Type, false);
                    if (type == null) continue;

                    results.Add(type);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return results;
        }
    }
}
