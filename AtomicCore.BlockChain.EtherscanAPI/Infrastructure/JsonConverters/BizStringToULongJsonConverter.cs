using Newtonsoft.Json;
using System;

namespace AtomicCore.BlockChain.EtherscanAPI
{
    /// <summary>
    /// String 与 ULong Json转换器
    /// </summary>
    public sealed class BizStringToULongJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ulong);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;

            return ulong.Parse(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
