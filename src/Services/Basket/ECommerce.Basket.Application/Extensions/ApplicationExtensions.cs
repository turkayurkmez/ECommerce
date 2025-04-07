using ECommerce.Common.Extensions;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
           
            services.AddCommonServices(Assembly.GetExecutingAssembly());    


        }
    }

}
