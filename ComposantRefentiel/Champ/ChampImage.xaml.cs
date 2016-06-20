using ComposantReferentiel.Converter;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ComposantReferentiel.Champ
{
    /// <summary>
    /// Logique d'interaction pour ChampImage.xaml
    /// </summary>
    public partial class ChampImage : ChampGenerique
    {
        #region Attribut(s)


        #endregion

        #region Propriété(s)

        #region DependencyProperty

        public static readonly DependencyProperty ListExtensionProperty = DependencyProperty.Register("ListExtension", typeof(string[]), typeof(ChampImage), new PropertyMetadata(null));
        public string[] ListExtension
        {
            get
            {
                return (string[])base.GetValue(ChampImage.ListExtensionProperty);
            }
            set
            {
                base.SetValue(ChampImage.ListExtensionProperty, value);
            }
        }

        #endregion

        #endregion

        #region Constructeur(s)

        public ChampImage()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Méthode(s)

        public override void PlugBinding()
        {
            Binding binding = new Binding();
            binding.Path = new PropertyPath(base.NomBDD, new object[0]);
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            binding.Mode = BindingMode.TwoWay;
            binding.ValidatesOnDataErrors = false;
            binding.NotifyOnValidationError = false;
            binding.Converter = new ConverterImage();
            this.img.SetBinding(Image.SourceProperty, binding);
        }

        #endregion
    }
}
