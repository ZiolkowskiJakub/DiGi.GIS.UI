using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.UI.Classes
{
    public class GISWindowSettings : SerializableObject
    {
        [JsonInclude, JsonPropertyName("Paths")]
        public List<string>? Paths { get; set; }

        public GISWindowSettings() 
        {
            
        }

        public GISWindowSettings(GISWindowSettings gISWindowSettings)
            :base(gISWindowSettings)
        {
            if(gISWindowSettings != null)
            {
                Paths = gISWindowSettings.Paths == null ? null : new List<string>(gISWindowSettings.Paths);
            }
        }

        public GISWindowSettings(JsonObject jsonObject)
            : base(jsonObject)
        {

        }
    }
}
