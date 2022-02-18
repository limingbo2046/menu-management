using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using System.Collections.ObjectModel;
using System.Linq;
using Volo.Abp.MultiTenancy;

namespace lcn.menu_management
{
    /// <summary>
    /// 菜单和按钮组合在这里
    /// </summary>
    public class MenuItem : Entity<Guid>, IMultiTenant
    {
        /// <summary>
        /// 父菜单（为空的时候，将当作是子系统的根菜单）
        ///  -system1
        ///  +++++++菜单1
        ///  +++++++++++子菜单1
        ///  +++++++++++子菜单2
        ///  ++++++++++++++++++按钮1
        ///  ++++++++++++++++++按钮2
        /// </summary>
        public Guid? ParentMenuItem { get; set; }

        private string _displayName;

        /// <summary>
        /// Default <see cref="Order"/> value of a menu item.
        /// </summary>
        public const int DefaultOrder = 1000;

        /// <summary>
        /// Display name of the menu item.
        /// 显示的菜单名字
        /// </summary>
        [NotNull]
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                Check.NotNullOrWhiteSpace(value, nameof(value));
                _displayName = value;
            }
        }

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
        [CanBeNull]
        public string Url { get; set; }

        /// <summary>
        /// Icon of the menu item if exists.
        /// 图标
        /// </summary>
        [CanBeNull]
        public string Icon { get; set; }

        /// <summary>
        /// Target of the menu item. Can be null, "_blank", "_self", "_parent", "_top" or a frame name for web applications.
        /// 目标 可空,"_blank", "_self", "_parent", "_top"
        /// </summary>
        [CanBeNull]
        public string Target { get; set; }

        /// <summary>
        /// Can be used to disable this menu item.
        /// 禁用
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Can be used to store a custom object related to this menu item. Optional.
        /// 自定义
        /// </summary>
        public string CustomData { get; set; }

        /// <summary>
        /// Can be used to render the element with a specific Id for DOM selections.
        /// 用于区分同级元素的ID
        /// </summary>
        public string ElementId { get; set; }

        /// <summary>
        /// Can be used to render the element with extra CSS classes.
        /// CSS类
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// 菜单（true)按钮(false)
        /// </summary>
        public bool IsMenuItem { get; set; }
        /// <summary>
        /// 菜单租户
        /// </summary>
        public Guid? TenantId { get; set; }

        public MenuItem(
            Guid Id,
            [NotNull] string displayName,
            Guid? parentMenuItemId,
            Guid? tenantId,
            string url = null,
            string icon = null,
            int order = DefaultOrder,
            string customData = null,
            string target = null,
            string elementId = null,
            string cssClass = null,
            bool isMenuItem = true

            ) : base(Id)
        {
            Check.NotNullOrWhiteSpace(displayName, nameof(displayName));

            DisplayName = displayName;
            ParentMenuItem = parentMenuItemId;
            TenantId = tenantId;
            Url = url;
            Icon = icon;
            Order = order;
            CustomData = customData;
            Target = target;
            ElementId = elementId;
            CssClass = cssClass;
            IsMenuItem = isMenuItem;

        }
        protected MenuItem()
        {

        }


    }
}
