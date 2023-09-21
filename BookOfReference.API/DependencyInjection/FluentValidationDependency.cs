using FluentValidation.AspNetCore;
using System.Reflection;

namespace BookOfReference.API.DependencyInjection
{
    public static class FluentValidationDependency
    {
        
        public static IMvcBuilder AddFluentDependency(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddFluentValidation(options =>
            {
                options.ImplicitlyValidateChildProperties = true;
                options.ImplicitlyValidateRootCollectionElements = true;
                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });
        }
    }
}
