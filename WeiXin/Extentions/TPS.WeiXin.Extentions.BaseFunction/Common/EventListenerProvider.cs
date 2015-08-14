using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using TPS.Common.Event;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.DataAccess.Implement;

namespace TPS.WeiXin.Extentions.BaseFunction.Common
{
    public static class EventListenerProvider
    {
        private static readonly EventConfigurationSection _section;

        static EventListenerProvider()
        {
            _section = (EventConfigurationSection)ConfigurationManager.GetSection("eventlistener");
        }

        public static IList<T> GetEventListener<T>()
        {
            var typeT = typeof(T);

            var events = _section.Events.Where(p => p.EventName == typeT.Name);

            IList<T> results = new List<T>();
            foreach (var e in events)
            {
                try
                {
                    var type = Assembly.Load(e.Assembly).GetType(e.Type, false);
                    if (type == null) continue;
                    if (type.GetInterface(typeT.Name) == null) continue;

                    results.Add((T)Activator.CreateInstance(type));
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return results;
        }

        public static IList<T> GetEventListener<T>(string key, out CustomMenu cMenu)
        {
            CustomMenuRepository repository = new CustomMenuRepository();
            cMenu = repository.GetByKey(key);
            if (cMenu == null)
            {
                return null;
            }

            return GetEventListener<T>();
        }

        public static T GetSpecialEvent<T>(IList<T> events, CustomMenu cMenu)
        {
            if (string.IsNullOrEmpty(cMenu.TypeFullName))
            {
                return (T)(object)null;
            }

            return events.FirstOrDefault(p => p.GetType().Name == cMenu.TypeFullName);
        }
    }
}