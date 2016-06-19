using GeneralServices.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ComposantReferentiel.RapportErreur
{
    /// <summary>
    /// View Model de la fenêtre pop-up des rapports d'erreurs
    /// </summary>
    public class RapportErreurVM : ViewModelBase
    {
        #region Attribut(s)

        /// <summary>
        /// Liste des rapports à afficher
        /// </summary>
        private ObservableCollection<Rapport> rapport;

        #endregion

        #region Propriété(s)

        #region Command

        /// <summary>
        /// Command pour fermer la pop-up et rafraichir le composant référentiel
        /// </summary>
        public ICommand Oui
        {
            get;
            private set;
        }

        /// <summary>
        /// Command pour fermer la pop-up et ne pas rafraichir le composant référentiel
        /// </summary>
        public ICommand Non
        {
            get;
            private set;
        }

        #endregion

        /// <summary>
        /// Liste des rapports à afficher
        /// </summary>
        public ObservableCollection<Rapport> Rapport
        {
            get
            {
                return this.rapport;
            }
            set
            {
                this.rapport = value;
                this.OnPropertyChanged("Rapport");
            }
        }

        #endregion

        #region Constructeur(s)

        /// <summary>
        /// Constructeur sans paramètres
        /// </summary>
        public RapportErreurVM()
        {
            this.Oui = new RelayCommand(new Action<object>(this.ExOui));
            this.Non = new RelayCommand(new Action<object>(this.ExNon));
        }

        #endregion

        #region Méthode(s)

        #region Event

        private void ExOui(object obj)
        {
            (obj as RapportErreurView).Refresh = true;
            (obj as RapportErreurView).Close();
        }

        private void ExNon(object obj)
        {
            (obj as RapportErreurView).Refresh = false;
            (obj as RapportErreurView).Close();
        }

        #endregion

        #endregion
    }
}
