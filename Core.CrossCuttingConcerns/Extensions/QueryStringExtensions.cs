using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.CrossCuttingConcerns.Extensions;

public static class QueryStringExtensions
{
    public static string ToQueryString<T>(this T obj, string baseUrl = "")
    {
        if (obj == null)
            return string.Empty;

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var query = HttpUtility.ParseQueryString(string.Empty);

        foreach (var prop in properties)
        {
            var value = prop.GetValue(obj, null);
            if (value == null) continue;

            // Özel formatlama kuralları
            if (value is DateTime dt)
            {
                query[prop.Name] = dt.ToString("o"); // ISO 8601 format
            }
            else if (value is bool b)
            {
                query[prop.Name] = b.ToString().ToLower(); // "true"/"false"
            }
            else
            {
                query[prop.Name] = value.ToString();
            }
        }

        var queryString = query.ToString(); // key1=value1&key2=value2

        if (string.IsNullOrWhiteSpace(queryString))
            return baseUrl;

        return string.IsNullOrWhiteSpace(baseUrl)
            ? $"?{queryString}"
            : $"{baseUrl}?{queryString}";
    }
}