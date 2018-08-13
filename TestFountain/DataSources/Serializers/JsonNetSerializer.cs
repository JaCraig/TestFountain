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
using Newtonsoft.Json;
using SerialBox.BaseClasses;
using System;

namespace TestFountain.DataSources.Serializers
{
    /// <summary>
    /// JSON.Net Serializer
    /// </summary>
    /// <seealso cref="SerializerBase{String}"/>
    public class JsonNetSerializer : SerializerBase<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonNetSerializer"/> class.
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        public JsonNetSerializer(IBootstrapper bootstrapper)
            : base(bootstrapper)
        {
        }

        /// <summary>
        /// Content type (MIME type)
        /// </summary>
        public override string ContentType => "application/json";

        /// <summary>
        /// Common file type (extension)
        /// </summary>
        public override string FileType => ".json";

        /// <summary>
        /// Name of the serializer
        /// </summary>
        public override string Name => "JSON";

        /// <summary>
        /// Deserializes the data
        /// </summary>
        /// <param name="objectType">Object type</param>
        /// <param name="data">Data to deserialize</param>
        /// <returns>The deserialized data</returns>
        public override object Deserialize(Type objectType, string data)
        {
            return JsonConvert.DeserializeObject(data, objectType);
        }

        /// <summary>
        /// Serializes the object
        /// </summary>
        /// <param name="objectType">Object type</param>
        /// <param name="data">Data to serialize</param>
        /// <returns>The serialized data</returns>
        public override string Serialize(Type objectType, object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}