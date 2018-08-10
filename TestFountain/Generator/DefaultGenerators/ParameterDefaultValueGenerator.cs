using System.Reflection;
using TestFountain.Generator.Interfaces;

namespace TestFountain.Generator
{
    /// <summary>
    /// Parameter default value generator
    /// </summary>
    /// <seealso cref="IGenerator"/>
    public class ParameterDefaultValueGenerator : IGenerator
    {
        /// <summary>
        /// Determines whether this instance can generate the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can generate the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGenerate(ParameterInfo parameter)
        {
            return parameter.HasDefaultValue;
        }

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The next object.</returns>
        public object Next(ParameterInfo parameter)
        {
            return parameter.DefaultValue;
        }
    }
}