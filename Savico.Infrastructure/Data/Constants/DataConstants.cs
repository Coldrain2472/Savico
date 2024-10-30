namespace Savico.Infrastructure.Data.Constants
{
    public static class DataConstants
    {
        public const string DateTimeDefaultFormat = "dd/MM/yyyy";

        public const string LengthErrorMessage = "{0} must be between {2} and {1} characters long!";

        public const string RangeErrorMessage = "{0} must be a number between {1} and {2}!";

        public static class UserConstants
        {
            // User constants
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

        public static class BudgetConstants
        {
            // Budget constants
        }

        public static class ExpenseConstants
        {
            // Expense constants
        }

        public static class GoalConstants
        {
            // Goal constants
        }

        public static class IncomeConstants
        {
            // Income constants
        }
    }
}
