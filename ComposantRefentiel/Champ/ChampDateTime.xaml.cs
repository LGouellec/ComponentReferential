using ComposantReferentiel.Converter;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ComposantReferentiel.Champ
{
    /// <summary>
    /// Logique d'interaction pour ChampDateTime.xaml
    /// </summary>
    public partial class ChampDateTime : ChampGenerique
    {
        #region Attribut(s)


        #endregion

        #region Propriété(s)

        #region DependencyProperty

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
                    if (this.datetime.Text.Equals(string.Empty))
                    {
                        result = false;
                        return result;
                    }
                }
                try
                {
                    Convert.ToDateTime(this.datetime.Text);
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
        /// Constructeur
        /// </summary>
        public ChampDateTime()
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
            binding.Path = new PropertyPath(base.NomBDD);
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            binding.Mode = BindingMode.TwoWay;
            binding.ValidatesOnDataErrors = false;
            binding.NotifyOnValidationError = false;
            binding.Converter = new ConverterDate();
            this.datetime.SetBinding(DatePicker.TextProperty, binding);
        }

        #endregion
    }
}
