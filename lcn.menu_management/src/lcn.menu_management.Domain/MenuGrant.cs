using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace lcn.menu_management
{
    /// <summary>
    /// 用户和菜单&按钮的关系
    /// 组成得到一个用户前端能使用业务菜单和按钮
    /// </summary>
    public class MenuGrant : Entity<Guid>
    {
        public Guid? TenantId { get; set; }
        /// <summary>
        /// 菜单的ID
        /// </summary>
        public Guid MenuId { get; set; }

        /// <summary>
        /// 拥有者ID(菜单组ID，用户ID）
        /// </summary>
        public Guid OwnerId { get; set; }
        /// <summary>
        /// 拥有者类型
        /// </summary>
        public string OwnerProvider { get; set; }
        public MenuGrant()
        {

        }
        public MenuGrant(Guid id, Guid ownerId, Guid menuId,string ownerProvider) : base(id)
        {
            OwnerId = ownerId;
            MenuId = menuId;
            OwnerProvider = ownerProvider;
        }
    }
}
