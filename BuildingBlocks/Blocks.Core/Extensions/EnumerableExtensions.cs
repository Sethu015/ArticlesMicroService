namespace Blocks.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> source) => !source.Any();
        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source) => source is null || !source.Any();
    }
}
