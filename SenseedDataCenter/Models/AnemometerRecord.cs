using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SenseedDataCenter.Models
{
    public class AnemometerRecord
    {
        public int Id { get; set; }

        [Display(Name = "东西向风速")]
        public float E2W { get; set; }

        [Display(Name = "南北向风速")]
        public float N2S { get; set; }

        [Display(Name = "风速")]
        public float Velocity { get; set; }

        [Display(Name = "风向")]
        public float Direction { get; set; }

        [Display(Name = "声速")]
        public float SoundVelocity { get; set; }

        public string ProductId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "检测时刻")]
        [DataType(DataType.DateTime)]
        public DateTime RecordDate { get; set; }
    }
}
