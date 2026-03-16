namespace Philiprehberger.Clock;

/// <summary>
/// Abstraction over system time for testable time-dependent code.
/// </summary>
public interface IClock
{
    /// <summary>Gets the current local date and time.</summary>
    DateTimeOffset Now { get; }

    /// <summary>Gets the current UTC date and time.</summary>
    DateTimeOffset UtcNow { get; }

    /// <summary>Gets the current date.</summary>
    DateOnly Today { get; }
}
