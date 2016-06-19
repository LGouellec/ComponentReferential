using ComposantRefentielV2.Converter;
using ComposantReferentielV2.ValidationRule;
using Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ComposantReferentiel.Champ
{
    /// <summary>
    /// Logique d'interaction pour ChampDecimal.xaml
    /// </summary>
    public partial class ChampDecimal : ChampGenerique
    {
        #region Attribut(s)


        #endregion

        #region Propriété(s)

        #region DependencyProperty

        #region Precision

        public static readonly DependencyProperty PrecisionProperty = DependencyProperty.Register("Precision", typeof(int), typeof(ChampDecimal), new PropertyMetadata(5, new PropertyChangedCallback(ChampDecimal.PrecisionChanged)));
        public int Precision
        {
            get
            {
                return (int)base.GetValue(ChampDecimal.PrecisionProperty);
            }
            set
            {
                base.SetValue(ChampDecimal.PrecisionProperty, value);
            }
        }

        private static void PrecisionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChampDecimal champDecimal = d as ChampDecimal;
            if (champDecimal != null)
            {
                champDecimal.MaxLength = Convert.ToInt32(e.NewValue) + 1;
            }
        }
        #endregion

        #region Scale

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register("Scale", typeof(int), typeof(ChampDecimal), new PropertyMetadata(2));
        public int Scale
        {
            get
            {
                return (int)base.GetValue(ChampDecimal.ScaleProperty);
            }
            set
            {
                base.SetValue(ChampDecimal.ScaleProperty, value);
            }
        }
        #endregion

        #endregion

        public int MaxLength
        {
            get;
            set;
        }

        /// <summary>
        /// Permet de savoir si le champ est valide
        /// </summary>
        public override bool IsValid
        {
            get
            {
                bool result;
                if (base.IsKey || base.NotNull)
                {
                    // Si c'est un champ obligatoire et qu'il est vide : Le champ n'est pas valide
                    if (this.textbox.Text.Equals(string.Empty))
                    {
                        result = false;
                        return result;
                    }
                }

                // On check si il correspond bien à un nombre décimal
                result = (this.textbox.Text.Equals(string.Empty) || RegexService.Instance.IsMatchDecimal(this.Precision, this.Scale, this.textbox.Text));
                return result;
            }
        }

        #endregion

        #region Constructeur(s)

        /// <summary>
        /// Champ sans paramétres
        /// </summary>
        public ChampDecimal()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Méthode(s)

        /// <summary>
        /// Réalise le Binding avec l'élément graphique
        /// </summary>
        public override void PlugBinding()
        {
            Binding binding = new Binding();
            binding.Path = new PropertyPath(base.NomBDD, new object[0]);
            binding.UpdateSourceTrigger = UpdateSourceTrigger.LostFocus;
            binding.ValidatesOnDataErrors = true;
            binding.NotifyOnValidationError = true;
            binding.Converter = new ConverterDecimal();
            binding.ValidationRules.Add(new DecimalValidationRule
            {
                ErrorMessage = string.Format("Valeur décimale invalide [NUMBER({0},{1})]", this.Precision, this.Scale),
                Entier = this.Precision,
                Decimal = this.Scale
            });
            this.textbox.SetBinding(TextBox.TextProperty, binding);
        }

        /// <summary>
        /// L'utilisateur ne peut saisir que des chiffres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Insert || e.Key == Key.Return || e.Key == Key.End || e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right || e.Key >= Key.NumPad0 || e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        #endregion
    }
}
