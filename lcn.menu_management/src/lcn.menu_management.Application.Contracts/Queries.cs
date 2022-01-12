using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lcn.menu_management
{
    /// <summary>
    /// 查询
    /// </summary>
    public class GetCurrentUserMenus
    {

    }
    public class GetCurrentUserButtons
    {
        public Guid? TreeNodeId { get; set; }
    }
    public class GetMenuTree
    {
        [Required]
        public Guid OwnerId { get; set; }
        /// <summary>
        /// 租户
        /// </summary>
        public Guid? TenantId { get; set; }
    }

    public class GetMenuGroupByUser
    {
        public Guid UserId { get; set; }
        public Guid? TenantId { get; set; }
    }

}
