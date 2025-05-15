using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.UI.Classes
{
    public class VoTTConversionOptions : Options
    {
        [JsonInclude, JsonPropertyName("Offset")]
        public double Offset { get; set; } = 1;

        public VoTTConversionOptions(double offset)
            : base()
        {
            Offset = offset;
        }

        public VoTTConversionOptions()
            : base()
        {

        }

        public VoTTConversionOptions(VoTTConversionOptions voTTConversionOptions)
            : base(voTTConversionOptions)
        {
            if (voTTConversionOptions != null)
            {
                Offset = voTTConversionOptions.Offset;
            }
        }

        public VoTTConversionOptions(JsonObject jsonObject)
            : base(jsonObject)
        {

        }
    }
}

