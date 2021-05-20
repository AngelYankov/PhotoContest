using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotoContest.Services.Services.SecuritySettings
{
    public class AddRoleModel
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
