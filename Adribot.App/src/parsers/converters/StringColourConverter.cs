using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Adribot.extensions;
using Discord;

namespace Adribot.parsers.converters;

internal class StringColourConverter : JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.GetString().ToDiscordColour();

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options) =>
        throw new NotImplementedException();
}
