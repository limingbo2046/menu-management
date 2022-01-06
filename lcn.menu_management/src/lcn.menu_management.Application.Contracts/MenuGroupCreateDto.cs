using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcn.menu_management
{
    public class MenuGroupCreateDto
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
    }
}
