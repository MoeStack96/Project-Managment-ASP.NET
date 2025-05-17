#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Project_management_system.Areas.Identity.Data;


public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }    
}

