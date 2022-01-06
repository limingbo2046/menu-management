using System;
using System.Collections.Generic;
using System.Text;

namespace lcn.menu_management
{
    public class MenuItemUpdateDto
    {
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentMenuItem { get; set; }

        /// <summary>
        /// Display name of the menu item.
        /// 显示的菜单名字
        /// </summary>

        public string DisplayName { get; set; }

        /// <summary>
        /// The Display order of the menu.
        /// Default value: 1000.
        /// 排列顺序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// The URL to navigate when this menu item is selected.
        /// 点击该菜单的链接
        /// </summary>

        public string Url { get; set; }

        /// <summary>
        /// Icon of the menu item if exists.
        /// 图标
        /// </summary>

        public string Icon { get; set; }

        /// <summary>
        /// Target of the menu item. Can be null, "_blank", "_self", "_parent", "_top" or a frame name for web applications.
        /// 目标 可空,"_blank", "_self", "_parent", "_top"
        /// </summary>

        public string Target { get; set; }

        /// <summary>
        /// Can be used to disable this menu item.
        /// 禁用
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Can be used to render the element with a specific Id for DOM selections.
        /// DOM的ID
        /// </summary>
        public string ElementId { get; set; }

        /// <summary>
        /// Can be used to render the element with extra CSS classes.
        /// CSS类
        /// </summary>
        public string CssClass { get; set; }

        public bool IsMenuItem { get; set; }
    }
}
