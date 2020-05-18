namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VaiTroChucNangPhanMem")]
    public partial class VaiTroChucNangPhanMem
    {
        [Key]
        public int VaiTroChucNangID { get; set; }

        public int? ChucNangID { get; set; }

        public bool? xem { get; set; }

        public bool? them { get; set; }

        public bool? sua { get; set; }

        public bool? xoa { get; set; }

        public int? VaiTroID { get; set; }

        public virtual ChucNangPhanMem ChucNangPhanMem { get; set; }

        public virtual VaiTro VaiTro { get; set; }
    }
}
