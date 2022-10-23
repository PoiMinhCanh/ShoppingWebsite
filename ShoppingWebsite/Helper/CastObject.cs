using Newtonsoft.Json;

namespace ShoppingWebsite.Helper;

public class CastObject
{
    public static string Serialize(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T Deserialize<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}
