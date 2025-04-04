using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FinanceApp.Application.Common.Converters;

public class DateTimeConverter : JsonConverter<DateTime>
{ 
    private const string DateFormat = "yyyy-MM-dd HH:mm";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if(reader.TokenType == JsonTokenType.String)
        {
            if(DateTime.TryParseExact(reader.GetString(), DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;
        }

        throw new JsonException("Invalid date format. Expected format: " + DateFormat);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
    }
}
