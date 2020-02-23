using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIcore.Models
{
    public class User_Function
    {
        [Key]
        public ICollection<Function> Functions { get; set; }
        [Key]
        public ICollection<User> Users { get; set; }
    }
}
