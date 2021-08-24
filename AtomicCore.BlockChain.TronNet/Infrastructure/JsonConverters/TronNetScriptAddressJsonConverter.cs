using Newtonsoft.Json;
using System;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Trc20 Contrract Script Address Json Converter
    /// </summary>
    public sealed class TronNetScriptAddressJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;

            return TronNetECKey.ConvertToTronAddressFromHexAddress(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(TronNetECKey.ConvertToHexAddress(value.ToString()));
        }
    }
}
