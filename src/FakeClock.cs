namespace Philiprehberger.Clock;

/// <summary>
/// Test double for <see cref="IClock"/> that allows manual control of time.
/// Thread-safe for use in concurrent test scenarios.
/// </summary>
public sealed class FakeClock : IClock
{
    private readonly object _lock = new();
    private DateTimeOffset _currentTime;

    /// <summary>
    /// Initializes a new instance of <see cref="FakeClock"/> set to the specified time.
    /// </summary>
    /// <param name="initialTime">The initial time for the clock.</param>
    public FakeClock(DateTimeOffset initialTime)
    {
        _currentTime = initialTime;
    }

    /// <summary>
    /// Gets or sets the amount of time to automatically advance after each read of <see cref="Now"/> or <see cref="UtcNow"/>.
    /// Defaults to <see cref="TimeSpan.Zero"/> (no auto-advance).
    /// </summary>
    public TimeSpan AutoAdvance { get; set; } = TimeSpan.Zero;

    /// <inheritdoc />
    public DateTimeOffset Now
    {
        get
        {
            lock (_lock)
            {
                var value = _currentTime;
                _currentTime = _currentTime.Add(AutoAdvance);
                return value;
            }
        }
    }

    /// <inheritdoc />
    public DateTimeOffset UtcNow
    {
        get
        {
            lock (_lock)
            {
                var value = _currentTime.ToUniversalTime();
                _currentTime = _currentTime.Add(AutoAdvance);
                return value;
            }
        }
    }

    /// <inheritdoc />
    public DateOnly Today
    {
        get
        {
            lock (_lock)
            {
                return DateOnly.FromDateTime(_currentTime.DateTime);
            }
        }
    }

    /// <summary>
    /// Advances the clock by the specified duration.
    /// </summary>
    /// <param name="duration">The amount of time to advance.</param>
    public void Advance(TimeSpan duration)
    {
        lock (_lock)
        {
            _currentTime = _currentTime.Add(duration);
        }
    }

    /// <summary>
    /// Sets the clock to the specified time.
    /// </summary>
    /// <param name="time">The new time for the clock.</param>
    public void SetTime(DateTimeOffset time)
    {
        lock (_lock)
        {
            _currentTime = time;
        }
    }
}
