namespace Vibe.Tools
{
    public static class ListExtends
    {
        public static Boolean IsEmpty<T>(this List<T> values) => values.Count == 0;
        public static Boolean IsNotEmpty<T>(this List<T> values) => values.Count > 0;
    }
}
