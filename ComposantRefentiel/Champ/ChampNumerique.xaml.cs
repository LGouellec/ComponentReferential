using ComposantReferentielV2.ValidationRule;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ComposantReferentiel.Champ
{
    /// <summary>
    /// Logique d'interaction pour ChampNumerique.xaml
    /// </summary>
    public partial class ChampNumerique : ChampGenerique
    {
        #region Attribut(s)


        #endregion

        #region Propriété(s)

        #region DependencyProperty

        public static readonly DependencyProperty PrecisionProperty = DependencyProperty.Register("Precision", typeof(int), typeof(ChampNumerique), new PropertyMetadata(5));

        public int Precision
        {
            get
            {
                return (int)base.GetValue(ChampNumerique.PrecisionProperty);
            }
            set
            {
                base.SetValue(ChampNumerique.PrecisionProperty, value);
            }
        }

        #endregion

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
                    // Si le champ est obligatoire et qu'il est vide, il n'est pas valide
                    if (this.textbox.Text.Equals(string.Empty))
                    {
                        result = false;
                        return result;
                    }
                }
                // On essaye de le convenir en entier, et en fonction du résultat, le champ est valide ou non.
                try
                {
                    if (!this.textbox.Text.Equals(string.Empty))
                    {
                        Convert.ToInt32(this.textbox.Text);
                    }
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
                return result;
            }
        }

        #endregion

        #region Constructeur(s)

        /// <summary>
        /// Champ sans paramétres
        /// </summary>
        public ChampNumerique()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Méthode(s)

        /// <summary>
        /// Réalise le Binding avec l'élément graphique [avec une régle de validation]
        /// </summary>
        public override void PlugBinding()
        {
            Binding binding = new Binding();
            binding.Path = new PropertyPath(base.NomBDD, new object[0]);
            binding.UpdateSourceTrigger = UpdateSourceTrigger.LostFocus;
            binding.ValidatesOnDataErrors = true;
            binding.NotifyOnValidationError = true;
            binding.ValidationRules.Add(new NumericValidationRule
            {
                Entier = this.Precision,
                ErrorMessage = string.Format("Valeur numérique invalide [NUMBER({0})]", this.Precision)
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
            if (e.Key == Key.Insert || e.Key == Key.Return || e.Key == Key.End || e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
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
