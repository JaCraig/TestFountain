using Mirage;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestFountain.Generator.Interfaces;

namespace TestFountain.Generator
{
    /// <summary>
    /// Generator manager
    /// </summary>
    public class GeneratorManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorManager"/> class.
        /// </summary>
        /// <param name="generators">The generators.</param>
        /// <param name="random">The random.</param>
        public GeneratorManager(IEnumerable<IGenerator> generators, Random random)
        {
            Generators = generators;
            Random = random;
        }

        /// <summary>
        /// Gets the generators.
        /// </summary>
        /// <value>The generators.</value>
        public IEnumerable<IGenerator> Generators { get; }

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        public Random Random { get; }

        /// <summary>
        /// Gets the next set of parameter values.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The values.</returns>
        public object[] Next(ParameterInfo[] parameters)
        {
            parameters = parameters ?? new ParameterInfo[0];
            object[] Data = new object[parameters.Length];
            IGenerator[] LocalGenerators = Generators.ToArray();
            for (int i = 0, maxLength = parameters.Length; i < maxLength; i++)
            {
                var Parameter = parameters[i];
                LocalGenerators = Random.Shuffle(LocalGenerators).ToArray();
                Data[i] = System.Array
                    .Find(LocalGenerators, y => y.CanGenerate(Parameter))?
                    .Next(Parameter);
            }
            return Data;
        }
    }
}