using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SenseedDataCenter.Models;

namespace SenseedDataCenter.ViewModel
{
    public class ProductModel
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

        [Display(Name = "设备类型")]
        public int Category { get; set; }

        [Display(Name = "所属用户")]
        public string UserName { get; set; }

    }
}
