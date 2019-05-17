namespace AnnonsService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Annonser")]
    public partial class Annonser
    {
        [Key]
        public int annonsID { get; set; }

        public int saljarID { get; set; }

        [Required]
        [StringLength(50)]
        public string annonsNamn { get; set; }

        public double pris { get; set; }

        [Required]
        public string beskrivning { get; set; }

        [Required]
        [StringLength(50)]
        public string kategori { get; set; }

        [Column(TypeName = "date")]
        public DateTime datum { get; set; }

        [Required]
        [StringLength(50)]
        public string status { get; set; }

        [Required]
        [StringLength(50)]
        public string betalningsmetod { get; set; }

        public string bild { get; set; }

        public string koparID { get; set; }

        [Required]
        public string ort { get; set; }

        [Required]
        public string adress { get; set; }

        public int postNr { get; set; }
    }
}
