using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TPS.WeiXin.DataAccess.Entities.Enums;

namespace TPS.WeiXin.DataAccess.Entities.Extentions
{
    public class Menu
    {
        [JsonProperty(PropertyName = "button")]
        public IList<MenuButton> Button { get; set; }

        public void AddButtons(IList<CustomMenu> menuList)
        {
            var rootMenus = menuList.Where(p => !p.ParentMenuID.HasValue).Select(p => new MenuButton
            {
                ID = p.ID,
                Type = MenuTypeToString((EnumMenuType)p.Type),
                Name = p.Name,
                Key = p.Key,
                Url = p.View_Url
            }).ToList();

            var childrenMenu = menuList.Where(p => p.ParentMenuID.HasValue).GroupBy(p => p.ParentMenuID);
            foreach (var menuButton in rootMenus)
            {
                if (!childrenMenu.Any(p => p.Key == menuButton.ID))
                {
                    continue;
                }

                var subMenus = childrenMenu.FirstOrDefault(p => p.Key == menuButton.ID);
                menuButton.SubButton = subMenus.Select(p => new MenuButton
                    {
                        Type = MenuTypeToString((EnumMenuType)p.Type),
                        Name = p.Name,
                        Key = p.Key,
                        Url = p.View_Url
                    }).ToList();

                menuButton.Key = string.Empty;
                menuButton.Type = string.Empty;
                menuButton.Url = string.Empty;
            }

            Button = rootMenus;
        }

        private string MenuTypeToString(EnumMenuType type)
        {
            switch (type)
            {
                case EnumMenuType.Click:
                    return "click";
                case EnumMenuType.View:
                    return "view";
                default:
                    return "";
            }
        }
    }


    public class MenuButton
    {
        internal Guid ID { set; get; }

        [JsonProperty(PropertyName = "type")]
        internal string Type { get; set; }

        [JsonProperty(PropertyName = "name")]
        internal string Name { get; set; }

        [JsonProperty(PropertyName = "url",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        internal string Url { get; set; }

        [JsonProperty(PropertyName = "key",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        internal string Key { get; set; }

        [JsonProperty(PropertyName = "sub_button",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        internal IList<MenuButton> SubButton { get; set; }
    }
}
