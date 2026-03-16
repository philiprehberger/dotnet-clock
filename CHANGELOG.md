# Changelog

## 0.1.1 (2026-03-16)

- Fix: add NuGet publishing secret

## 0.1.0 (2026-03-15)

- Initial release
- `IClock` abstraction over `DateTime`/`DateTimeOffset`
- `SystemClock` for production use
- `FakeClock` test double with `Advance`, `SetTime`, and `AutoAdvance`
- DI extensions: `AddClock()` and `AddFakeClock()`
