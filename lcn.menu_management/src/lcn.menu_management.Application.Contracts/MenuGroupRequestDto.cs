using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace lcn.menu_management
{
    public class MenuGroupRequestDto : PagedAndSortedResultRequestDto
    {
        public string MenuGroupName { get; set; }
    }
}
