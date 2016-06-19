using System.Windows;
using System.Windows.Controls;

namespace ComposantReferentiel.Champ
{
    /// <summary>
    /// Logique d'interaction pour ChampTexte.xaml
    /// </summary>
    public partial class ChampTexte : ChampGenerique
    {
        #region Attribut(s)


        #endregion

        #region Propriété(s)

        #region DependencyProperty

        public static readonly DependencyProperty TailleChampProperty = DependencyProperty.Register("TailleChamp", typeof(int), typeof(ChampTexte), new PropertyMetadata(10));
        public int TailleChamp
        {
            get
            {
                return (int)base.GetValue(ChampTexte.TailleChampProperty);
            }
            set
            {
                base.SetValue(ChampTexte.TailleChampProperty, value);
            }
        }

        #endregion

        public override bool IsValid
        {
            get
            {
                bool result;
                if (base.IsKey || base.NotNull)
                {
                    if (this.textbox.Text.Equals(string.Empty))
                    {
                        result = false;
                        return result;
                    }
                }
                result = true;
                return result;
            }
        }

        #endregion

        #region Constructeur(s)

        public ChampTexte()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Méthode(s)

        public override void PlugBinding()
        {
            this.textbox.SetBinding(TextBox.TextProperty, base.NomBDD);
        }

        #endregion
    }
}
