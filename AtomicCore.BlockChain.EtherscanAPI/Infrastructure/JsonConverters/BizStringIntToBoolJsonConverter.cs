using Newtonsoft.Json;
using System;

namespace AtomicCore.BlockChain.EtherscanAPI
{
    /// <summary>
    /// String的数字类型 与 BOOL类型互转
    /// </summary>
    public sealed class BizStringIntToBoolJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;
            return reader.Value.ToString() == "1";
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((bool)value ? "1" : "0");
        }
    }
}
