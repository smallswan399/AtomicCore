using Newtonsoft.Json;
using System;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Json Value to TronscanJsonStatus
    /// </summary>
    public sealed class BizTronJsonStatusConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TronscanJsonStatus);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;

            if (reader.Value.ToString().Equals("0", StringComparison.OrdinalIgnoreCase))
                return TronscanJsonStatus.Success;
            else
                return TronscanJsonStatus.Failure;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((int)value).ToString());
        }
    }
}
