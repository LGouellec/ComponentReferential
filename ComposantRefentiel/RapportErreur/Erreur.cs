using System.ComponentModel;

namespace ComposantReferentiel.RapportErreur
{
    /// <summary>
    /// Classe représentant une erreur lors de l'ajout d'une entité dans la base
    /// </summary>
    public class Erreur : INotifyPropertyChanged
    {
        /// <summary>
        /// Titre de l'erreur
        /// </summary>
        private string title;
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
                this.OnPropertyChanged("Title");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
