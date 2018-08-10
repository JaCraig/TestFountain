using System.Reflection;
using TestFountain.Generator.Interfaces;

namespace TestFountain.Generator
{
    /// <summary>
    /// Default generator
    /// </summary>
    /// <seealso cref="IGenerator"/>
    public class DefaultGenerator : IGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultGenerator"/> class.
        /// </summary>
        /// <param name="random">The random.</param>
        public DefaultGenerator(Mirage.Random random)
        {
            RandomObj = random;
        }

        /// <summary>
        /// Gets the random object.
        /// </summary>
        /// <value>The random object.</value>
        private Mirage.Random RandomObj { get; }

        /// <summary>
        /// Determines whether this instance can generate the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can generate the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGenerate(ParameterInfo parameter)
        {
            return !parameter.HasDefaultValue;
        }

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The next object.</returns>
        public object Next(ParameterInfo parameter)
        {
            return RandomObj.Next(parameter.ParameterType);
        }
    }
}