using System.Text.Json.Serialization;

namespace BookOfReference.API.DependencyInjection
{
    public static class JsonConverterDependency
    {
        public static IMvcBuilder AddJsonConverterDependency(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }
    }
}
