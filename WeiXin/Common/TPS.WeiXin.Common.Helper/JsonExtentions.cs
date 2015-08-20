using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Zeus.Common.Helper;

namespace TPS.WeiXin.Common.Helper
{
    public class UnixDateTimeConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long? nullable = null;
            if (value is DateTime)
            {
                nullable = ((DateTime)value).ToTimestamp(DateTimeKind.Utc);
            }
            writer.WriteValue(nullable.HasValue ? nullable.ToString() : string.Empty);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var bytes = reader.ReadAsBytes();
            var value = Convert.ToInt64(bytes);
            return DateTimeHelper.FromTimestamp(value);
        }
    }
}
