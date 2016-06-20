using ComposantRefentiel.BLL;
using ComposantReferentiel.Champ;
using ComposantReferentiel.RapportErreur;
using MessageBox;
using ReadAndWrite;
using GeneralServices.Model;
using GeneralServices.PatternBuilder;
using GeneralServices.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;
using ComposantRefentiel.BLL;
using ComposantReferentiel.RapportErreur;

namespace ComposantReferentiel.ViewModel
{
    public class ReferentielControlVM : ViewModelBase
    {
        #region Attribut(s)

        private bool noBlock;
        private MODE_COMPOSANT_REFERENTIIEL mode;
        private ObservableCollection<IDTO> listSource;
        private object currentElement;
        private IComposantModification client;
        private IBLLReferentiel business;
        private System.Windows.Visibility isLoading;

        private static String messLoading = @"Une opération est déjà en cours d'éxécution. Veuillez réessayer ultérieurement.";

        #endregion

        #region Propriété(s)

        #region Command

        public ICommand FonctionAdd
        {
            get;
            private set;
        }

        public ICommand FonctionErase
        {
            get;
            private set;
        }

        public ICommand Valid
        {
            get;
            private set;
        }

        public ICommand Cancel
        {
            get;
            private set;
        }

        public ICommand ExportCSV
        {
            get;
            private set;
        }

        public ICommand ImportCSV
        {
            get;
            private set;
        }

        public ICommand Close
        {
            get;
            private set;
        }

        #endregion

        #region Items Source DataGrid

        public bool NoBlock
        {
            get
            {
                return this.noBlock;
            }
            set
            {
                this.noBlock = value;
                this.OnPropertyChanged("NoBlock");
            }
        }

