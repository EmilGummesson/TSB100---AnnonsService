namespace AnnonsService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AnnonsModel : DbContext
    {
        public AnnonsModel()
            : base("name=AnnonsModel1")
        {
        }

        public virtual DbSet<Annonser> Annonser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
