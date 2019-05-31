namespace NicheNameSorter.Models
{
    /// <summary>
    /// Allows a complex type to display it's value as text.
    /// More explicit than the ToString() method.
    /// Allows additional flexibility if this is to be different than the ToString() method.
    /// </summary>
    public interface IDisplayText
    {
        /// <summary>
        /// Defines the text representation of a specific type.
        /// </summary>
        string ToText();
    }
}
