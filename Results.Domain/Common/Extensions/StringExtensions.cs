using Newtonsoft.Json;

namespace Results.Domain.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToJson<T>(this T obj, Formatting format = Formatting.None)
        {
            return JsonConvert.SerializeObject(obj, format);
        }

        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
