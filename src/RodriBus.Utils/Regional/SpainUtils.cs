using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RodriBus.Utils.Regional
{
    //http://www.interior.gob.es/web/servicios-al-ciudadano/dni/calculo-del-digito-de-control-del-nif-nie

    /// <summary>
    /// Reflecs the different types of identification numbers in Spain. 
    /// </summary>
    public enum SpainIdNumber
    {
        /// <summary>
        /// This is the generic term for the tax ID number for all individuals.
        /// For Spaniards, it's the DNI plus one letter; for foreigners the NIE must be used.
        /// </summary>
        NIF,

        /// <summary>
        /// This is the identification number for foreigners in Spain gave by the immigration service.
        /// </summary>
        NIE,
    }

    /// <summary>
    /// Contains utils related to especific topics in Spain such as Id number and phone validations
    /// </summary>
    public static class SpainUtils
    {
        /// <summary>
        /// Validates if a document number is valid according to spanish rules.
        /// </summary>
        /// <param name="type">The type of the document</param>
        /// <param name="documentId">The Id number</param>
        /// <returns></returns>
        public static bool IsValidIdNumber(SpainIdNumber type, string documentId)
        {
            switch (type)
            {
                case SpainIdNumber.NIF:
                    return _validateNif(documentId);
                case SpainIdNumber.NIE:
                    return _validateNie(documentId);
            }
            return false;
        }

        private static bool _validateNif(string doc)
        {
            var chars = "trwagmyfpdxbnjzsqvhlcke";

            doc = doc.ToLower();

            var groups = Regex.Match(doc, @"^(\d+)([a-zA-Z])$").Groups;
            var numStr = groups[1].Value;
            var controlChar = groups[2].Value;
            if (int.TryParse(numStr, out int num))
            {
                var i = num % 23;
                return controlChar.Equals(chars[i].ToString());
            }
            return false;
        }

        private static bool _validateNie(string doc)
        {
            var groups = Regex.Match(doc, @"^([a-zA-Z])(\d+)([a-zA-Z])$").Groups;
            var controlDigitStr = groups[1].Value.ToLower().Replace("x", "0").Replace("y", "1").Replace("z", "2");
            return groups.Count == 4 && _validateNif(controlDigitStr + groups[2] + groups[3]);
        }

        /// <summary>
        /// Validates if a phone number is valid according to spanish rules.
        /// </summary>
        /// <param name="phone">The phone number</param>
        /// <returns></returns>
        public static bool IsValidPhone(string phone)
        {
            var phoneRegex = new Regex(@"^(\+34|0034|34)?[\s|\-|\.]?[6|7|9][\s|\-|\.]?([0-9][\s|\-|\.]?){8}$");
            return phoneRegex.IsMatch(phone);
        }
    }
}
