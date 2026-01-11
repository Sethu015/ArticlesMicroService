namespace Blocks.Core
{
    public static class Guard
    {
        public static void ThrowIfNullOrWhiteSpace(string value) => ArgumentException.ThrowIfNullOrWhiteSpace(value);
        public static void ThrowIfNotEqual<T>(T actual, T expected) where T : IEquatable<T> => ArgumentOutOfRangeException.ThrowIfNotEqual(actual, expected);
    }
}
