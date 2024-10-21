namespace Solstice.Applications.Attributes;

/// <summary>
/// The ServiceAttribute is a custom attribute used for marking classes within the Solstice.Service namespace.
/// This attribute is sealed, implying that it cannot be inherited from.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class ServiceAttribute : Attribute;