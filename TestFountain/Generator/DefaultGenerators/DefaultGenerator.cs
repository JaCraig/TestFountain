﻿/*
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

using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TestFountain.Generator.Interfaces;

namespace TestFountain.Generator.DefaultGenerators
{
    /// <summary>
    /// Default generator
    /// </summary>
    /// <seealso cref="IGenerator"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="DefaultGenerator"/> class.
    /// </remarks>
    /// <param name="random">The random.</param>
    public class DefaultGenerator(Mirage.Random random) : IGenerator
    {

        /// <summary>
        /// Gets the random object.
        /// </summary>
        /// <value>The random object.</value>
        private Mirage.Random RandomObj { get; } = random;

        /// <summary>
        /// Determines whether this instance can generate the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can generate the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGenerate(ParameterInfo parameter) => !parameter.HasDefaultValue && !parameter.ParameterType.IsInterface;

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The next object.</returns>
        public object? Next(ParameterInfo parameter)
        {
            var ReturnValue = RandomObj.Next(parameter.ParameterType);
            ValidationAttribute? Validation = parameter.GetCustomAttribute<ValidationAttribute>();
            if (Validation != null)
            {
                while (!Validation.IsValid(ReturnValue))
                {
                    ReturnValue = RandomObj.Next(parameter.ParameterType);
                }
            }
            return ReturnValue;
        }
    }
}