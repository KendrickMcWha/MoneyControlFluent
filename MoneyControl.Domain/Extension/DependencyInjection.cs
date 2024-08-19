using Microsoft.Extensions.DependencyInjection;

namespace MoneyControl.Domain.Extension;
public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}
