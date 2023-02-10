using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Commands
{
    public class TraceRouteCommand
    {
        [Required]
        [MaxLength(3)]

        public string From { get; set; }

        [Required]
        [MaxLength(3)]
        public string To { get; set; }
    }
}
