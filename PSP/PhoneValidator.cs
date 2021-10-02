using System;
using System.Collections.Generic;
using System.Linq;

namespace PSP
{
    public class PhoneValidator
    {
        private static List<CountryValidationRule> _countryRules = new List<CountryValidationRule> { new CountryValidationRule { Country = "Lithuania", Length = 11, LocalStartsWith = "8", InternationalCode = "+370" } };
        public static bool Check(string number)
        {
            return HasOnlyNumbers(number);
        }

        public static void AddValidationRule(string country, int length, string localStartsWith, string internationalCode)
        {
            _countryRules.Add(new CountryValidationRule
            {
                Country = country,
                Length = length,
                LocalStartsWith = localStartsWith,
                InternationalCode = internationalCode
            });
        }

        public static string ChangePrefix(string number)
        {
            var country = _countryRules.FirstOrDefault(x => x.LocalStartsWith.First() == number.First());

            if (country == null) return number;

            return country.InternationalCode + number[1..];
        }

        private static bool HasOnlyNumbers(string number)
        {
            var letters = number.ToCharArray()
                            .Where(n => Char.IsLetter(n));

            if (letters.Any())
                return false;

            return true;
        }
    }

    public class CountryValidationRule
    {
        public string Country { get; set; }
        public int Length { get; set; }
        public string LocalStartsWith { get; set; }
        public string InternationalCode { get; set; }
    }
}
