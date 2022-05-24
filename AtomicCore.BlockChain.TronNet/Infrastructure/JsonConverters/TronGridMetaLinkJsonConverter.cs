using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronGrid Meta Link Json Converter
    /// </summary>
    public sealed class TronGridMetaLinkJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            var dics = jo.ToObject<Dictionary<string, string>>();

            return new TronGridMetaLinkInfo()
            {
                Next = dics["next"]
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is TronGridMetaLinkInfo metaLinkInfo)
            {
                string link_json = JsonConvert.SerializeObject(new Dictionary<string, string>()
                {
                    { "next", metaLinkInfo.Next }
                });
                writer.WriteValue(link_json);
                return;
            }

            throw new NotImplementedException($"'TronGridMetaLinkJsonConverter' need value type is 'TronGridMetaLinkInfo', but current type is '{value.GetType().FullName}'");
        }
    }
}
