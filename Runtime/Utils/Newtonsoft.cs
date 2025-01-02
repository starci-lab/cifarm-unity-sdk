using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CiFarm.Utils
{
    public class EnumAsStringConverter<T> : JsonConverter<T>
        where T : class, new()
    {
        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            // Start writing the object
            writer.WriteStartObject();

            // Get the properties of the object
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(value);
                var jsonProperty = property.GetCustomAttribute<JsonPropertyAttribute>();

                // Get the property name from JsonProperty attribute if it exists, otherwise use the property name
                var propertyName = jsonProperty?.PropertyName ?? property.Name;
                writer.WritePropertyName(propertyName);
                // Check if the property is of enum type
                if (property.PropertyType.IsEnum || property.PropertyType.IsNullableEnum())
                {
                    // If it's an enum, serialize its string representation (e.g., "Approved" instead of 1)
                    writer.WriteValue(propertyValue.GetStringValue());
                }
                else
                {
                    // Otherwise, serialize the property normally
                    serializer.Serialize(writer, propertyValue);
                }
            }

            // End the object
            writer.WriteEndObject();
        }

        public override T ReadJson(
            JsonReader reader,
            Type objectType,
            T existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        )
        {
            // Tạo đối tượng mục tiêu
            var target = Activator.CreateInstance<T>();

            // Tải JSON vào JObject để dễ dàng truy cập các thuộc tính theo tên
            var jObject = JObject.Load(reader);

            // Lấy các thuộc tính của đối tượng
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var jsonProperty = property.GetCustomAttribute<JsonPropertyAttribute>();
                var propertyName = jsonProperty?.PropertyName ?? property.Name;

                // If the property exists in the JSON, set the value
                if (jObject[propertyName] != null)
                {
                    var value = jObject[propertyName];

                    if (property.PropertyType.IsEnum)
                    {
                        var enumValue = EnumExtensions.GetEnumValue(
                            property.PropertyType,
                            value.ToString()
                        );
                        property.SetValue(target, enumValue);
                    }
                    else
                    {
                        // if not enum, set the value normally
                        property.SetValue(target, value.ToObject(property.PropertyType));
                    }
                }
            }

            return target;
        }
    }
}
