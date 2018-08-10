using Canister.Interfaces;
using FileCurator.Registration;
using Mirage.Registration;
using SerialBox.Registration;
using Valkyrie.Registration;

namespace TestFountain.Registration
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
        public static IBootstrapper RegisterTestFountain(this IBootstrapper bootstrapper)
        {
            return bootstrapper.AddAssembly(typeof(CanisterMethods).Assembly)
                                .RegisterFileCurator()
                                .RegisterValkyrie()
                                .RegisterMirage()
                                .RegisterSerialBox();
        }
    }
}