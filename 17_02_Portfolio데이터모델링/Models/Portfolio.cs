using System.Text.Json;
using System.Text.Json.Serialization;

namespace _17_02_Portfolio데이터모델링.Models
{
    public class Portfolio
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        [JsonPropertyName("img")]
        public string Image { get; set; }

        public int[] Ratings { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<Portfolio>(this);
        }
    }
}
