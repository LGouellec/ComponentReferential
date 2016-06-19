using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ComposantReferentiel.Champ
{
    /// <summary>
    /// Classe générique champ qui est décliné en plusieurs champ
    /// </summary>
    public class ChampGenerique : UserControl, INotifyPropertyChanged
    {
        #region Attribut(s)
        public const string BEFORE_UPDATE = "BEFORE_UPDATE";
        #endregion

        #region Propriété(s)

        #region DependencyProperty

        #region Nom du champ

        public static readonly DependencyProperty NomChampProperty = DependencyProperty.Register("NomChamp", typeof(string), typeof(ChampGenerique), new PropertyMetadata(""));
        public string NomChamp
        {
            get
            {
                return (string)base.GetValue(ChampGenerique.NomChampProperty);
            }
            set
            {
                base.SetValue(ChampGenerique.NomChampProperty, value);
            }
        }

        #endregion

        #region Nom de la colonne en BDD

        public static readonly DependencyProperty NomBDDProperty = DependencyProperty.Register("NomBDD", typeof(string), typeof(ChampGenerique), new PropertyMetadata("", new PropertyChangedCallback(ChampGenerique.NomBDDChanged)));
        public string NomBDD
        {
            get
            {
                return (string)base.GetValue(ChampGenerique.NomBDDProperty);
            }
            set
            {
                base.SetValue(ChampGenerique.NomBDDProperty, value);
            }
        }
        private static void NomBDDChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChampGenerique champGenerique = d as ChampGenerique;
            if (champGenerique != null)
            {
                champGenerique.PlugBinding();
            }
        }

        #endregion

        #region Clé primaire

        public static readonly DependencyProperty IsKeyProperty = DependencyProperty.Register("IsKey", typeof(bool), typeof(ChampGenerique), new PropertyMetadata(false));
        public bool IsKey
        {
            get
            {
                return (bool)base.GetValue(ChampGenerique.IsKeyProperty);
            }
            set
            {
                base.SetValue(ChampGenerique.IsKeyProperty, value);
            }
        }

        #endregion

        #region Champ obligatoire

        public static readonly DependencyProperty NotNullProperty = DependencyProperty.Register("NotNull", typeof(bool), typeof(ChampGenerique), new PropertyMetadata(false));
        public bool NotNull
        {
            get
            {
                return (bool)base.GetValue(ChampGenerique.NotNullProperty);
            }
            set
            {
                base.SetValue(ChampGenerique.NotNullProperty, value);
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// Composant Referentiel
        /// </summary>
        public ReferentielControl Control
        {
            get;
            set;
        }

        /// <summary>
        /// Permet de savoir si le champ est valide
        /// </summary>
        public virtual bool IsValid
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Event
        public delegate void Update(ChampGenerique champ, String nameInfo, params object[] args);
        public event Update Event;

        protected void Change(String nameInfo, params object[] args)
        {
            // Check if there are any Subscribers
            if (Event != null)
            {
                // Call the Event
                Event(this, nameInfo, args);
            }
        }
        #endregion

        #region Constructeur(s)

        /// <summary>
        /// Constructeur sans paramétres
        /// </summary>
        public ChampGenerique()
        {
            base.LostFocus += new RoutedEventHandler(this.ChampGenerique_LostFocus);
        }

        #endregion

        #region Méthode(s)

        /// <summary>
        /// Méthode virtual à redéfinir par tous les champs qui héritent de ChampGenerique
        /// </summary>
        public virtual void PlugBinding() { }

        /// <summary>
        /// Méthode virtual à définir par les nouveau champs implémentés par projet.
        /// </summary>
        /// <returns>Retourne la colonne qui sera affiché sur la partie grille du composant référentiel.</returns>
        public virtual DataGridColumn CreateColumn() { return null; }

        /// <summary>
        /// Est appelé lorsque le mode du champ change
        /// </summary>
        /// <param name="mode"></param>
        public virtual void ModeChanged(MODE_COMPOSANT_REFERENTIIEL mode)
        {
            bool isEnabled = true;

            if (this.IsKey)
            {
                switch (mode)
                {
                    case MODE_COMPOSANT_REFERENTIIEL.CONSULT:
                        isEnabled = false;
                        break;
                    case MODE_COMPOSANT_REFERENTIIEL.ADD:
                        isEnabled = true;
                        break;
                    case MODE_COMPOSANT_REFERENTIIEL.ERASE:
                        isEnabled = false;
                        break;
                }
            }

            switch (mode)
            {
                case MODE_COMPOSANT_REFERENTIIEL.LOADING:
                    isEnabled = false;
                    break;
                case MODE_COMPOSANT_REFERENTIIEL.LOADED:
                    isEnabled = true;
                    break;
            }
            base.IsEnabled = isEnabled;
        }

        private void ChampGenerique_LostFocus(object sender, RoutedEventArgs e)
        {
            Change(BEFORE_UPDATE, e);
        }

        #region Implémentation INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
        
        #endregion
    }
}
