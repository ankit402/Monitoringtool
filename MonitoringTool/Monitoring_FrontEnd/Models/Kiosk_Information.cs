using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Monitoring_FrontEnd.Models
{
    public class Kiosk_Information
    {
        [Key]
        public int Id { get; set; }
        public string SerialNo { get; set; }
        public string Firmware { get; set; }
        public string Branch { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }

      

    }
}
