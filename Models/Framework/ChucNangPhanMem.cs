namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChucNangPhanMem")]
    public partial class ChucNangPhanMem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChucNangPhanMem()
        {
            VaiTroChucNangPhanMems = new HashSet<VaiTroChucNangPhanMem>();
        }

        [Key]
        public int ChucNangID { get; set; }

        [StringLength(50)]
        public string TenChucNang { get; set; }

        [StringLength(50)]
        public string TenTrang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VaiTroChucNangPhanMem> VaiTroChucNangPhanMems { get; set; }
    }
}
