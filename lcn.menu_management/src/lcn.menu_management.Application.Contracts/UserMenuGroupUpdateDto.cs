using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcn.menu_management
{
    public class UserMenuGroupUpdateDto
    {
        [Required]
        public string[] MenuGroupIds { get; set; }
    }
}
