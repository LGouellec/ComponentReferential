using ComposantReferentielV2.Converter;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace ComposantReferentiel.Champ
{
    /// <summary>
    /// Logique d'interaction pour ChampBoolean.xaml
    /// </summary>
    public partial class ChampBoolean : ChampGenerique
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
                return true;
            }
        }

        #endregion

        #region Constructeur(s)

        /// <summary>
        /// Constructeur
        /// </summary>
        public ChampBoolean()
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
            Binding binding = new Binding(base.NomBDD);
            binding.Converter = new ConverterBoolean();
            this.checkbox.SetBinding(ToggleButton.IsCheckedProperty, binding);
        }

        #endregion
    }
}
