using LitJson;

public static class Gods
{
    public static byte[] GetBytes(object dataModel)
    {
        return (byte[])dataModel;
    }

    public static T DeserializeJSON<T>(string jsonString) where T : class
    {
        object data = JsonMapper.ToObject<T>(jsonString);
        return (T)data;
    }

    public static string SerializeJSON<T>(T modelData) where T : class
    {
        return JsonMapper.ToJson(modelData);
    }
}

