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
            builder.RegisterType<PdfWrapper>().As<IPdfWrapper>();
            builder.RegisterType<DelivaryManagerProcessRequest>().As<IDelivaryManagerProcessRequest>();
            builder.RegisterType<DeliveryProcessManager>().As<IDeliveryProcessManager>();
            builder.RegisterType<DeliveryManagerLogger>().As<IDeliveryManagerLogger>();
        }
    }
}