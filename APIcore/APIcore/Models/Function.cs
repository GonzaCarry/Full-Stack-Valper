using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIcore.Models
{
    public class Function
    {
        [Key]
        public string Id { get; set; }

        public string Server { get; set; }

        public byte Action { get; set; }

        public string Description { get; set; }

    }
}
