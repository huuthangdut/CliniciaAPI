namespace Clinicia.Common
{
    public static class EmptyArray<T>
    {
        public static T[] Instance { get; } = new T[0];
    }
}