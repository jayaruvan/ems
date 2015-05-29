
using EMS.DAL.Contract;
using EMS.DAL.EntityConfiguration;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

using EMS.Core.Entities;

namespace EMS.DAL
{
    public class UnitOfWork : DbContext, IQueryableUnitOfWork
    {
        #region Constructor

        public UnitOfWork()
            : base("name=EMS.DAL.UnitOfWork")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = false;
        }

        #endregion Constructor

        #region IDbSet Members





        #region location 

        IDbSet<Province> _province;
        public IDbSet<Province> Province
        {
            get
            {
                if (_province == null)
                    _province = base.Set<Province>();

                return _province;
            }
        }


        IDbSet<Area> _areaOffice;
        public IDbSet<Area> AreaOffice
        {
            get
            {
                if (_areaOffice == null)
                    _areaOffice = base.Set<Area>();

                return _areaOffice;
            }
        }

        IDbSet<SubStation> _subStation;
        public IDbSet<SubStation> SubStation
        {
            get
            {
                if (_subStation == null)
                    _subStation = base.Set<SubStation>();

                return _subStation;
            }
        }

        IDbSet<Supplier> _supplier;
        public IDbSet<Supplier> Supplier
        {
            get
            {
                if (_supplier == null)
                    _supplier = base.Set<Supplier>();

                return _supplier;
            }
        }

        IDbSet<Meter> _meter;
        public IDbSet<Meter> Meter
        {
            get
            {
                if (_meter == null)
                    _meter = base.Set<Meter>();

                return _meter;
            }
        }

        IDbSet<MeterReading> _meterReading;
        public IDbSet<MeterReading> MeterReading
        {
            get
            {
                if (_meterReading == null)
                    _meterReading = base.Set<MeterReading>();

                return _meterReading;
            }
        }


        IDbSet<Report> _report;
        public IDbSet<Report> Report
        {
            get
            {
                if (_report == null)
                    _report = base.Set<Report>();

                return _report;
            }
        }

        #endregion

        #endregion

        #region IQueryableUnitOfWork Members

        public DbSet<T> CreateSet<T>()
            where T : class
        {
            return base.Set<T>();
        }

        public void Attach<T>(T item)
            where T : class
        {
            //attach and set as unchanged
            base.Entry<T>(item).State = System.Data.EntityState.Unchanged;
        }

        public void SetModified<T>(T item)
            where T : class
        {
            //this operation also attach item in object state manager
            base.Entry<T>(item).State = System.Data.EntityState.Modified;
        }
        public void ApplyCurrentValues<T>(T original, T current)
            where T : class
        {
            //if it is not attached, attach original and set current values
            base.Entry<T>(original).CurrentValues.SetValues(current);
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);

        }

        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = System.Data.EntityState.Unchanged);
        }

        public IEnumerable<T> ExecuteQuery<T>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<T>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion

        #region DbContext Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {



            //Add entity configurations in a structured way using 'TypeConfiguration’ classes

            modelBuilder.Configurations.Add(new ProvinceConfiguration());
            modelBuilder.Configurations.Add(new AreaConfiguration());
            modelBuilder.Configurations.Add(new SubStationConfiguration());
            modelBuilder.Configurations.Add(new SupplierConfiguration());
            modelBuilder.Configurations.Add(new MeterConfiguration());
            modelBuilder.Configurations.Add(new MeterReadingConfiguration());
            modelBuilder.Configurations.Add(new ReportConfiguration());

            //Remove unused conventions
            // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            /**
            //modelBuilder.Entity<aspnet_UsersInRoles>().HasMany(i => i.Users).WithRequired().WillCascadeOnDelete(false);
            modelBuilder.Entity<Province>().HasMany(i => i.Areas).WithRequired().WillCascadeOnDelete(false);
            modelBuilder.Entity<Province>().HasRequired(c => c.Areas).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Province>().HasMany(i => i.SubStations).WithRequired().WillCascadeOnDelete(false);
            modelBuilder.Entity<Province>().HasRequired(c => c.SubStations).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Area>().HasMany(i => i.SubStations).WithRequired().WillCascadeOnDelete(false);
            modelBuilder.Entity<Area>().HasRequired(c => c.SubStations).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<SubStation>().HasMany(i => i.Meters).WithRequired().WillCascadeOnDelete(false);
            modelBuilder.Entity<SubStation>().HasRequired(c => c.Meters).WithMany().WillCascadeOnDelete(false);

           
            modelBuilder.Entity<Meter>().HasMany(i => i.MeterReadings).WithRequired().WillCascadeOnDelete(false);
            modelBuilder.Entity<Meter>().HasRequired(c => c.MeterReadings).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Report>().HasMany(i => i.MeterReadings).WithRequired().WillCascadeOnDelete(false);
            modelBuilder.Entity<Report>().HasRequired(c => c.MeterReadings).WithMany().WillCascadeOnDelete(false);


            modelBuilder.Entity<Supplier>().HasMany(i => i.Meters).WithRequired().WillCascadeOnDelete(false);
            modelBuilder.Entity<Supplier>().HasRequired(c => c.Meters).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Supplier>().HasMany(i => i.Reports).WithRequired().WillCascadeOnDelete(false);
            modelBuilder.Entity<Supplier>().HasRequired(c => c.Reports).WithMany().WillCascadeOnDelete(false);
            

            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
             * **/
        }
        #endregion

    }
}
