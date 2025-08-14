using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyAllConfigurations(this ModelBuilder modelBuilder)
        {
            // يطبّق IEntityTypeConfiguration من هذا التجميع
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // يطبّق IEntityTypeConfiguration الخاصة بالـ Seeds من مجلد Seeds
            var seedsAssembly = Assembly.GetExecutingAssembly();
            modelBuilder.ApplyConfigurationsFromAssembly(seedsAssembly,
                x => x.Namespace != null && x.Namespace.Contains(".Seeds."));
        }
    }
}