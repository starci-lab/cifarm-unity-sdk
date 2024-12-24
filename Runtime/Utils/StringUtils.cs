using System;
using System.Collections.Generic;
using System.Text;

namespace CiFarm.Utils
{
    public static class StringUtils
    {
        public static string SerializeQueryParams(Dictionary<string, object> queryParams)
        {
            if (queryParams == null || queryParams.Count == 0)
                return string.Empty;

            var queryBuilder = new StringBuilder();

            foreach (var pair in queryParams)
            {
                if (pair.Value != null)
                {
                    if (queryBuilder.Length > 0)
                        queryBuilder.Append("&");

                    queryBuilder.Append($"{Uri.EscapeDataString(pair.Key)}={Uri.EscapeDataString(pair.Value.ToString())}");
                }
            }

            return queryBuilder.ToString();
        }

        public static Dictionary<string, object> SerializeToDictionary<TObject>(TObject obj)
        {
            if (obj == null)
                return null;

            var dictionary = new Dictionary<string, object>();

            foreach (var property in obj.GetType().GetProperties())
            {
                dictionary[property.Name] = property.GetValue(obj);
            }

            return dictionary;
        }
    }
}
