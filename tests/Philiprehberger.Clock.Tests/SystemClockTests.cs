using Xunit;
namespace Philiprehberger.Clock.Tests;

public class SystemClockTests
{
    [Fact]
    public void Now_ReturnsCurrentLocalTime()
    {
        var clock = new SystemClock();
        var before = DateTimeOffset.Now;

        var result = clock.Now;

        var after = DateTimeOffset.Now;
        Assert.InRange(result, before, after);
    }

    [Fact]
    public void UtcNow_ReturnsCurrentUtcTime()
    {
        var clock = new SystemClock();
        var before = DateTimeOffset.UtcNow;

        var result = clock.UtcNow;

        var after = DateTimeOffset.UtcNow;
        Assert.InRange(result, before, after);
    }

    [Fact]
    public void Today_ReturnsCurrentDate()
    {
        var clock = new SystemClock();

        var result = clock.Today;

        Assert.Equal(DateOnly.FromDateTime(DateTime.Today), result);
    }

    [Fact]
    public void SystemClock_ImplementsIClock()
    {
        var clock = new SystemClock();

        Assert.IsAssignableFrom<IClock>(clock);
    }
}
