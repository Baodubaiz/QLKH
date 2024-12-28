namespace QLKH.MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LopHoc")]
    public partial class LopHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LopHoc()
        {
            HocViens = new HashSet<HocVien>();
        }

        [Key]
        [StringLength(10)]
        public string MaLop { get; set; }

        [Required]
        [StringLength(255)]
        public string TenLop { get; set; }

        [Required]
        [StringLength(10)]
        public string MaKhoaHoc { get; set; }

        [Required]
        [StringLength(10)]
        public string MaGiangVien { get; set; }

        [Required]
        [StringLength(15)]
        public string NgayBatDau { get; set; }

        [StringLength(15)]
        public string NgayKetThuc { get; set; }

        public int SoLuongHocVien { get; set; }

        public virtual GiangVien GiangVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HocVien> HocViens { get; set; }

        public virtual KhoaHoc KhoaHoc { get; set; }
    }
}
