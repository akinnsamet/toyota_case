using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Toyota.Shared.Validators;
using Toyota.Shared.Validators.LanguageManager;

namespace Toyota.Application.Validators
{
    public static class ValidatorConfiguration
    {
        public static IServiceCollection AddValidatorConfigurationLayer(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidationFilter));
            }).AddFluentValidation(c =>
            {
                c.ImplicitlyValidateChildProperties = true;
                c.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
                c.ValidatorOptions.LanguageManager =new FluentLanguageManager();           
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}