        public MODE_COMPOSANT_REFERENTIIEL Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                this.mode = value;
                this.OnPropertyChanged("Mode");
                if (this.ControlChamps != null)
                {
                    this.ControlChamps.Mode = this.mode;
                }
            }
        }

        public ObservableCollection<IDTO> ListSource
        {
            get { return listSource; }
            set
            {
                listSource = value;
                //OnPropertyChanged("ListSource");
                RaisePropertyChanged<ObservableCollection<IDTO>>(() => ListSource);
            }
        }
        
        #endregion

        #region Controls de Champ

        public ControlChamps ControlChamps
        {
            get;
            set;
        }

        #endregion

        #region CurrentElement
        /// <summary>
        /// Objet courant de la data grid view
        /// </summary>
        public object CurrentElement
        {
            get { return currentElement; }
            set
            {
                currentElement = value;
                OnPropertyChanged("CurrentElement");
            }
        }
        #endregion

        #region Client

        public IComposantModification Client
        {
            get { return client; }
            set
            {
                if (value == null)
                    throw new Exception("Le client d'un composant référentiel ne peut-être null.");

                ThreadPool.QueueUserWorkItem(async =>
                {
                    var dispatcher = Application.Current.Dispatcher;
                    dispatcher.Invoke(() =>
                    {
                        IsLoading = Visibility.Visible;
                    });

                    client = value;

                    if (BLL == null)
                        BLL = Client.GetBLLReferentiel();

                    // Lancer le refresh des données + alimenter la liste listSource.
                    ListSource = new ObservableCollection<IDTO>(BLL.SelectAllBusiness());

                    dispatcher.Invoke(() =>
                    {
                        IsLoading = Visibility.Hidden;
                    });
                });
            }
        }
        #endregion

        #region BLL
        /// <summary>
        /// Business Layer du réferentiel.
        /// </summary>
        //public ReferentielControlBLL BLL { get { return business; } private set { business = value; } }
        public IBLLReferentiel BLL { get { return business; } private set { business = value; } }
        #endregion

        #region Visibility Progress Bar

        public System.Windows.Visibility IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged("IsLoading");

                if (isLoading == Visibility.Visible && ControlChamps != null)
                    ControlChamps.Visibility = Visibility.Hidden;
                else if ((isLoading == Visibility.Hidden || isLoading == Visibility.Collapsed) && ControlChamps != null)
                    ControlChamps.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #endregion

        #region Constructeur(s)

        public ReferentielControlVM()
        {
            this.IsLoading = Visibility.Hidden;
            this.NoBlock = true;
            this.FonctionAdd = new RelayCommand(new Action<object>(this.FonctionAddDelegate), new Func<object, bool>(this.CanFonctionAddDelegate));
            this.FonctionErase = new RelayCommand(new Action<object>(this.FonctionEraseDelegate), new Func<object, bool>(this.CanFonctionEraseDelegate));
            this.Valid = new RelayCommand(new Action<object>(this.ValidDelegate));
            this.Cancel = new RelayCommand(new Action<object>(this.CancelDelegate));
            this.Close = new RelayCommand(new Action<object>(this.CloseDelegate));
            this.ExportCSV = new RelayCommand(new Action<object>(this.ExportCSVDelegate));
            this.ImportCSV = new RelayCommand(new Action<object>(this.ImportCSVDelegate));

            ControlChamps = new ControlChamps();
            Binding bindingCurrentElement = new Binding("CurrentElement");
            bindingCurrentElement.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            bindingCurrentElement.NotifyOnSourceUpdated = true;
            bindingCurrentElement.Mode = BindingMode.TwoWay;
            bindingCurrentElement.Source = this;
            ControlChamps.SetBinding(ControlChamps.CurrentElementProperty, bindingCurrentElement);

            Binding bindingMode = new Binding("Mode");
            bindingMode.Source = this;
            bindingMode.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            bindingMode.NotifyOnSourceUpdated = true;
            ControlChamps.SetBinding(ControlChamps.ModeProperty, bindingMode);
            ControlChamps.Event += ControlChamps_Event;

            Mode = MODE_COMPOSANT_REFERENTIIEL.CONSULT;
        }

        #endregion

        #region Méthode(s)

        #region Event

        /// <summary>
        /// Import CSV de données. Attention le DTO en question doit implémenter l'interface ISerializableCSV
        /// </summary>
        /// <param name="obj"></param>
        private void ImportCSVDelegate(object obj)
        {
            List<ISerializableCSV> list;
            if (IsLoading == Visibility.Visible)
                ShowMessageBox.Show(TYPE_WINDOW_BOX.AVERTISSEMENT, messLoading, "");
            else
            {
                ThreadPool.QueueUserWorkItem(async =>
                {
                    var dispatcher = Application.Current.Dispatcher;
                    dispatcher.Invoke(() =>
                    {
                        IsLoading = Visibility.Visible;
                    });

                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                    openFileDialog.CheckFileExists = true;
                    openFileDialog.CheckPathExists = true;
                    openFileDialog.Multiselect = false;
                    openFileDialog.Title = String.Format("{0}", "Import CSV File");

                    if (openFileDialog.ShowDialog() == true)
                    {
                        ReadAndWriteCSV readAndWriteCSV = new ReadAndWriteCSV(openFileDialog.FileName, ";");
                        ReadAndWriteCSV.encoding = System.Text.Encoding.Default;
                        Type type = BLL.CreateElement().GetType();

                        list = readAndWriteCSV.ReadCSV(type);
                        ListSource = new ObservableCollection<IDTO>(ListSource.Concat<ISerializableCSV>(list).Cast<IDTO>());
                    }

                    dispatcher.Invoke(() =>
                    {
                        IsLoading = Visibility.Hidden;
                    });
                });
            }
        }

        /// <summary>
        /// Export CSV de la grille de données. Attention le DTO en question doit implémenter l'interface ISerializableCSV
        /// </summary>
        /// <param name="obj"></param>
        private void ExportCSVDelegate(object obj)
        {
            if (IsLoading == Visibility.Visible)
                ShowMessageBox.Show(TYPE_WINDOW_BOX.AVERTISSEMENT, messLoading, "");
            else
            {
                ParamExportCSV p = Client.GetParam();
                ReadAndWriteCSV readAndWriteCSV = new ReadAndWriteCSV(p.Path, p.Separateur);
                List<ISerializableCSV> list = new List<ISerializableCSV>(ListSource.Cast<ISerializableCSV>());
                readAndWriteCSV.WriteCSV(p.GetHeaderBody(), list);
            }
        }

        /// <summary>
        /// Annuler les modifications sur la grille de données.
        /// </summary>
        /// <param name="obj"></param>
        private void CancelDelegate(object obj)
        {
            if (IsLoading == Visibility.Visible)
                ShowMessageBox.Show(TYPE_WINDOW_BOX.AVERTISSEMENT, messLoading, "");
            else
            {
                ThreadPool.QueueUserWorkItem(async =>
                {
                    var dispatcher = Application.Current.Dispatcher;
                    dispatcher.Invoke(() =>
                    {
                        IsLoading = Visibility.Visible;
                    });

                    if (BLL != null)
                    {
                        BLL.RollBack();
                        ListSource = new ObservableCollection<IDTO>(BLL.SelectAllBusiness());
                    }

                    dispatcher.Invoke(() =>
                    {
                        IsLoading = Visibility.Hidden;
                    });
                });
            }
        }


        /// <summary>
        /// Valider les modifications sur la grille de données.
        /// Peut générer un rapport d'erreur s'il y a des problèmes d'insertion en base de données.
        /// FK , PK, ou autres contraintes.
        /// </summary>
        /// <param name="obj"></param>
        private void ValidDelegate(object obj)
        {
            if (IsLoading == Visibility.Visible)
                ShowMessageBox.Show(TYPE_WINDOW_BOX.AVERTISSEMENT, messLoading, "");
            else
            {
                ThreadPool.QueueUserWorkItem(async =>
                {
                    // Activation de la progress bar
                    var dispatcher = Application.Current.Dispatcher;
                    dispatcher.Invoke(() =>
                    {
                        IsLoading = Visibility.Visible;
                    });

                    // Validation
                    IBuilder builder = null;
                    RapportErreurView rapport = null;
                    try
                    {
                        BLL.SaveChanges(ListSource.ToList<IDTO>());
                    }
                    catch (DbEntityValidationException)
                    {
                        List<DbEntityValidationResult> list = BLL.GetValidationErrors().ToList<DbEntityValidationResult>();
                        if (list.Count != 0)
                        {
                            // Construction du builder pour le rapport
                            builder = new BuilderRapportValidation();
                            builder.BuildPart(list);
                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        // Construction du builder pour le rapport
                        builder = new BuilderRapportOracleException();
                        builder.BuildPart(ex);
                    }

                    // Affichage du rapport
                    dispatcher.Invoke(() =>
                    {
                        if (builder != null)
                        {
                            rapport = new RapportErreurView(builder.GetResult().ConvertAll<Rapport>((IBuilt input) => input as Rapport))
                            {
                                BasPage = "Voulez-vous annuler les modifications ?"
                            };
                            rapport.ShowDialog();
                        }

                    });

                    // Si l'utilisateur veut rafraichir.
                    if (rapport != null && rapport.Refresh)
                    {
                        BLL.RollBack();
                        // Lancer le refresh des données + alimenter la liste listSource.
                        ListSource = new ObservableCollection<IDTO>(BLL.SelectAllBusiness());
                    }

                    // Désactivation de la progress bar
                    dispatcher.Invoke(() =>
                    {
                        IsLoading = Visibility.Hidden;
                    });
                    
                });

            }
        }

        private bool CanFonctionAddDelegate(object arg)
        {
            return true;
        }

        /// <summary>
        /// Permet d'ajouter un enregistrement à la grille de données.
        /// Dans un premier temps, la grille est vérouillé tant que l'utilisateur ne valide pas la saisie de ce nouvel enregistrement.
        /// </summary>
        /// <param name="obj"></param>
        private void FonctionAddDelegate(object obj)
        {
            if (IsLoading == Visibility.Visible)
                ShowMessageBox.Show(TYPE_WINDOW_BOX.AVERTISSEMENT, messLoading, "");
            else
            {
                bool flag = true;
                if (this.Mode == MODE_COMPOSANT_REFERENTIIEL.CONSULT)
                {
                    IDTO dto = BLL.CreateElement();

                    // L'observer est appelé pour mettre à jour les champs que le développeur souhaite
                    // Ex : Champs de traca (Date heure creation, modif, ...)
                    if (Client != null)
                        Client.AfterCreate(ref dto);

                    ListSource.Add(dto);
                    CurrentElement = dto;
                }
                else
                {
                    if (this.Mode == MODE_COMPOSANT_REFERENTIIEL.ADD)
                    {
                        foreach (ChampGenerique current in this.ControlChamps.CollectionChamps)
                        {
                            if (!current.IsValid)
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            ShowMessageBox.Show(TYPE_WINDOW_BOX.AVERTISSEMENT, "", "La validation a échoué. Certains champs sont invalides par rapport aux contraintes définies dans la configuration.");
                        }
                    }
                }
                this.Mode = (flag ? ((this.Mode == MODE_COMPOSANT_REFERENTIIEL.CONSULT) ? MODE_COMPOSANT_REFERENTIIEL.ADD : MODE_COMPOSANT_REFERENTIIEL.CONSULT) : this.Mode);
            }
        }

        private bool CanFonctionEraseDelegate(object arg)
        {
            return this.mode != MODE_COMPOSANT_REFERENTIIEL.ADD && CurrentElement != null;
        }

        /// <summary>
        /// Permet de supprimer un enregistrement de la grille de données.
        /// </summary>
        /// <param name="obj"></param>
        private void FonctionEraseDelegate(object obj)
        {
            if (IsLoading == Visibility.Visible)
                ShowMessageBox.Show(TYPE_WINDOW_BOX.AVERTISSEMENT, messLoading, "");
            else
            {
                this.Mode = MODE_COMPOSANT_REFERENTIIEL.ERASE;

                BLL.Remove(CurrentElement as IDTO);
                ListSource.Remove(CurrentElement as IDTO);
                
                this.Mode = MODE_COMPOSANT_REFERENTIIEL.CONSULT;
            }
        }

        /// <summary>
        /// Permet de femer le composant référentiel.
        /// </summary>
        /// <param name="obj"></param>
        private void CloseDelegate(object obj)
        {
            if (IsLoading == Visibility.Visible)
                ShowMessageBox.Show(TYPE_WINDOW_BOX.AVERTISSEMENT, messLoading, "");
            else
            {
                if (ShowMessageBox.Show(
                    TYPE_WINDOW_BOX.CONFIRM,
                    @"Vous allez fermer cet écran référentiel. Toutes les modifications non enregistrées seront perdues.",
                    @"") == TYPE_RESULT_BOX.OK)
                {
                    ReferentielControl _ref = obj as ReferentielControl;
                    if (_ref != null)
                        _ref.Observer.CloseReferentielComposant(_ref);
                }
            }
        }

        #endregion

        #region Other

        private void ProgressOn(ReferentielControlVM vm)
        {
            Mode = MODE_COMPOSANT_REFERENTIIEL.LOADING;
        }

        private void ProgressOff(ReferentielControlVM vm)
        {
            Mode = MODE_COMPOSANT_REFERENTIIEL.LOADED;
            Mode = MODE_COMPOSANT_REFERENTIIEL.CONSULT;
        }

        private void ControlChamps_Event(ChampGenerique champ, string nameInfo, params object[] args)
        {
            if (nameInfo.Equals(ChampGenerique.BEFORE_UPDATE))
                Client.BeforeUpdate(CurrentElement as IDTO);
        }
        #endregion

        #endregion
    }
}
