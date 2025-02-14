using Microsoft.Extensions.Configuration;

namespace CleanySharp.Configuration.Extensions;

public static class Extensions
{
    public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionName) where TOptions : class, new()
    {
        var options = new TOptions();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);
        
        return options;
    }
}