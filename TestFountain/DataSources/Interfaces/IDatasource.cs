using System.Collections.Generic;
using System.Reflection;

namespace TestFountain.DataSources.Interfaces
{
    /// <summary>
    /// Data source interface
    /// </summary>
    public interface IDatasource
    {
        /// <summary>
        /// Retrieves the data for the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The list of data for the method.</returns>
        List<object[]> Read(MethodInfo method);

        /// <summary>
        /// Saves the specified param data for the method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="paramData">The parameter data.</param>
        void Save(MethodInfo method, object[] paramData);
    }
}