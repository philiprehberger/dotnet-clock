# Philiprehberger.Clock

[![CI](https://github.com/philiprehberger/dotnet-clock/actions/workflows/ci.yml/badge.svg)](https://github.com/philiprehberger/dotnet-clock/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Philiprehberger.Clock.svg)](https://www.nuget.org/packages/Philiprehberger.Clock)
[![License](https://img.shields.io/github/license/philiprehberger/dotnet-clock)](LICENSE)
[![Sponsor](https://img.shields.io/badge/sponsor-GitHub%20Sponsors-ec6cb9)](https://github.com/sponsors/philiprehberger)

Abstraction over DateTime/DateTimeOffset for testable time-dependent code with a fake clock for testing.

## Installation

```bash
dotnet add package Philiprehberger.Clock
```

## Usage

### Inject IClock into your services

```csharp
using Philiprehberger.Clock;

public class OrderService
{
    private readonly IClock _clock;

    public OrderService(IClock clock)
    {
        _clock = clock;
    }

    public Order PlaceOrder(string item)
    {
        return new Order(item, _clock.UtcNow);
    }
}
```

### Register with dependency injection

```csharp
using Philiprehberger.Clock;

// Production — uses real system time
builder.Services.AddClock();

// Testing — uses controllable fake time
builder.Services.AddFakeClock(new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero));
```

### Use SystemClock directly

```csharp
using Philiprehberger.Clock;

var clock = new SystemClock();
Console.WriteLine(clock.UtcNow);
Console.WriteLine(clock.Today);
```

### Use FakeClock in tests

```csharp
using Philiprehberger.Clock;

var start = new DateTimeOffset(2026, 6, 15, 10, 0, 0, TimeSpan.Zero);
var clock = new FakeClock(start);

// Advance time manually
clock.Advance(TimeSpan.FromHours(2));
Console.WriteLine(clock.UtcNow); // 2026-06-15 12:00:00

// Set exact time
clock.SetTime(new DateTimeOffset(2026, 12, 25, 0, 0, 0, TimeSpan.Zero));
Console.WriteLine(clock.Today); // 2026-12-25

// Auto-advance on each access
clock.AutoAdvance = TimeSpan.FromMinutes(5);
Console.WriteLine(clock.UtcNow); // 2026-12-25 00:00:00
Console.WriteLine(clock.UtcNow); // 2026-12-25 00:05:00
Console.WriteLine(clock.UtcNow); // 2026-12-25 00:10:00
```

## API

### `IClock`

| Member | Type | Description |
|--------|------|-------------|
| `Now` | `DateTimeOffset` | Current local date and time with offset |
| `UtcNow` | `DateTimeOffset` | Current UTC date and time with offset |
| `Today` | `DateOnly` | Current local date |

### `SystemClock`

Real implementation of `IClock` that delegates to `DateTimeOffset.Now`, `DateTimeOffset.UtcNow`, and `DateTime.Today`.

### `FakeClock`

| Member | Description |
|--------|-------------|
| `FakeClock(DateTimeOffset initialTime)` | Creates a fake clock at the given time |
| `Now` | Returns the current fake local time |
| `UtcNow` | Returns the current fake UTC time |
| `Today` | Returns the current fake date |
| `Advance(TimeSpan duration)` | Moves time forward by the specified duration |
| `SetTime(DateTimeOffset time)` | Sets the clock to an exact time |
| `AutoAdvance` | If set, time advances by this amount on each property access |

### DI Extensions

| Method | Description |
|--------|-------------|
| `AddClock()` | Registers `SystemClock` as `IClock` singleton |
| `AddFakeClock(DateTimeOffset? initialTime)` | Registers `FakeClock` as `IClock` singleton |

## Development

```bash
dotnet build src/Philiprehberger.Clock.csproj --configuration Release
```

## License

MIT
