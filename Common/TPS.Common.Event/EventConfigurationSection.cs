using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml;

namespace TPS.Common.Event
{
    public class EventConfigurationSection : IConfigurationSectionHandler
    {
        public IList<EventConfigurationElement> Events { set; get; }

        /// <summary>
        /// Creates a configuration section handler.
        /// </summary>
        /// <returns>
        /// The created section handler object.
        /// </returns>
        /// <param name="parent">Parent object.</param>
        /// <param name="configContext">Configuration context object.</param>
        /// <param name="section">Section XML node.</param>
        public object Create(object parent, object configContext, XmlNode section)
        {
            var eventConfigurationSection = new EventConfigurationSection();
            var xmlNodeList = section.SelectNodes("events");
            if (xmlNodeList != null)
            {
                IList<EventConfigurationElement> eventList = (from XmlNode node in xmlNodeList
                    select new EventConfigurationElement
                    {
                        EventName = node.Attributes["eventname"].Value,
                        Type = node.Attributes["type"].Value,
                        Key = node.Attributes["key"].Value, 
                        Assembly = node.Attributes["assembly"].Value,
                        AccountID = node.Attributes["accountid"].Value.ToLower()
                    }).ToList();

                eventConfigurationSection.Events = eventList;
            }

            return eventConfigurationSection;
        }
    }
}
