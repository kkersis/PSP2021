using System;
using System.Linq;

namespace PSP
{
    public class PasswordChecker
    {
        private static readonly int _minLength = 8;
        private static readonly char[] _specialSymbols = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '+', '<', '>', '?' };

        public static bool Check(string password)
        {
            return ValidLength(password) && ContainsUppercase(password) && ContainsSpecialChar(password);
        }

        private static bool ValidLength(string password)
        {
            return password.Length >= _minLength; 
        }

        private static bool ContainsUppercase(string password)
        {
            var upperLetters = password.ToCharArray()
                                .Where(c => Char.IsUpper(c));

            return upperLetters.Any();
        }

        private static bool ContainsSpecialChar(string password)
        {
            var specialChars = password.ToCharArray()
                                .Where(c => _specialSymbols.Contains(c));

            return specialChars.Any();
        }
    }
}
