namespace Philiprehberger.Clock;

/// <summary>
/// Real implementation of <see cref="IClock"/> that delegates to <see cref="DateTimeOffset"/>.
/// </summary>
public sealed class SystemClock : IClock
{
    /// <inheritdoc />
    public DateTimeOffset Now => DateTimeOffset.Now;

    /// <inheritdoc />
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

    /// <inheritdoc />
    public DateOnly Today => DateOnly.FromDateTime(DateTime.Today);
}
