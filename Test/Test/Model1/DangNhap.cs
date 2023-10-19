using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Test.Model1
{
    [Table("DangNhap")]
    public partial class  DangNhap
    {
        [Key]
        [StringLength(20)]
        public string TaiKhoang { get; set; }
        [Required]
        [StringLength(10)]
        public string MatKhau { get; set; }

    }
}
