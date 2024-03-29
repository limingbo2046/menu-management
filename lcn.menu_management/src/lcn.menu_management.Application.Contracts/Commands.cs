﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcn.menu_management
{
    /// <summary>
    /// 设置菜单
    /// </summary>
    public class GrantMenu
    {
        /// <summary>
        /// 菜单拥有者ID
        /// </summary>
        public Guid OwnerId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Provider { get; set; }
        /// <summary>
        /// 菜单
        /// </summary>
        public MenuTreeDto[] MenuItems { get; set; }
    }
    public class AddUser2MenuGroup
    {
        [Required]
        public Guid UserId { get; set; }
        public Guid? TenantId { get; set; }
        /// <summary>
        /// 修改的菜单组集合
        /// </summary>
        public Guid[] MenuGroupIds { get; set; }
    }
    /// <summary>
    /// 为菜单指定租户
    /// </summary>
    public class AssignedTenant2MenuItem
    {
        public Guid RootMenuItemId { get; set; }
        public Guid TenantId { get; set; }
        public Guid? FromTenantId { get; set; }//可以迁移菜单
    }
}
