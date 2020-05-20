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

        public DateTime? NgayTao { get; set; }

        [StringLength(250)]
        public string YeuCauThem { get; set; }

        public bool? TinhTrang { get; set; }

        public int? ParentID { get; set; }

        public int? ChuyenVienID { get; set; }

        [Column(TypeName = "date")]
		[DataType(DataType.Date)]
		public DateTime? NgayHen { get; set; }

        public int? DanhMucID { get; set; }

        public int? UserID { get; set; }

        public int? DonViID { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        public virtual DonVi DonVi { get; set; }

        public virtual User User { get; set; }
    }
}
