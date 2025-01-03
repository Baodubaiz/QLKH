namespace QLKH.MODEL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhoaHoc")]
    public partial class KhoaHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhoaHoc()
        {
            LopHocs = new HashSet<LopHoc>();
        }

        [Key]
        [StringLength(10)]
        public string MaKhoaHoc { get; set; }

        [Required]
        [StringLength(255)]
        public string TenKhoaHoc { get; set; }

        [Required]
        [StringLength(100)]
        public string ThoiGianHoc { get; set; }

        [StringLength(255)]
        public string LichHoc { get; set; }

        [Required]
        [StringLength(10)]
        public string MaGiangVien { get; set; }

        public virtual GiangVien GiangVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LopHoc> LopHocs { get; set; }
    }
}
