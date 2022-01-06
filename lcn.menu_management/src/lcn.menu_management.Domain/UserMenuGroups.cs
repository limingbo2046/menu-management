using System;
using Volo.Abp.Domain.Entities;

namespace lcn.menu_management
{
    public class UserMenuGroups : Entity<Guid>
    {
        public Guid? TenantId { get; protected set; }
        public Guid UserId { get; protected set; }
        public Guid MenuGroupId { get; protected set; }
        public MenuGroup MenuGroup { get; protected set; }
        protected UserMenuGroups()
        {

        }
        public UserMenuGroups(Guid groupId, Guid userId, Guid? tenantId = null)
        {
            MenuGroupId = groupId;
            UserId = userId;
            TenantId = tenantId;
        }
    }
}
