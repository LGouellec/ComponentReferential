using GeneralServices.PatternBuilder;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ComposantReferentiel.RapportErreur
{
    /// <summary>
    /// Rapport d'erreur concernant une entité.
    /// Contient une liste d'erreur concernant cette entité
    /// </summary>
    public class Rapport : ObservableCollection<Erreur>, IBuilt, INotifyPropertyChanged
    {
        /// <summary>
        /// Titre du rapport
        /// </summary>
        private string titleRapport;
        public string TitleRapport
        {
            get
            {
                return this.titleRapport;
            }
            set
            {
                this.titleRapport = value;
                this.OnPropertyChanged("TitleRapport");
            }
        }

        public Rapport(string title)
        {
            this.TitleRapport = title;
        }

        public new event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
