namespace Solstice.Applications.Attributes;

/// <summary>
/// The ServiceInterfaceAttribute is a custom attribute used for marking interface within the Solstice.Service namespace.
/// This attribute is sealed, implying that it cannot be inherited from.
/// </summary>
[AttributeUsage(AttributeTargets.Interface)]
public sealed class ServiceInterfaceAttribute : Attribute;