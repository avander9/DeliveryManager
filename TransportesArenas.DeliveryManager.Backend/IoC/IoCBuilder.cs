using Autofac;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class IoCBuilder
    {
        internal static IContainer Build()  
        {  
            var builder = new ContainerBuilder();  
            RegisterTypes(builder);  
            return builder.Build();  
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<PdfWrapper>().As<IPdfWrapper>();

        }
    }
}