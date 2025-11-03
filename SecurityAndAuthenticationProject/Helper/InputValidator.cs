namespace SecurityAndAuthenticationProject.Helper
{
    public class InputValidator
    {
        public static bool IsValidInput(string input, string allowedSpecialCharacters = "")
        {
            if (string.IsNullOrEmpty(input))
                return false;

            var lowerInput = input.ToLower();

            var validCharacters = allowedSpecialCharacters.ToHashSet();
            var safeChar = lowerInput.All(c => char.IsLetterOrDigit(c) || validCharacters.Contains(c));

            return safeChar && IsSafeFromSqlInjection(input) && IsValidXssInput(input);
        }

        public static bool IsSafeFromSqlInjection(string input)
        {
            var lowerInput = input.ToLower();

            // Basic blacklist check for common SQL injection patterns
            return !(lowerInput.Contains("' or ") || lowerInput.Contains("--") || lowerInput.Contains(";--") || lowerInput.Contains("/*") || lowerInput.Contains("xp_"));
        }

        public static bool IsValidXssInput(string input)
        {
            var lowerInput = input.ToLower();

            // Basic blacklist check
            if (lowerInput.Contains("<script") || lowerInput.Contains("<iframe"))
                return false;

            return true;
        }
    }
}
