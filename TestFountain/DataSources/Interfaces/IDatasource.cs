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