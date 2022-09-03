using System;

namespace Utils
{
    public static class Parser
    {
        public static string GetEnumName<T>(T value)
        {
            return Enum.GetName(typeof(T), value);
        }
    }
}