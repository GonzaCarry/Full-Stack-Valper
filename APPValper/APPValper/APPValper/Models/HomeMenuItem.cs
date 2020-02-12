using System;
using System.Collections.Generic;
using System.Text;

namespace APPValper.Models
{
    public enum MenuItemType
    {
        Home,
        EditUser,
        Options
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
