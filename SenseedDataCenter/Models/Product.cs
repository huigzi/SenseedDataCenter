using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SenseedDataCenter.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "设备序列号")]
        public string SerialId { get; set; }

        [Display(Name = "软件版本")]
        public float SoftWareRev { get; set; }

        [Display(Name = "硬件版本")]
        public float HardWareRev { get; set; }

        [Display(Name = "设备所在地")]
        public string Locate { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "设备类型")]
        public Category Category { get; set; }

        public string UserName { get; set; }
    }
}
