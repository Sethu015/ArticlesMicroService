using Blocks.Core.StringExtensions;
using FluentValidation;

namespace Blocks.Core.FluentValidation
{
    public static class Extensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithMessageForInvalidId<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, string propertyName)
        {
            return ruleBuilder.WithMessage(c => ValidationMessages.InvalidId.FormatWith(propertyName));
        }

        public static IRuleBuilderOptions<T,TProperty> NotEmptyWithMessage<T,TProperty>(this IRuleBuilderInitial<T, TProperty> ruleBuilder, string propertyName)
        {
            return ruleBuilder.NotEmpty().WithMessage(c => ValidationMessages.NullOrEmptyValue.FormatWith(propertyName));
        }

        public static IRuleBuilderOptions<T, string?> MaximumLengthWithMessage<T>(this IRuleBuilderOptions<T, string?> ruleBuilder, int maxLength, string propertyName)
        {
            return ruleBuilder.MaximumLength(maxLength).WithMessage(c => ValidationMessages.MaxLengthExceeded.FormatWith(propertyName, maxLength));
        }

        public static IRuleBuilderOptions<T, TProperty> NotNullWithMessage<T, TProperty>(this IRuleBuilderInitial<T, TProperty> ruleBuilder, string propertyName)
        {
            return ruleBuilder.NotNull().WithMessage(c => ValidationMessages.NullOrEmptyValue.FormatWith(propertyName));
        }
    }
}
