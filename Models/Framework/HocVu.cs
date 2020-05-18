namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HocVu")]
    public partial class HocVu
    {
        public int HocVuID { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayTao { get; set; }

        [Column(TypeName = "ntext")]
        public string YeuCauThem { get; set; }

        [Required]
        [StringLength(50)]
        public string TinhTrang { get; set; }

        public int ParentID { get; set; }

        public int ChuyenVienID { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayHen { get; set; }

        public int? DanhMucID { get; set; }

        public int? UserID { get; set; }

        public int? DonViID { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        public virtual DonVi DonVi { get; set; }

        public virtual User User { get; set; }
    }
}
