using System;

namespace HotPathGuard;

/// <summary>
/// Marks a type or member as latency-sensitive code that should avoid managed allocations.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Property)]
public sealed class HotPathAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the maximum number of branch points allowed in the method body.
    /// Zero disables the branch budget check.
    /// </summary>
    public int MaxBranches { get; set; }
}
