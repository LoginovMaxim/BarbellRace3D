namespace Utils
{
    public static class TypeUtils
    {
        public static string GetConcreteTypeName(string fullTypeName)
        {
            var namespaces = fullTypeName.Split('.');
            return namespaces[namespaces.Length - 1];
        }
    }
}