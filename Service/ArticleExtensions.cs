using System.Linq;
using Model.Interface;

namespace Service
{
    internal static class ArticleExtensions
    {
        public static bool Contains(this IArticle model, string pattern)
        {
            var modelProperties = model.GetType().GetProperties();
            return modelProperties.Any(prop => prop.GetValue(model).ToString().Contains(pattern));
        }
    }
}