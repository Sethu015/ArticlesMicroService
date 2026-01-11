using Submission.Domain.Entities.ValueObjects;

namespace Submission.Domain.Entities
{
    public partial class Author
    {
        public static Author Create(string email, string firstName, string lastName, string title, string affiliation)
        {
            Author author = new()
            {
                EmailAddress = EmailAddress.Create(email),
                FirstName = firstName,
                LastName = lastName,
                Title = title,
                Affiliation = affiliation
            };
            return author;
        }
    }
}
