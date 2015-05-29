
using EMS.Common.Logging;
using EMS.Common.Validator;
using EMS.Core.Entities;
using EMS.DAL;
using EMS.DAL.Contract;
using EMS.Manager.Contract;
using EMS.Manager.Implementation;
using EMS.Repository;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace EMS.Web.Dependency
{
    public class DependencyConventions : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                                .BasedOn<IController>()
                                .LifestyleTransient());

            container.Register(

                        Component.For<IQueryableUnitOfWork, UnitOfWork>().ImplementedBy<UnitOfWork>(),

                        Component.For<IProvinceRepository, ProvinceRepository>().ImplementedBy<ProvinceRepository>(),
                        Component.For<IAreaRepository, AreaRepository>().ImplementedBy<AreaRepository>(),
                        Component.For<ISubStationRepository, SubStationRepository>().ImplementedBy<SubStationRepository>(),
                        Component.For<ISupplierRepository, SupplierRepository>().ImplementedBy<SupplierRepository>(),
                        Component.For<IMeterRepository, MeterRepository>().ImplementedBy<MeterRepository>(),
                        Component.For<IMeterReadingRepository, MeterReadingRepository>().ImplementedBy<MeterReadingRepository>(),
                        Component.For<IReportRepository, ReportRepository>().ImplementedBy<ReportRepository>(),

                        Component.For<ILocationManager>().ImplementedBy<LocationManager>(),

                        AllTypes.FromThisAssembly().BasedOn<IHttpController>().LifestyleTransient()

                        )
                       .AddFacility<LoggingFacility>(f => f.UseLog4Net());

            LoggerFactory.SetCurrent(new TraceSourceLogFactory());
            EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());


        }

    }
}