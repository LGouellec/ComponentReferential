using System.Globalization;
using System.Windows.Controls;

namespace ComposantReferentiel.ValidationRule
{
    /// <summary>
    /// Régle de validation pour les champs de type DECIMAL
    /// </summary>
    public class DecimalValidationRule : System.Windows.Controls.ValidationRule
    {
        /// <summary>
        /// Message d'erreur
        /// </summary>
        public string ErrorMessage
        {
            get;
            set;
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
        /// Nombre de chiffre partie décimal
        /// </summary>
        public int Decimal
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
            ValidationResult validationResult = new ValidationResult(true, null);
            string text = (value ?? string.Empty).ToString();
            // TODO : FINISH TO IMPLEMENT
            // IF math KO : return new ValidationResult(false, this.ErrorMessage);
            return validationResult;
        }
    }
}
