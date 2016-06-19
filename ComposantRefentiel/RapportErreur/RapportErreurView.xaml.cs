using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ComposantReferentiel.RapportErreur
{
    /// <summary>
    /// Logique d'interaction pour RapportErreurView.xaml
    /// </summary>
    public partial class RapportErreurView : Window
    {
        public Boolean Refresh
        {
            get;
            set;
        }

        public static readonly DependencyProperty BasPageProperty = DependencyProperty.Register("BasPage", typeof(string), typeof(RapportErreurView), new PropertyMetadata(""));

        public string BasPage
        {
            get
            {
                return (string)base.GetValue(RapportErreurView.BasPageProperty);
            }
            set
            {
                base.SetValue(RapportErreurView.BasPageProperty, value);
            }
        }
        public RapportErreurView(List<Rapport> listRapport)
        {
            this.InitializeComponent();
            RapportErreurVM rapportErreurVM = base.Resources["vm"] as RapportErreurVM;
            if (rapportErreurVM != null)
            {
                rapportErreurVM.Rapport = new ObservableCollection<Rapport>(listRapport);
            }
        }
    }
}
