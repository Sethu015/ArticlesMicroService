namespace Blocks.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> source) => !source.Any();
    }
}
