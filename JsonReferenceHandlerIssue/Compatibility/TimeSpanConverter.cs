using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonReferenceHandlerIssue
{
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        #region Methods

        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeSpan.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }

        #endregion
    }
}
