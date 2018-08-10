using System.Reflection;

namespace TestFountain.Generator.Interfaces
{
    /// <summary>
    /// Generator interface.
    /// </summary>
    public interface IGenerator
    {
        /// <summary>
        /// Determines whether this instance can generate the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can generate the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        bool CanGenerate(ParameterInfo parameter);

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The next object.</returns>
        object Next(ParameterInfo parameter);
    }
}