using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Application.Common;
//Add to startup this code

//services.AddSession();
//services.AddDistributedMemoryCache();
public static class SessionExtensions
{
    public static void SetJson(this ISession session, string key, object value)
    {
        if (value.ToString() == "")
        {
            session.Remove(key);
            return;
        }

        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static void SetStringText(this ISession session, string key, object value)
    {
        session.SetString(key, value.ToString());
    }

    public static string GetConnectionString(this ISession session, string key)
    {
        var data = session.GetString(key);
        return data;
    }

    public static T GetJson<T>(this ISession session, string key)
    {
        var data = session.GetString(key);

        if (key == "Branch")
            return data is T data1 ? data1 : default;
        return data == null ? default : JsonConvert.DeserializeObject<T>(data);
    }

    public static T GetComplexData<T>(this ISession session, string key)
    {
        var data = session.GetString(key);
        if (data == null)
        {
            return default(T);
        }
        return JsonConvert.DeserializeObject<T>(data);
    }

    public static void SetComplexData(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }
}