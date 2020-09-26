namespace Repository
{
    static class Util
    {
        static string TableName<T>()
        {
            return typeof(T).ToString();
        }
    }
}