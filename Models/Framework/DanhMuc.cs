namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMuc")]
    public partial class DanhMuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhMuc()
        {
            HocVus = new HashSet<HocVu>();
        }

        public int DanhMucID { get; set; }

        [Required]
        [StringLength(50)]
		[DisplayName("Tên danh mục")]
		public string TenDanhMuc { get; set; }
		[DisplayName("Đơn vị")]
		public int? DonViID { get; set; }

        public virtual DonVi DonVi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HocVu> HocVus { get; set; }
    }
}
