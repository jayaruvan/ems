using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EMS.Core.Entities
{
    public interface IProvinceRepository : IRepository<Province>
    {
    }

    public interface IAreaRepository : IRepository<Area>
    {
    }

    public interface ISubStationRepository : IRepository<SubStation>
    {
    }

    public interface IMeterRepository : IRepository<Meter>
    {
    }

    public interface IMeterReadingRepository : IRepository<MeterReading>
    {
    }

    public interface ISupplierRepository : IRepository<Supplier>
    {
    }

    public interface IReportRepository : IRepository<Report>
    {
    }

}
