namespace Radiant.NET.Service.Attributes
{
    /// <summary>
    /// The ServiceAttribute is a custom attribute used for marking classes within the Radiant.NET.Service namespace.
    /// This attribute is sealed, implying that it cannot be inherited from.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ServiceAttribute : Attribute
    {
    }
}