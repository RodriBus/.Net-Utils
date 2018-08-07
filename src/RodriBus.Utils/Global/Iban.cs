using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace RodriBus.Utils.Global
{
    /// <summary>
    /// Represents an International Bank Account Number.
    /// </summary>
    public class Iban
    {
        /// <summary>
        /// Represents the status of the validation for an IBAN.
        /// </summary>
        public enum ValidationResult
        {
            /// <summary>
            /// The IBAN is valid.
            /// </summary>
            IsValid,

            /// <summary>
            /// No value was provided to the validator.
            /// </summary>
            ValueMissing,

            /// <summary>
            /// The IBAN is too small for its country code.
            /// </summary>
            ValueTooSmall,

            /// <summary>
            /// The IBAN is too big for its country code.
            /// </summary>
            ValueTooBig,

            /// <summary>
            /// The IBAN fails the module97 check.
            /// </summary>
            ValueFailsModule97Check,

            /// <summary>
            /// Country code not recognized.
            /// </summary>
            CountryCodeNotKnown
        }

        /// <summary>
        /// The status of the IBAN validation.
        /// </summary>
        public ValidationResult ValidationStatus { get; set; }

        /// <summary>
        /// Creates an instance of <see cref="Iban"/> from an account string.
        /// </summary>
        /// <remarks>The separator will be infered but must be consistent.</remarks>
        /// <param name="account">The account string</param>
        public Iban(string account)
        {
            var separator = _identifySeparator(account);
            _initialize(account, separator);
        }

        /// <summary>
        /// Creates an instance of <see cref="Iban"/> from an account string.
        /// </summary>
        /// <param name="account">The account string</param>
        /// <param name="separator">The separator</param>
        public Iban(string account, char separator)
        {
            _initialize(account, separator);
        }

        private void _initialize(string account, char separator)
        {
            ValidationStatus = _validateIban(account, separator);
        }

        private char _identifySeparator(string account)
        {
            char separator = '\0';
            var separators = new List<char>();
            if (account.Contains("-"))
            {
                separator = '-';
                separators.Add('-');
            }

            if (account.Contains(" "))
            {
                separator = ' ';
                separators.Add(' ');
            }

            if (separators.Count > 1)
                throw new ArgumentException("More than one separator found in account number: " + string.Join(",", separators));

            return separator;
        }

        private ValidationResult _validateIban(string account, char separator)
        {
            if (string.IsNullOrEmpty(account))
                return ValidationResult.ValueMissing;

            var sanitized = string.Join(string.Empty, account.Split(separator));

            if (sanitized.Length < 2)
                return ValidationResult.ValueTooSmall;


            var countryCode = sanitized.Substring(0, 2).ToUpper();

            var countryCodeKnown = _lengths.TryGetValue(countryCode, out int lengthForCountryCode);
            if (!countryCodeKnown)
            {
                return ValidationResult.CountryCodeNotKnown;
            }


            if (sanitized.Length < lengthForCountryCode)
                return ValidationResult.ValueTooSmall;

            if (sanitized.Length > lengthForCountryCode)
                return ValidationResult.ValueTooBig;


            sanitized = sanitized.ToUpper();
            var newIban = sanitized.Substring(4) + sanitized.Substring(0, 4);

            newIban = Regex.Replace(newIban, @"\D", match => (match.Value[0] - 55).ToString());

            var remainder = BigInteger.Parse(newIban) % 97;

            if (remainder != 1)
                return ValidationResult.ValueFailsModule97Check;

            return ValidationResult.IsValid;

        }


        private static readonly IDictionary<string, int> _lengths = new Dictionary<string, int>
        {
            {"AL", 28},
            {"AD", 24},
            {"AT", 20},
            {"AZ", 28},
            {"BE", 16},
            {"BH", 22},
            {"BA", 20},
            {"BR", 29},
            {"BG", 22},
            {"CR", 21},
            {"HR", 21},
            {"CY", 28},
            {"CZ", 24},
            {"DK", 18},
            {"DO", 28},
            {"EE", 20},
            {"FO", 18},
            {"FI", 18},
            {"FR", 27},
            {"GE", 22},
            {"DE", 22},
            {"GI", 23},
            {"GR", 27},
            {"GL", 18},
            {"GT", 28},
            {"HU", 28},
            {"IS", 26},
            {"IE", 22},
            {"IL", 23},
            {"IT", 27},
            {"KZ", 20},
            {"KW", 30},
            {"LV", 21},
            {"LB", 28},
            {"LI", 21},
            {"LT", 20},
            {"LU", 20},
            {"MK", 19},
            {"MT", 31},
            {"MR", 27},
            {"MU", 30},
            {"MC", 27},
            {"MD", 24},
            {"ME", 22},
            {"NL", 18},
            {"NO", 15},
            {"PK", 24},
            {"PS", 29},
            {"PL", 28},
            {"PT", 25},
            {"RO", 24},
            {"SM", 27},
            {"SA", 24},
            {"RS", 22},
            {"SK", 24},
            {"SI", 19},
            {"ES", 24},
            {"SE", 24},
            {"CH", 21},
            {"TN", 24},
            {"TR", 26},
            {"AE", 23},
            {"GB", 22},
            {"VG", 24}
        };
    }

}
