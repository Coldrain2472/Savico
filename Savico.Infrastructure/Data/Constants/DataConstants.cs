namespace Savico.Infrastructure.Data.Constants
{
    public static class DataConstants
    {
        public const string DateTimeDefaultFormat = "dd/MM/yyyy";

        public const string LengthErrorMessage = "{0} must be between {2} and {1} characters long!";

        public const string RangeErrorMessage = "{0} must be a number between {1} and {2}!";

        public static class UserConstants
        {
            public const int UsernameMinLength = 3;
            public const int UsernameMaxLength = 30;

            public const int FirstNameMinLength = 1;
            public const int FirstNameMaxLength = 30;

            public const int LastNameMinLength = 1;
            public const int LastNameMaxLength = 30;

            public const int EmailMinLength = 1;
            public const int EmailMaxLength = 30;

            public const int PasswordMinLength = 1;
            public const int PasswordMaxLength = 30;

            public const int CurrencyMinLength = 2;
            public const int CurrencyMaxLength = 5;
        }

        public static class ExpenseConstants
        {
            public const int CategoryMinLength = 2;
            public const int CategoryMaxLength = 30;

            public const int DescriptionMinLength = 5;
            public const int DescriptionMaxLength = 300;
        }

        public static class IncomeConstants
        {
            public const int SourceMinLength = 5;
            public const int SourceMaxLength = 100;
        }

        public static class ExpenseCategoryConstants
        {
            public const int CategoryNameMinLength = 2;
            public const int CategoryNameMaxLength = 30;
        }

        public static class GoalConstants
        {
            public const int DescriptionMinLength = 3;
            public const int DescriptionMaxLength = 500;
        }

        public static class TipConstants
        {
            public const int ContentMinLength = 2;
            public const int ContentMaxLength = 300;
        }
    }
}
