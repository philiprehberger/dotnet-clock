using Xunit;
namespace Philiprehberger.Clock.Tests;

public class FakeClockTests
{
    private static readonly DateTimeOffset FixedTime = new(2025, 6, 15, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Now_ReturnsInitialTime()
    {
        var clock = new FakeClock(FixedTime);

        var result = clock.Now;

        Assert.Equal(FixedTime, result);
    }

    [Fact]
    public void UtcNow_ReturnsInitialTimeInUtc()
    {
        var localTime = new DateTimeOffset(2025, 6, 15, 14, 0, 0, TimeSpan.FromHours(2));
        var clock = new FakeClock(localTime);

        var result = clock.UtcNow;

        Assert.Equal(localTime.ToUniversalTime(), result);
    }

    [Fact]
    public void Today_ReturnsDatePartOfCurrentTime()
    {
        var clock = new FakeClock(FixedTime);

        var result = clock.Today;

        Assert.Equal(new DateOnly(2025, 6, 15), result);
    }

    [Fact]
    public void Advance_MovesTimeForward()
    {
        var clock = new FakeClock(FixedTime);

        clock.Advance(TimeSpan.FromHours(3));

        Assert.Equal(FixedTime.AddHours(3), clock.Now);
    }

    [Fact]
    public void SetTime_ChangesCurrentTime()
    {
        var clock = new FakeClock(FixedTime);
        var newTime = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        clock.SetTime(newTime);

        Assert.Equal(newTime, clock.Now);
    }

    [Fact]
    public void AutoAdvance_AdvancesTimeAfterEachRead()
    {
        var clock = new FakeClock(FixedTime)
        {
            AutoAdvance = TimeSpan.FromMinutes(10)
        };

        var first = clock.Now;
        var second = clock.Now;
        var third = clock.Now;

        Assert.Equal(FixedTime, first);
        Assert.Equal(FixedTime.AddMinutes(10), second);
        Assert.Equal(FixedTime.AddMinutes(20), third);
    }

    [Fact]
    public void AutoAdvance_DefaultsToZero()
    {
        var clock = new FakeClock(FixedTime);

        Assert.Equal(TimeSpan.Zero, clock.AutoAdvance);

        var first = clock.Now;
        var second = clock.Now;

        Assert.Equal(first, second);
    }

    [Fact]
    public void FakeClock_ImplementsIClock()
    {
        var clock = new FakeClock(FixedTime);

        Assert.IsAssignableFrom<IClock>(clock);
    }

    [Fact]
    public void Advance_NegativeDuration_MovesTimeBackward()
    {
        var clock = new FakeClock(FixedTime);

        clock.Advance(TimeSpan.FromHours(-1));

        Assert.Equal(FixedTime.AddHours(-1), clock.Now);
    }
}
