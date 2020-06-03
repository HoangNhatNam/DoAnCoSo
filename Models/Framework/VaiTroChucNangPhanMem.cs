namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VaiTroChucNangPhanMem")]
    public partial class VaiTroChucNangPhanMem
    {
        [Key]
        public int VaiTroChucNangID { get; set; }
		[DisplayName("Tên chức năng")]
		public int? ChucNangID { get; set; }
		[DisplayName("Xem")]
		public bool? xem { get; set; }
		[DisplayName("Thêm")]
		public bool? them { get; set; }
		[DisplayName("Sửa")]
		public bool? sua { get; set; }
		[DisplayName("Xóa")]
		public bool? xoa { get; set; }
		[DisplayName("Vai trò")]
		public int? VaiTroID { get; set; }

        public virtual ChucNangPhanMem ChucNangPhanMem { get; set; }

        public virtual VaiTro VaiTro { get; set; }
    }
}
