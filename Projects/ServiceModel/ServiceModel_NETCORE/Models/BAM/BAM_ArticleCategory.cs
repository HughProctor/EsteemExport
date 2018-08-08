using System.Collections.Generic;

namespace ServiceModel.Models.BAM
{
    public class ArticleCategoryRoot
    {
        public List<ArticleCategory> Categories { get; set; }
    }

    public class ArticleCategory
    {
        public string label { get; set; }
        public string nodeId { get; set; }
        public List<object> children { get; set; }
    }
}
