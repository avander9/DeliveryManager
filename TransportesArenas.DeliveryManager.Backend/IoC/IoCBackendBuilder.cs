using Autofac;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public static class IoCBackendBuilder
    {
        public static IContainer Build()  
        {  
            var builder = new ContainerBuilder();  
            RegisterTypes(builder);  
            return builder.Build();  
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<DeliveryManagerLogger>().As<IDeliveryManagerLogger>();
            builder.RegisterType<DeliveriesMissingReportExcelGenerator>();
            builder.RegisterType<PdfManager>().As<IPdfManager>();
            builder.RegisterType<PdfWrapper>().As<IPdfWrapper>();
            builder.RegisterType<ExcelReader>().As<IExcelReader>();
            builder.RegisterType<DelivaryManagerProcessRequest>().As<IDelivaryManagerProcessRequest>();
            builder.RegisterType<DeliveryProcessManager>().As<IDeliveryProcessManager>();
            builder.RegisterType<ExcelReportBuilder>().As<IExcelReportBuilder>();
        }
    }
}