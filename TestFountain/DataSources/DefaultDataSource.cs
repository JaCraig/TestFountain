using FileCurator;
using SerialBox.Enums;
using System;
using System.Collections.Generic;
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
            var Results = new List<object[]>();
            var DataDirectoryInfo = new DirectoryInfo(RemoveIllegalDirectoryNameCharacters(DataDirectory + method.DeclaringType.Name + "/" + method.Name));
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
            var DataDirectoryInfo = new DirectoryInfo(RemoveIllegalDirectoryNameCharacters(DataDirectory + method.DeclaringType.Name + "/" + method.Name + "/" + Guid.NewGuid()));

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
    }
}