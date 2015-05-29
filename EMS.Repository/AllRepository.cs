using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Core.Entities;
using EMS.DAL;

namespace EMS.Repository
{

    public class ProvinceRepository : Repository<Province>, IProvinceRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public ProvinceRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public AreaRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }

    public class SubStationRepository : Repository<SubStation>, ISubStationRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public SubStationRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }

    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public SupplierRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }

    public class MeterRepository : Repository<Meter>, IMeterRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public MeterRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }

    public class MeterReadingRepository : Repository<MeterReading>, IMeterReadingRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public MeterReadingRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }

    public class ReportRepository : Repository<Report>, IReportRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public ReportRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }

}
