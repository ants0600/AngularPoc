using Newtonsoft.Json.Linq;

namespace RestfulApi
{
    public static class JsonHelper
    {
        public static string SerializeToMinimalJson(object obj)
        {
            return JToken.FromObject(obj).RemoveEmptyChildrens().ToString();
        }

        public static JToken RemoveEmptyChildrens(this JToken token)
        {
            if (token.Type == JTokenType.Object)
            {
                var copy = new JObject();
                foreach (var prop in token.Children<JProperty>())
                {
                    var child = prop.Value;
                    if (child.HasValues)
                    {
                        child = child.RemoveEmptyChildrens();
                    }
                    if (!child.IsEmptyOrDefault())
                    {
                        copy.Add(prop.Name, child);
                    }
                }
                return copy;
            }
            if (token.Type == JTokenType.Array)
            {
                var copy = new JArray();
                foreach (var item in token.Children())
                {
                    var child = item;
                    if (child.HasValues)
                    {
                        child = child.RemoveEmptyChildrens();
                    }
                    if (!child.IsEmptyOrDefault())
                    {
                        copy.Add(child);
                    }
                }
                return copy;
            }
            return token;
        }

        public static bool IsEmptyOrDefault(this JToken token)
        {
            return token.Type == JTokenType.Array && !token.HasValues ||
                   token.Type == JTokenType.Object && !token.HasValues ||
                   token.Type == JTokenType.String && token.ToString() == string.Empty ||
                   token.Type == JTokenType.Boolean && token.Value<bool>() == false ||
                   token.Type == JTokenType.Integer && token.Value<int>() == 0 ||
                   token.Type == JTokenType.Float && token.Value<double>() == 0.0 ||
                   token.Type == JTokenType.Null;
        }
    }
}