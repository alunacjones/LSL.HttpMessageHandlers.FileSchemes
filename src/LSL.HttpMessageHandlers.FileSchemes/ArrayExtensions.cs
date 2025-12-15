namespace LSL.HttpMessageHandlers.FileSchemes;

internal static class ArrayExtensions
{
    public static bool TryGetLastItem<T>(this T[] source, out T value)
    {
        if (source.Length == 0)
        {
            value = default;
            return false;
        }

        value = source[source.Length - 1];
        return true;
    }
}