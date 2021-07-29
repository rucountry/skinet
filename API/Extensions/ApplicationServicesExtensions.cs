using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.Configure<ApiBehaviorOptions>(options => {
         options.InvalidModelStateResponseFactory = actionContext => {
           var errors = actionContext.ModelState
                                     .Where(e => e.Value.Errors.Count > 0)
                                     .SelectMany(x => x.Value.Errors)
                                     .Select(x => x.ErrorMessage).ToArray();
           var errorResponce = new ApiValidationErrorResponse()
           {
             Errors = errors
           };
           return new BadRequestObjectResult(errorResponce);
         };
      });

      return services;
        }
    }
}