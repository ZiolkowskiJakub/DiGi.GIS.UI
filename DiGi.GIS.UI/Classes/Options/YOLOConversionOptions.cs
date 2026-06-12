using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.UI.Classes
{
    /// <summary>
    /// Provides configuration options for the YOLO conversion process, including offset values, category weights, and data clearing preferences.
    /// </summary>
    public class YOLOConversionOptions : SerializableOptions
    {
        /// <summary>
        /// Gets or sets the offset value applied during the YOLO conversion process.
        /// </summary>
        [JsonInclude, JsonPropertyName("Offset")]
        public double Offset { get; set; } = 1;

        /// <summary>
        /// Gets or sets the weights associated with each category used during the conversion process.
        /// </summary>
        [JsonInclude, JsonPropertyName("Weights")]
        public Dictionary<YOLO.Enums.Category, double> Weights { get; set; } = Query.Weights();

        /// <summary>
        /// Gets or sets a value indicating whether existing data should be cleared before starting the conversion process.
        /// </summary>
        [JsonInclude, JsonPropertyName("ClearData")]
        public bool ClearData { get; set; } = false;

        /// <summary>
        /// Gets or sets the file path to the configuration file used for the YOLO conversion process.
        /// </summary>
        [JsonInclude, JsonPropertyName("ConfigurationFilePath")]
        public string? ConfigurationFilePath { get; set; } = null;

        /// <summary>
        /// Gets or sets the directory path where the GIS model files are located.
        /// </summary>
        [JsonInclude, JsonPropertyName("GISModelFilesDirectory")]
        public string? GISModelFilesDirectory { get; set; } = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOConversionOptions"/> class with the specified offset.
        /// </summary>
        /// <param name="offset">The offset value to be used for the YOLO conversion process.</param>
        public YOLOConversionOptions(double offset)
            : base()
        {
            Offset = offset;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOConversionOptions"/> class.
        /// </summary>
        public YOLOConversionOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOConversionOptions"/> class using the values from an existing <see cref="YOLOConversionOptions"/> instance.
        /// </summary>
        /// <param name="YOLOConversionOptions">The source options instance to copy settings from.</param>
        public YOLOConversionOptions(YOLOConversionOptions YOLOConversionOptions)
            : base(YOLOConversionOptions)
        {
            if (YOLOConversionOptions != null)
            {
                Offset = YOLOConversionOptions.Offset;
                ClearData = YOLOConversionOptions.ClearData;
                ConfigurationFilePath = YOLOConversionOptions.ConfigurationFilePath;
                GISModelFilesDirectory = YOLOConversionOptions.GISModelFilesDirectory;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOConversionOptions"/> class using the specified JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the configuration data to initialize the options.</param>
        public YOLOConversionOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the weight associated with the specified YOLO category.
        /// Returns a default value of 1 if no specific weight is defined for the category.
        /// </summary>
        /// <param name="category">The YOLO category to retrieve the weight for.</param>
        /// <returns>The weight assigned to the specified category, or 1 if not found.</returns>
        public double this[YOLO.Enums.Category category]
        {
            get
            {
                if (Weights == null || !Weights.TryGetValue(category, out double result))
                {
                    result = 1;
                }

                return result;
            }

            set
            {
                Weights ??= [];

                Weights[category] = value;
            }
        }
    }
}