﻿namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            HocVus = new HashSet<HocVu>();
        }

        public int UserID { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }
		[DisplayName("Đơn vị")]
		public int? DonViID { get; set; }
		[DisplayName("Lớp")]
		public int? LopID { get; set; }

        [StringLength(75)]
		[DisplayName("Tên người dùng")]
		public string UserName { get; set; }
		[DisplayName("Vai trò")]
		public int? VaiTroID { get; set; }

        public virtual DonVi DonVi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HocVu> HocVus { get; set; }

        public virtual Lop Lop { get; set; }

        public virtual VaiTro VaiTro { get; set; }
    }
}
