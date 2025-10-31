using Toyota.Shared.Entities.Helper;
using Toyota.Shared.Language;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Toyota.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static int ToInt(this Enum enumValue)
        {
            return Convert.ToInt32(enumValue);
        }

        public static byte ToByte(this Enum enumValue)
        {
            return Convert.ToByte(enumValue);
        }

        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes.IsAny())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        public static void AddEnumWithValueConverters(this JsonSerializerOptions options)
        {
            var enumTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsEnum);

            foreach (var enumType in enumTypes)
            {
                var converterType = typeof(EnumWithValueConverter<>).MakeGenericType(enumType);
                var converter = (JsonConverter)Activator.CreateInstance(converterType);
                options.Converters.Add(converter);
            }
        }

        public static List<EnumLocalizedEntity> GetLocalizedEnumList<T>(string culture) where T : Enum
        {
            var enumList = new List<EnumLocalizedEntity>();

            foreach (var enumValue in Enum.GetValues(typeof(T)))
            {
                var name = enumValue.ToString();
                var intValue = (int)enumValue;
                var localizedValue = DataLanguageManager.GetDataTranslation(culture, name) ?? name;

                enumList.Add(new EnumLocalizedEntity
                {
                    Value = intValue,
                    Name = name,
                    LocalizedValue = localizedValue
                });
            }

            return enumList;
        }
    }
    public class EnumDescription : Attribute
    {
        public EnumDescription(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }
    }

    //public class EnumValidationAttribute : ValidationAttribute
    //{
    //    public Type EnumType { get; set; }
    //    public object[] AllowedValues { get; set; }

    //    public EnumValidationAttribute(Type enumType)
    //    {
    //        if (!enumType.IsEnum)
    //        {
    //            throw new ArgumentException("Invalid Enum.");
    //        }
    //        EnumType = enumType;
    //    }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        if (value == null)
    //        {
    //            return new ValidationResult("The value cannot be empty.");
    //        }

    //        if (!EnumType.IsEnum || !Enum.IsDefined(EnumType, value))
    //        {
    //            return new ValidationResult("Invalid Enum.");
    //        }

    //        if (AllowedValues != null && AllowedValues.Length > 0)
    //        {
    //            if (!AllowedValues.Contains(value))
    //            {
    //                return new ValidationResult($"Invalid value: {value}. Only {string.Join(", ", AllowedValues)} values ​​are accepted.");
    //            }
    //        }

    //        return ValidationResult.Success;
    //    }
    //}

    public class EnumValidationAttribute : ValidationAttribute
    {
        public Type EnumType { get; set; }
        public object[] AllowedValues { get; set; }
        public bool AllowNull { get; set; }
        public EnumValidationAttribute(Type enumType, bool allowNull = false)
        {
            if (!enumType.IsEnum && Nullable.GetUnderlyingType(enumType)?.IsEnum != true)
            {
                throw new ArgumentException("Invalid Enum.");
            }
            EnumType = enumType;
            AllowNull = allowNull;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return AllowNull ? ValidationResult.Success : new ValidationResult("The value cannot be empty.");
            }

            Type actualEnumType = Nullable.GetUnderlyingType(EnumType) ?? EnumType;

            if (value is Array valuesArray)
            {
                foreach (var item in valuesArray)
                {
                    //if (item == null)
                    //{
                    //    continue;
                    //}

                    if (!actualEnumType.IsEnum || !Enum.IsDefined(actualEnumType, item))
                    {
                        return new ValidationResult($"Invalid enum value: {item}");
                    }

                    if (AllowedValues != null && AllowedValues.Length > 0 && !AllowedValues.Contains(item))
                    {
                        return new ValidationResult($"Invalid value: {item}. Only {string.Join(", ", AllowedValues)} values are accepted.");
                    }
                }
            }
            else
            {
                if (!actualEnumType.IsEnum || !Enum.IsDefined(actualEnumType, value))
                {
                    return new ValidationResult("Invalid Enum.");
                }

                if (AllowedValues != null && AllowedValues.Length > 0)
                {
                    if (!AllowedValues.Contains(value))
                    {
                        return new ValidationResult($"Invalid value: {value}. Only {string.Join(", ", AllowedValues)} values ​​are accepted.");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }

    public class EnumWithValueConverter<T> : JsonConverter<T> where T : Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected StartObject token");

            int enumId = -1;

            // JSON nesnesini gezerek Id değerini bul
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    reader.Read();

                    if (propertyName == "Id" && reader.TokenType == JsonTokenType.Number)
                    {
                        enumId = reader.GetInt32();
                    }
                }

                if (reader.TokenType == JsonTokenType.EndObject)
                    break;
            }

            if (enumId == -1)
                throw new JsonException("Expected an 'Id' property for the enum.");

            // Enum Id değerini enum türüne dönüştür
            return (T)Enum.ToObject(typeof(T), enumId);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Enum'un ID ve adını ayrı ayrı yaz
            writer.WriteNumber("Id", Convert.ToInt32(value));
            writer.WriteString("Name", value.ToString());

            writer.WriteEndObject();
        }
    }
}
