using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace TransportRouteApi.Converters;

public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        // TimeOnly.Parse is smart enough to understand both "05:00" and "05:00:00"
        return TimeOnly.Parse(value!); 
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        // This ensures your C# API also sends clean "HH:mm" strings back to Next.js
        writer.WriteStringValue(value.ToString("HH:mm")); 
    }
}