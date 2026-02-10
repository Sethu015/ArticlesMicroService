using Blocks.Domain.ValueObjects;

namespace Auth.Domain.Users.ValueObjects
{
    public class ProfessionalProfile : ValueObject
    {
        private ProfessionalProfile() { }

        public string? Position { get; private set; }
        public string? CompanyName { get; private set; }
        public string? Affiliation { get; private set; }

        public static ProfessionalProfile Create(string? position, string? companyName, string? affiliation)
        {
            return new ProfessionalProfile
            {
                Position = string.IsNullOrWhiteSpace(position) ? null : position,
                CompanyName = string.IsNullOrWhiteSpace(companyName) ? null : companyName,
                Affiliation = string.IsNullOrWhiteSpace(affiliation) ? null : affiliation
            };
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Position;
            yield return CompanyName;
            yield return Affiliation;
        }

        public override string ToString() => $"{Position} @ {CompanyName}, Affiliation: {Affiliation}";
    }
}
