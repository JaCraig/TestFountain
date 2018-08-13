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

using BigBook;
using FileCurator;
using SerialBox.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestFountain.DataSources.Interfaces;

namespace TestFountain.DataSources
{
    /// <summary>
    /// Default data source.
    /// </summary>
    /// <seealso cref="IDatasource"/>
    public class DefaultDataSource : IDatasource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDataSource"/> class.
        /// </summary>
        /// <param name="serialBox">The serial box.</param>
        public DefaultDataSource(SerialBox.SerialBox serialBox)
        {
            SerialBox = serialBox;
        }

        /// <summary>
        /// Gets the data directory.
        /// </summary>
        /// <value>The data directory.</value>
        private static string DataDirectory = "./TestFountain/SavedTests/";

        /// <summary>
        /// Gets the serial box.
        /// </summary>
        /// <value>The serial box.</value>
        public SerialBox.SerialBox SerialBox { get; }

        /// <summary>
        /// Retrieves the data for the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The list of data for the method.</returns>
        public List<object[]> Read(MethodInfo method)
        {
            var Parameters = method.GetParameters();
            if (Parameters.Any(x => x.ParameterType.IsInterface))
                return new List<object[]>();

            var Results = new List<object[]>();
            var DataDirectoryInfo = GetDirectory(DataDirectory, method);
            foreach (var Directory in DataDirectoryInfo.EnumerateDirectories())
            {
                var TempResult = new object[Parameters.Length];
                for (int x = 0; x < Parameters.Length; ++x)
                {
                    var File = new FileInfo(Directory.FullName + "/" + x + ".json");
                    var FileData = File.Read();
                    TempResult[x] = SerialBox.Deserialize(FileData, Parameters[x].ParameterType, SerializationType.JSON);
                }
                Results.Add(TempResult);
            }
            return Results;
        }

        /// <summary>
        /// Saves the specified param data for the method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="paramData">The parameter data.</param>
        public void Save(MethodInfo method, object[] paramData)
        {
            var Parameters = method.GetParameters();
            if (Parameters.Any(x => x.ParameterType.IsInterface))
                return;

            var DataDirectoryInfo = GetDirectory(DataDirectory, method, Guid.NewGuid());

            for (int x = 0; x < Parameters.Length; ++x)
            {
                var File = new FileInfo(DataDirectoryInfo.FullName + "/" + x + ".json");
                File.Write(SerialBox.Serialize<string>(paramData[x], Parameters[x].ParameterType, SerializationType.JSON));
            }
        }

        /// <summary>
        /// Removes illegal characters from a directory
        /// </summary>
        /// <param name="directoryName">Directory name</param>
        /// <param name="replacementChar">Replacement character</param>
        /// <returns>DirectoryName with all illegal characters replaced with ReplacementChar</returns>
        private static string RemoveIllegalDirectoryNameCharacters(string directoryName, char replacementChar = '_')
        {
            if (string.IsNullOrEmpty(directoryName))
                return directoryName;
            var InvalidChars = System.IO.Path.GetInvalidPathChars();
            for (int i = 0, maxLength = InvalidChars.Length; i < maxLength; i++)
            {
                char Char = InvalidChars[i];
                directoryName = directoryName.Replace(Char, replacementChar);
            }

            return directoryName;
        }

        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <param name="dataDirectory">The data directory.</param>
        /// <param name="method">The method.</param>
        /// <returns>The directory specified.</returns>
        private DirectoryInfo GetDirectory(string dataDirectory, MethodInfo method)
        {
            var FullDirectory = RemoveIllegalDirectoryNameCharacters(dataDirectory +
                method.DeclaringType.Namespace +
                "/" +
                method.DeclaringType.GetName().Replace(method.DeclaringType.Namespace + ".", "") +
                "/" +
                method.Name);
            return new DirectoryInfo(FullDirectory);
        }

        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <param name="dataDirectory">The data directory.</param>
        /// <param name="method">The method.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <returns>The directory specified.</returns>
        private DirectoryInfo GetDirectory(string dataDirectory, MethodInfo method, Guid guid)
        {
            var FullDirectory = GetDirectory(dataDirectory, method).FullName + "/" + guid;
            return new DirectoryInfo(FullDirectory);
        }
    }
}