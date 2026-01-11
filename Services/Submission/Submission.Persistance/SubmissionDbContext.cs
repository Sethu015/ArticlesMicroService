using Microsoft.EntityFrameworkCore;
using Submission.Domain.Entities;

namespace Submission.Persistance
{
    public class SubmissionDbContext : DbContext
    {
        public virtual DbSet<Journal> Journals { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<ArticleActor> ArticleActors { get; set; }
        public virtual DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
