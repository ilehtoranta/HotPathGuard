using System;

namespace HotPathGuard;

/// <summary>
/// Allows managed allocations in a hot-path-adjacent member when the reason is explicit.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Constructor)]
public sealed class HotPathAllocationAllowedAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HotPathAllocationAllowedAttribute"/> class.
    /// </summary>
    /// <param name="reason">The reason this member is allowed to allocate.</param>
    public HotPathAllocationAllowedAttribute(string reason)
    {
        Reason = reason;
    }

    /// <summary>
    /// Gets the reason this member is allowed to allocate.
    /// </summary>
    public string Reason { get; }
}
