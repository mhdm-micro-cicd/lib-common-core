using YourOrg.Common.Core.Results;

namespace YourOrg.Common.Core.Guards;

public static class Guard
{
    public static T AgainstNull<T>(T? value, string paramName) where T : class =>
        value ?? throw new ArgumentNullException(paramName);

    public static string AgainstNullOrEmpty(string? value, string paramName) =>
        string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Value cannot be null or empty.", paramName)
            : value;

    public static T AgainstDefault<T>(T value, string paramName) where T : struct =>
        value.Equals(default(T))
            ? throw new ArgumentException("Value cannot be the default.", paramName)
            : value;

    public static int AgainstNegative(int value, string paramName) =>
        value < 0 ? throw new ArgumentOutOfRangeException(paramName, "Value cannot be negative.") : value;

    public static decimal AgainstNegativeOrZero(decimal value, string paramName) =>
        value <= 0 ? throw new ArgumentOutOfRangeException(paramName, "Value must be positive.") : value;
}
