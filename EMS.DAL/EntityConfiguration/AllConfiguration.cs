using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EMS.Core.Entities;

namespace EMS.DAL.EntityConfiguration
{
    class ProvinceConfiguration : EntityTypeConfiguration<Province>
    {
        public ProvinceConfiguration()
        {
            //configure table map
            ToTable("Province");

            HasKey(p => p.Id);
            //Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).HasMaxLength(25).IsRequired();


            //For the momoent make this optionsl --
            Property(p => p.CreatedDate).IsOptional();
            Property(p => p.AlteredDate).IsOptional();
            Property(p => p.IP).IsOptional();
            
            //HasRequired
            
        }
    }

    class AreaConfiguration : EntityTypeConfiguration<Area>
    {
        public AreaConfiguration()
        {
            //configure table map
            ToTable("Area");

            HasKey(p => p.Id);
            //Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).HasMaxLength(25).IsRequired();
            Property(p => p.ProvinceId).IsRequired();

            //For the momoent make this optionsl --
            Property(p => p.CreatedDate).IsOptional();
            Property(p => p.AlteredDate).IsOptional();
            Property(p => p.IP).IsOptional();

            
            //relationship mappint
            // 1..*
            this.HasRequired(pp => pp.Province)
                .WithMany(pp => pp.Areas)
                .HasForeignKey(pp => pp.ProvinceId)
                .WillCascadeOnDelete(false);
            
        }
    }



    class SubStationConfiguration : EntityTypeConfiguration<SubStation>
    {
        public SubStationConfiguration()
        {
            //configure table map
            ToTable("SubStation");

            HasKey(p => p.Id);
            //Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).HasMaxLength(25).IsRequired();
            
            Property(s => s.ProvinceId).IsRequired();
            Property(s => s.AreaId).IsRequired();

            //For the momoent make this optionsl --
            Property(p => p.CreatedDate).IsOptional();
            Property(p => p.AlteredDate).IsOptional();
            Property(p => p.IP).IsOptional();

            //relationship mappint
            // 1..*
            this.HasRequired(pp => pp.Province)
                .WithMany(pp => pp.SubStations)
                .HasForeignKey(pp => pp.ProvinceId)
                .WillCascadeOnDelete(false);

            this.HasRequired(pp => pp.Area)
                .WithMany(pp => pp.SubStations)
                .HasForeignKey(pp => pp.AreaId)
                .WillCascadeOnDelete(false);
        }
    }


    class SupplierConfiguration : EntityTypeConfiguration<Supplier>
    {
        public SupplierConfiguration()
        {
            //configure table map
            ToTable("Supplier");

            HasKey(p => p.Id);
            //Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).HasMaxLength(25).IsRequired();

            //For the momoent make this optionsl --
            Property(p => p.CreatedDate).IsOptional();
            Property(p => p.AlteredDate).IsOptional();
            Property(p => p.IP).IsOptional();

        }
    }

    class MeterConfiguration : EntityTypeConfiguration<Meter>
    {
        public MeterConfiguration()
        {
            //configure table map
            ToTable("Meter");

            HasKey(p => p.Id);
            //Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).HasMaxLength(25).IsRequired();
            Property(p => p.Serial).HasMaxLength(25).IsRequired();


            Property(s => s.SupplierId).IsRequired();
            Property(s => s.SubStationId).IsRequired();

            //For the momoent make this optionsl --
            Property(p => p.CreatedDate).IsOptional();
            Property(p => p.AlteredDate).IsOptional();
            Property(p => p.IP).IsOptional();


            this.HasRequired(pp => pp.Supplier)
                .WithMany(pp => pp.Meters)
                .HasForeignKey(pp => pp.SupplierId)
                .WillCascadeOnDelete(false);
            this.HasRequired(pp => pp.SubStation)
                .WithMany(pp => pp.Meters)
                .HasForeignKey(pp => pp.SubStationId)
                .WillCascadeOnDelete(false);

        }
    }


    class MeterReadingConfiguration : EntityTypeConfiguration<MeterReading>
    {
        public MeterReadingConfiguration()
        {
            //configure table map
            ToTable("MeterReading");

            HasKey(p => p.Id);
            //Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.ReadingDate).IsRequired();
            Property(p => p.DayValue).IsRequired();
            Property(p => p.PeakValue).IsRequired();
            Property(p => p.OffPeakValue).IsRequired();
            Property(p => p.CoincidentPeak).IsRequired();
            Property(p => p.MeterId).IsRequired();
            
            Property(p => p.ReportId).IsOptional();
            
            Property(p => p.Remarks).IsOptional();
            

            //For the momoent make this optionsl --
            Property(p => p.CreatedDate).IsOptional();
            Property(p => p.AlteredDate).IsOptional();
            Property(p => p.IP).IsOptional();

            this.HasRequired(pp => pp.Meter)
                .WithMany(pp => pp.MeterReadings)
                .HasForeignKey(pp => pp.MeterId)
                .WillCascadeOnDelete(false);
            this.HasRequired(pp => pp.Report)
                .WithMany(pp => pp.MeterReadings)
                .HasForeignKey(pp => pp.ReportId)
                .WillCascadeOnDelete(false);

        }
    }

    class ReportConfiguration : EntityTypeConfiguration<Report>
    {
        public ReportConfiguration()
        {
            //configure table map
            ToTable("Report");

            HasKey(p => p.Id);
           // Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).HasMaxLength(25).IsRequired();
            Property(p => p.ReportDate).IsRequired();
            Property(p => p.SupplierId).IsRequired();


            //For the momoent make this optionsl --
            Property(p => p.CreatedDate).IsOptional();
            Property(p => p.AlteredDate).IsOptional();
            Property(p => p.IP).IsOptional();
        }
    }

   
}
