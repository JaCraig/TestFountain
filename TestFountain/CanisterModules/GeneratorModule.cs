using Canister.Interfaces;
using TestFountain.DataSources.Interfaces;
using TestFountain.Generator;
using TestFountain.Generator.Interfaces;

namespace TestFountain.CanisterModules
{
    /// <summary>
    /// Generator module
    /// </summary>
    /// <seealso cref="IModule"/>
    public class GeneratorModule : IModule
    {
        /// <summary>
        /// Order to run this in
        /// </summary>
        public int Order => 1;

        /// <summary>
        /// Loads the module using the bootstrapper
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        public void Load(IBootstrapper bootstrapper)
        {
            if (bootstrapper == null)
                return;
            bootstrapper.RegisterAll<IGenerator>();
            bootstrapper.RegisterAll<IDatasource>();
            bootstrapper.Register<GeneratorManager>();
        }
    }
}