using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace lcn.menu_management
{
    /// <summary>
    /// 菜单组（可以按角色看待）
    /// </summary>
    public class MenuGroup : Entity<Guid>
    {
        public Guid? TenantId { get; set; }
        /// <summary>
        /// 分组名（key)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 大写名称
        /// </summary>
        public string NormalizedName { get; set; }

        
        protected MenuGroup()
        {

        }
        public MenuGroup(Guid id,string name,string normalizeName,Guid? tenantId=null):base(id)
        {
            this.Name = name;
            this.NormalizedName = normalizeName;
            this.TenantId = tenantId;
        }
    }
}
