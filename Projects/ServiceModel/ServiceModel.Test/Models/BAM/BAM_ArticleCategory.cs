using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Test.Models.BAM
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
