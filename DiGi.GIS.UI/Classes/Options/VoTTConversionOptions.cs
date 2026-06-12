using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.UI.Classes
{
    /// <summary>
    /// Provides configuration options for the VoTT conversion process.
    /// </summary>
    public class VoTTConversionOptions : SerializableOptions
    {
        /// <summary>
        /// Gets or sets the offset value used during the VoTT conversion process.
        /// </summary>
        [JsonInclude, JsonPropertyName("Offset")]
        public double Offset { get; set; } = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="VoTTConversionOptions"/> class with the specified offset.
        /// </summary>
        /// <param name="offset">The offset value to be used for conversion.</param>
        public VoTTConversionOptions(double offset)
            : base()
        {
            Offset = offset;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VoTTConversionOptions"/> class.
        /// </summary>
        public VoTTConversionOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VoTTConversionOptions"/> class using the values from an existing <see cref="VoTTConversionOptions"/> instance.
        /// </summary>
        /// <param name="voTTConversionOptions">The source options instance to copy settings from.</param>
        public VoTTConversionOptions(VoTTConversionOptions voTTConversionOptions)
            : base(voTTConversionOptions)
        {
            if (voTTConversionOptions != null)
            {
                Offset = voTTConversionOptions.Offset;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VoTTConversionOptions"/> class from the specified <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the configuration settings for VoTT conversion.</param>
        public VoTTConversionOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }
    }
}