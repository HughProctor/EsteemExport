using System.Collections.Generic;

namespace ServiceModel.Test.Models.BAM
{
    public class TemplateClassList
    {
        public List<TemplateClass> TemplateClasses { get; set; }
    }

    public class TemplateClass
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
