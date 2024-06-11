using CleanArchitecture.Common.Settings;

namespace CleanArchitecture.Application.Models.Identity;

public sealed class JWtSettings : IModuleSettings
{
    public string SettingName => "JWtSettings";
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double DurationInMinute { get; set; }
}