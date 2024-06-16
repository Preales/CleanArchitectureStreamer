using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Common.Settings;

public static class MdouleExtension
{
    public static T LoadConfiguration<T>(this IConfiguration config) where T : IModuleSettings, new()
    {
        var result = new T();
        config.GetSection(result.SettingName).Bind(result);
        return result;
    }

    public static bool Configured<T>(this IConfiguration config) where T : IModuleSettings, new()
    => config.GetSection(new T().SettingName).Exists();
}