using Microsoft.Extensions.DependencyInjection;

namespace Philiprehberger.Clock;

/// <summary>
/// Extension methods for registering clock services with <see cref="IServiceCollection"/>.
/// </summary>
public static class ClockServiceCollectionExtensions
{
    /// <summary>
    /// Registers <see cref="SystemClock"/> as the <see cref="IClock"/> implementation (singleton).
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddClock(this IServiceCollection services)
    {
        services.AddSingleton<IClock, SystemClock>();
        return services;
    }

    /// <summary>
    /// Registers a <see cref="FakeClock"/> as the <see cref="IClock"/> implementation (singleton).
    /// Intended for use in tests.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="initialTime">The initial time for the fake clock.</param>
    /// <returns>The registered <see cref="FakeClock"/> instance for test manipulation.</returns>
    public static FakeClock AddFakeClock(this IServiceCollection services, DateTimeOffset initialTime)
    {
        var fakeClock = new FakeClock(initialTime);
        services.AddSingleton<IClock>(fakeClock);
        return fakeClock;
    }
}
