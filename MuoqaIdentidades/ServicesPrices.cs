using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuoqaIdentidades
{
    public class ServicesPrices
    {
        [Key]
        public int? ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServicePrice { get; set; }
        public string ServiceActive { get; set; }
    }
}
