/*
Copyright 2018 James Craig

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using Mirage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestFountain.Generator.Interfaces;

namespace TestFountain.Generator
{
    /// <summary>
    /// Generator manager
    /// </summary>
    /// <remarks>Initializes a new instance of the <see cref="GeneratorManager"/> class.</remarks>
    /// <param name="generators">The generators.</param>
    /// <param name="random">The random.</param>
    public class GeneratorManager(IEnumerable<IGenerator> generators, Mirage.Random random)
    {
        /// <summary>
        /// Gets the generators.
        /// </summary>
        /// <value>The generators.</value>
        public IEnumerable<IGenerator> Generators { get; } = generators;

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        public Mirage.Random Random { get; } = random;

        /// <summary>
        /// Gets the next set of parameter values.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The values.</returns>
        public object?[] Next(ParameterInfo[] parameters)
        {
            parameters ??= [];
            var Data = new object?[parameters.Length];
            IGenerator[] LocalGenerators = Generators.ToArray();
            for (int I = 0, MaxLength = parameters.Length; I < MaxLength; I++)
            {
                ParameterInfo Parameter = parameters[I];
                LocalGenerators = Random.Shuffle(LocalGenerators)?.ToArray() ?? [];
                Data[I] = System.Array
                    .Find(LocalGenerators, y => y.CanGenerate(Parameter))?
                    .Next(Parameter);
            }
            return Data;
        }
    }
}