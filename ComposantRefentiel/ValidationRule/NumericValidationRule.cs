using System;
using System.Globalization;
using System.Windows.Controls;

namespace ComposantReferentiel.ValidationRule
{
    /// <summary>
    /// Régle de validation pour les champs de type NUMERIC
    /// </summary>
    public class NumericValidationRule : System.Windows.Controls.ValidationRule
    {
        /// <summary>
        /// Message d'erreur
        /// </summary>
        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return this._errorMessage;
            }
            set
            {
                this._errorMessage = value;
            }
        }

        /// <summary>
        /// Nombre de chiffre
        /// </summary>
        public int Entier
        {
            get;
            set;
        }

        /// <summary>
        /// Méthode de validation
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            string value2 = (value ?? string.Empty).ToString();
            int res = 0;

            try
            {
                if (value2.Length > Entier)
                    result = new ValidationResult(false, this._errorMessage);

                if(!Int32.TryParse(value2, out res))
                    result = new ValidationResult(false, this._errorMessage);
            }
            catch (Exception)
            {
                result = new ValidationResult(false, this._errorMessage);
            }
            return result;
        }
    }
}
