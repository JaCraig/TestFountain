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