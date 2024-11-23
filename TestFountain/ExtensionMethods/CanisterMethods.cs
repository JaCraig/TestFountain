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

using Canister.Interfaces;
using SerialBox.Registration;
using TestFountain.DataSources.Interfaces;
using TestFountain.Generator;
using TestFountain.Generator.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Canister methods
    /// </summary>
    public static class CanisterMethods
    {
        /// <summary>
        /// Registers the system with canister.
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        /// <returns>The bootstrapper.</returns>
        public static ICanisterConfiguration? RegisterTestFountain(this ICanisterConfiguration? bootstrapper)
        {
            return bootstrapper?.AddAssembly(typeof(CanisterMethods).Assembly)
                                .RegisterFileCurator()
                                .RegisterMirage()
                                .RegisterSerialBox();
        }

        /// <summary>
        /// Registers the TestFountain services with the specified IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <returns>The IServiceCollection with the registered services.</returns>
        public static IServiceCollection? RegisterTestFountain(this IServiceCollection? services)
        {
            return services
                ?.AddAllTransient<IGenerator>()
                ?.AddAllTransient<IDatasource>()
                ?.AddTransient<GeneratorManager>();
        }
    }
}