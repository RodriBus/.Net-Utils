using System;
using System.Collections.Generic;
using System.Text;

namespace RodriBus.Utils.Regional
{
    /// <summary>
    /// Represents a Client's Account Code (CCC - Código Cuenta Cliente) used in Spain.
    /// </summary>
    public class Ccc
    {
        private const string DEFAULT_SEPARATOR = "-";

        /// <summary>
        /// Represents the registered bank code.
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        /// Represents the bank's office code.
        /// </summary>
        public string Office { get; set; }

        /// <summary>
        /// Represents the validation code of the account.
        /// </summary>
        public string ControlDigits { get; set; }

        /// <summary>
        /// Represents the unique identification of the account.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Represents if the CCC is compilant with the Control Digit validation rules.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return _isValid();
            }
        }

        /// <summary>
        /// Creates an instance of <see cref="Ccc"/> from an account string.
        /// </summary>
        /// <remarks>The separator will be infered but must be consistent.</remarks>
        /// <param name="account">The account string</param>
        public Ccc(string account)
        {
            var separator = _identifySeparator(account);
            _initialize(account, separator);
        }

        /// <summary>
        /// Creates an instance of <see cref="Ccc"/> from an account string.
        /// </summary>
        /// <param name="account">The account string</param>
        /// <param name="separator">The separator</param>
        public Ccc(string account, char separator)
        {
            _initialize(account, separator);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join(DEFAULT_SEPARATOR, Entity, Office, ControlDigits, AccountNumber);
        }

        private void _initialize(string account, char separator)
        {
            var sanitized = string.Join(string.Empty, account.Split(separator));

            if (sanitized.Length != 20)
                throw new ArgumentException("Account length must be 20 characters (not including separators).");

            try
            {
                Entity = sanitized.Substring(0, 4);
                Office = sanitized.Substring(4, 4);
                ControlDigits = sanitized.Substring(8, 2);
                AccountNumber = sanitized.Substring(10, 10);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unexpected error identifying CCC parts.", ex);
            }
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
            if (account.Contains("_"))
            {
                separator = '_';
                separators.Add('_');
            }

            if (separators.Count > 1)
                throw new ArgumentException("More than one separator found in account number: " + string.Join(",", separators));

            return separator;
        }

        private bool _isValid()
        {
            var entityOffice = string.Concat(Entity, Office);
            var cd1 = _calculateControlDigit(entityOffice);
            var cd2 = _calculateControlDigit(AccountNumber);
            var calculatedDc = string.Concat(cd1, cd2);
            return (ControlDigits == calculatedDc);
        }

        private static string _calculateControlDigit(string partial)
        {
            var weightTable = new[] { 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
            var sum = 0;
            var length = partial.Length;
            for (var i = 0; i < partial.Length; i++)
            {
                var chr = partial[length - 1 - i];
                if (int.TryParse(chr.ToString(), out var num))
                {
                    var weight = num * weightTable[i];
                    sum = sum + weight;
                }
                else
                {
                    return int.MinValue.ToString();
                }
            }

            var dc = (11 - (sum % 11));
            if (dc == 11) dc = 0;
            else if (dc == 10) dc = 1;
            return dc.ToString();
        }
    }
}
