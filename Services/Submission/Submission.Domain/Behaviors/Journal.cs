namespace Submission.Domain.Entities
{
    public partial class Journal
    {
        public Article CreateArticle(string title, ArticleType articleType,string scope)
        {
            Article article = new Article
            {
                Title = title,
                ArticleType = articleType,
                Scope = scope,
                ArticleStage = ArticleStage.Created,
                Journal = this
            };
            _articles.Add(article);
            return article;
        }
    }
}
