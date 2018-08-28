using System;
using System.Collections.Generic;

namespace ServiceModel.Models.BAM
{
    public class Type
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int HierarchyLevel { get; set; }
        public string HierarchyPath { get; set; }
    }

    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int HierarchyLevel { get; set; }
        public string HierarchyPath { get; set; }
    }

    public class Status
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int HierarchyLevel { get; set; }
        public string HierarchyPath { get; set; }
    }

    public class Article
    {
        public string Id { get; set; }
        public string ArticleId { get; set; }
        public string Title { get; set; }
        public int Popularity { get; set; }
        public string Description { get; set; }
        public string ExternalUrl { get; set; }
        public string EndUserContent { get; set; }
        public string AnalystContent { get; set; }
        public bool HasEndUserContent { get; set; }
        public bool HasAnalystContent { get; set; }
        public string HTMLKnowledgeId { get; set; }
        public Type Type { get; set; }
        public Category Category { get; set; }
        public Status Status { get; set; }
        public string StatusString { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public double AverageRating { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }

    public class ArticleList
    {
        public List<Article> Articles { get; set; }
    }
}
