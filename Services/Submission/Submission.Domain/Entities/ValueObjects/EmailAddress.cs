using Blocks.Core;
using Blocks.Domain.ValueObjects;
using System.Text.RegularExpressions;

namespace Submission.Domain.Entities.ValueObjects;
public class EmailAddress : StringValueObject
{
	private EmailAddress(string value) => Value = value;

    public static EmailAddress Create(string value)
    {
        Guard.ThrowIfNullOrWhiteSpace(value);
        if (!IsValid(value))
            throw new ArgumentException("Invalid email format.");
        return new EmailAddress(value);
    }

    private static bool IsValid(string value)
    {
        const string emailRegex = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$";
        return Regex.IsMatch(value, emailRegex,RegexOptions.IgnoreCase);
    }
}
