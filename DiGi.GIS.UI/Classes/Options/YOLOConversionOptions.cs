using DiGi.Core.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.UI.Classes
{
    public class YOLOConversionOptions : Options
    {
        [JsonInclude, JsonPropertyName("Offset")]
        public double Offset { get; set; } = 1;

        [JsonInclude, JsonPropertyName("Weights")]
        public Dictionary<YOLO.Enums.Category, double> Weights { get; set; } = Query.Weights();

        [JsonInclude, JsonPropertyName("ClearData")]
        public bool ClearData { get; set; } = false;

        public YOLOConversionOptions(double offset)
            : base()
        {
            Offset = offset;
        }

        public YOLOConversionOptions()
            : base()
        {

        }

        public YOLOConversionOptions(YOLOConversionOptions YOLOConversionOptions)
            : base(YOLOConversionOptions)
        {
            if (YOLOConversionOptions != null)
            {
                Offset = YOLOConversionOptions.Offset;
                ClearData = YOLOConversionOptions.ClearData;
            }
        }

        public YOLOConversionOptions(JsonObject jsonObject)
            : base(jsonObject)
        {

        }

        public double this[YOLO.Enums.Category category]
        {
            get
            {
                if(Weights == null || !Weights.TryGetValue(category, out double result))
                {
                    result = 1;
                }

                return result;
            }

            set
            {
                if(Weights == null)
                {
                    Weights = new Dictionary<YOLO.Enums.Category, double>();
                }

                Weights[category] = value;
            }
        }
    }
}

