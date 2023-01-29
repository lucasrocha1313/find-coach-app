using System.ComponentModel;

namespace FindCoachApi.Extensions;

public static class EnumExtension
{
    public static string? GetDescription<T>(this T enumValue) where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
        {
            return null;
        }

        var description = enumValue.ToString();
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        if (fieldInfo == null) return description;
        var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
        description = ((DescriptionAttribute)attrs[0]).Description;

        return description;
    }
}