using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.UI.Classes
{
    /// <summary>
    /// Represents the configuration settings for a GIS window, providing properties for persisting and retrieving window-specific state such as associated file paths.
    /// </summary>
    public class GISWindowSettings : SerializableObject
    {
        /// <summary>
        /// Gets or sets the collection of paths associated with the GIS window settings.
        /// </summary>
        [JsonInclude, JsonPropertyName("Paths")]
        public List<string>? Paths { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GISWindowSettings"/> class.
        /// </summary>
        public GISWindowSettings()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GISWindowSettings"/> class by copying values from an existing <see cref="GISWindowSettings"/> instance.
        /// </summary>
        /// <param name="gISWindowSettings">The source settings instance to copy from, or <see langword="null"/>.</param>
        public GISWindowSettings(GISWindowSettings? gISWindowSettings)
            : base(gISWindowSettings)
        {
            if (gISWindowSettings != null)
            {
                Paths = gISWindowSettings.Paths == null ? null : [.. gISWindowSettings.Paths];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GISWindowSettings"/> class from the specified JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the settings data to be deserialized.</param>
        public GISWindowSettings(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }
    }
}