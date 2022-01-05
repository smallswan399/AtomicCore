﻿using Nethereum.Util;
using Newtonsoft.Json;
using System;
using System.Numerics;

namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// BNB Deciamls Converter
    /// </summary>
    public class BscBNBUnitConverter : JsonConverter
    {
        /// <summary>
        /// CanConvert
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType) => true;

        /// <summary>
        /// ReadJson
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;

            return UnitConversion.Convert.FromWei(BigInteger.Parse(reader.Value.ToString()), UnitConversion.EthUnit.Ether);
        }

        /// <summary>
        /// WriteJson
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        /// <exception cref="TypeAccessException"></exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is decimal balance)
                writer.WriteValue(UnitConversion.Convert.ToWei(balance, UnitConversion.EthUnit.Ether));

            throw new TypeAccessException(nameof(value));
        }
    }
}
