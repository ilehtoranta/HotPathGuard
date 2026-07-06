# HotPathGuard

HotPathGuard is a small Roslyn analyzer package for code where accidental managed allocations are correctness bugs, not just performance smells.

Mark a type or member with `[HotPath]`, and the analyzer reports common allocation-prone constructs inside that hot path. Cold or diagnostic helpers near a hot path can be marked with `[HotPathAllocationAllowed("reason")]`, but hot paths may not call those helpers.

## Packages

| Package | Purpose |
| --- | --- |
| `HotPathGuard.Abstractions` | Public attributes used by libraries and applications. |
| `HotPathGuard.Analyzers` | Roslyn analyzer package that reports `HPG` diagnostics. |

## Example

```csharp
using HotPathGuard;

[HotPath]
public sealed class AudioMixer
{
    public void MixFrame(ReadOnlySpan<float> input, Span<float> output)
    {
        // HPG001: reference-type allocation in a hot path.
        var scratch = new float[1024];
    }
}
```

## Diagnostics

| ID | Title |
| --- | --- |
| `HPG001` | Hot path contains an allocation |
| `HPG002` | Hot path calls an allocating API |
| `HPG003` | Hot path creates a compiler state machine |
| `HPG004` | Allocation allowance requires a reason |
| `HPG005` | Hot path calls a cold allocation-allowed member |
| `HPG006` | Hot path method exceeds branch complexity limit |

## Branch Budgets

`HotPathAttribute.MaxBranches` can put a simple branch-count budget on a hot-path method:

```csharp
[HotPath(MaxBranches = 2)]
public int SelectVoice(int a, int b, int c)
{
    if (a > 0) return a;
    if (b > 0) return b;
    if (c > 0) return c; // HPG006
    return 0;
}
```

## Philosophy

HotPathGuard is a guardrail, not a profiler or a formal allocation proof. It catches common source-level mistakes in code that you already know is hot: object and array creation, closures, method-group-to-delegate conversions, boxing, interpolated strings, LINQ and other known allocating APIs, async and iterator state machines, and calls into explicitly allocation-allowed members.

## Build

Requires the .NET 10 SDK.

```powershell
dotnet build .\HotPathGuard.slnx
dotnet test .\HotPathGuard.slnx
```
