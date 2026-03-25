using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace Philiprehberger.Clock.Tests;

public class ClockServiceCollectionExtensionsTests
{
    [Fact]
    public void AddClock_RegistersSystemClockAsSingleton()
    {
        var services = new ServiceCollection();

        services.AddClock();

        var provider = services.BuildServiceProvider();
        var clock = provider.GetRequiredService<IClock>();
        Assert.IsType<SystemClock>(clock);
    }

    [Fact]
    public void AddClock_ReturnsSameInstance()
    {
        var services = new ServiceCollection();
        services.AddClock();
        var provider = services.BuildServiceProvider();

        var first = provider.GetRequiredService<IClock>();
        var second = provider.GetRequiredService<IClock>();

        Assert.Same(first, second);
    }

    [Fact]
    public void AddClock_ReturnsServiceCollection()
    {
        var services = new ServiceCollection();

        var result = services.AddClock();

        Assert.Same(services, result);
    }

    [Fact]
    public void AddFakeClock_RegistersFakeClockAsSingleton()
    {
        var services = new ServiceCollection();
        var initialTime = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);

        services.AddFakeClock(initialTime);

        var provider = services.BuildServiceProvider();
        var clock = provider.GetRequiredService<IClock>();
        Assert.IsType<FakeClock>(clock);
    }

    [Fact]
    public void AddFakeClock_ReturnsFakeClockInstance()
    {
        var services = new ServiceCollection();
        var initialTime = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);

        var fakeClock = services.AddFakeClock(initialTime);

        Assert.NotNull(fakeClock);
        Assert.Equal(initialTime, fakeClock.Now);
    }

    [Fact]
    public void AddFakeClock_ResolvedClockMatchesReturnedInstance()
    {
        var services = new ServiceCollection();
        var initialTime = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var fakeClock = services.AddFakeClock(initialTime);

        var provider = services.BuildServiceProvider();
        var resolved = provider.GetRequiredService<IClock>();

        Assert.Same(fakeClock, resolved);
    }
}
