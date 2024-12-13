using System.Text.RegularExpressions;
using Blog.SharedKernel;

namespace Blog.Domain.ArticleAggregate;

public sealed class Article : HasDomainEvent, IEntity, IAggregateRoot
{
    public Guid Id { get; set; }
    public string Title { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public ICollection<string> Tags { get; private set; } = [];
    public bool IsDraft { get; private set; }
   
    public Guid AuthorId { get; private set; }
    
    public DateTime PublishedAt { get; set; }
    public DateTime CreatedOn { get; set; } 
    public DateTime? ModifiedOn { get; set; }
    
    private Article() { }

    private Article(Guid id,Guid authorId, string title, string content, ICollection<string> tags, bool isDraft)
    {
        Id = id;
        Title = title;
        GenerateSlug(title); 
        Content = content;
        Tags = tags;
        IsDraft = isDraft;
        CreatedOn = DateTime.UtcNow;
    }

    public static Article Create(Guid id, Guid authorId, string title, string content, ICollection<string> tags, bool isDraft) 
        => new(id, authorId, title, content, tags, isDraft);

    private void GenerateSlug(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(title));
        title = title.ToLowerInvariant();
        title = Regex.Replace(title, @"[^a-z0-9\s-]", "");
        title = Regex.Replace(title, @"\s+", " ").Trim();
        title = Regex.Replace(title, @"\s", "-");
        Slug = title;
    }
}