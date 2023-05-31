using Newtonsoft.Json;

namespace TinkoffMapper.Invest.Rest.Response
{
    public class ErrorMessage
    {
        [JsonProperty("code")]
        public int? Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
