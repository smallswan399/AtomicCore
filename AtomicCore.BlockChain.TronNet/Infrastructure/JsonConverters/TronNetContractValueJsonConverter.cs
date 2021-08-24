using Newtonsoft.Json;
using System;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Contract Abstract Value Json Converter
    /// </summary>
    public sealed class TronNetContractValueJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TronNetContractBaseValueJson);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}
