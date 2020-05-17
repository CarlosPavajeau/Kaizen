using Newtonsoft.Json;

namespace Kaizen.Models.Error
{
    public class ErrorDetail
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
