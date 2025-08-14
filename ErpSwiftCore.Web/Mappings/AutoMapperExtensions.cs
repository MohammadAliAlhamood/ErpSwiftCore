using System.Reflection;

namespace ErpSwiftCore.Web.Mappings
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
