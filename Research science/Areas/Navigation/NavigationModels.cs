using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Research_science.Areas.Navigation
{
    public class NavigationMenu
    {
        public List<NavigationMenuItem> MenuItems { get; set; }
    }
    public class NavigationMenuItem
    {
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
        public bool IsNested { get; set; }
        public int Sequence { get; set; }
        public List<string> UserRoles { get; set; }
        public List<NavigationMenuItem> NestedItems { get; set; }
        public class NavigationModels
        {
        }
    }
}