using ComposantReferentielV2.Champ;
using ComposantReferentielV2.Converter;
using ComposantReferentielV2.ViewModel;
using GeneralServices.Model;
using GeneralServices.ViewModel;
using System;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ComposantReferentiel
{
    public enum MODE_COMPOSANT_REFERENTIIEL
    {
        Unknow,
        CONSULT,
        ADD,
        ERASE,
        LOADING,
        LOADED
    }

    /// <summary>
    /// Logique d'interaction pour ReferentielControl.xaml
    /// </summary>
    public partial class ReferentielControl : UserControl
    {
        #region Attribut(s)

        private bool _firstLoaded = true;

        #endregion

        #region Propriété(s)

        #region DependencyProperty

        #region Champs  
        
        public static readonly DependencyProperty ChampsProperty = DependencyProperty.Register("CollectionChamps", typeof(Champs), typeof(ReferentielControl), new PropertyMetadata(new Champs(), ChampsUpdated));

        public Champs CollectionChamps
        {
            get
            {
                return (Champs)base.GetValue(ReferentielControl.ChampsProperty);
            }
            set
            {
                base.SetValue(ReferentielControl.ChampsProperty, value);
            }
        }

        private static void ChampsUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReferentielControl refControl = d as ReferentielControl;
            if(refControl != null)
            {
                ReferentielControlVM vm = refControl.Resources["vm"] as ReferentielControlVM;
                if (vm != null)
                    vm.ControlChamps.CollectionChamps = e.NewValue as Champs;
            }
        }
        
        #endregion

        #region Titre du composant
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(ReferentielControl), new PropertyMetadata(string.Empty));
        public string Title
        {
            get
            {
                return (string)base.GetValue(ReferentielControl.TitleProperty);
            }
            set
            {
                base.SetValue(ReferentielControl.TitleProperty, value);
            }
        }
        #endregion

        #region Observer du composant
        public static readonly DependencyProperty ObserverProperty = DependencyProperty.Register("Observer", typeof(IComposantModification), typeof(ReferentielControl), new PropertyMetadata(null, new PropertyChangedCallback(ReferentielControl.ObserverChanged)));
        public IComposantModification Observer
        {
            get
            {
                return (IComposantModification)base.GetValue(ReferentielControl.ObserverProperty);
            }
            set
            {
                base.SetValue(ReferentielControl.ObserverProperty, value);
            }
        }
        private static void ObserverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReferentielControl refControl = d as ReferentielControl;
            if (refControl != null)
            {
                ReferentielControlVM vm = refControl.Resources["vm"] as ReferentielControlVM;
                if (vm != null)
                    vm.Client = e.NewValue as IComposantModification;
            }
        }
        #endregion

        #region Taille Composant


        public int WidthGrid
        {
            get { return (int)GetValue(WidthGridProperty); }
            set { SetValue(WidthGridProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WidthGrid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthGridProperty =
            DependencyProperty.Register("WidthGrid", typeof(int), typeof(ReferentielControl), new PropertyMetadata(0, WidthGridChanged));

        private static void WidthGridChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReferentielControl control = d as ReferentielControl;
            if (control != null)
                control.dg.SetValue(DataGrid.WidthProperty, Convert.ToDouble(e.NewValue));
        }

    
        public int HeightGrid
        {
            get { return (int)GetValue(HeightGridProperty); }
            set { SetValue(HeightGridProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeightGrid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeightGridProperty =
            DependencyProperty.Register("HeightGrid", typeof(int), typeof(ReferentielControl), new PropertyMetadata(0, HeightGridChanged));

        private static void HeightGridChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReferentielControl control = d as ReferentielControl;
            if (control != null)
                control.dg.SetValue(DataGrid.HeightProperty, Convert.ToDouble(e.NewValue));
        }

        public int WidthControlChamp
        {
            get { return (int)GetValue(WidthControlChampProperty); }
            set { SetValue(WidthControlChampProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WidthControlChamp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthControlChampProperty =
            DependencyProperty.Register("WidthControlChamp", typeof(int), typeof(ReferentielControl), new PropertyMetadata(0, WidthControlChampChanged));

        private static void WidthControlChampChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReferentielControl control = d as ReferentielControl;
            if (control != null)
                if(control.Resources["vm"] is ReferentielControlVM)
                    (control.Resources["vm"] as ReferentielControlVM).ControlChamps.Width = Convert.ToInt32(e.NewValue);
        }

        public int HeightControlChamp
        {
            get { return (int)GetValue(HeightControlChampProperty); }
            set { SetValue(HeightControlChampProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WidthControlChamp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeightControlChampProperty =
            DependencyProperty.Register("HeightControlChamp", typeof(int), typeof(ReferentielControl), new PropertyMetadata(0, HeightControlChampChanged));

        private static void HeightControlChampChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReferentielControl control = d as ReferentielControl;
            if (control != null)
                if (control.Resources["vm"] is ReferentielControlVM)
                    (control.Resources["vm"] as ReferentielControlVM).ControlChamps.Height = Convert.ToInt32(e.NewValue);
        }

        

        
        #endregion

        #endregion

        #endregion

        #region Constructeur(s)

        public ReferentielControl()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Méthode(s)

        public void SaveChange()
        {
            //TODO : 
          // TODO : A finir UnitOfWorkGeneral.RollExcept<typeof(t)>(this.Context);
            //this.Context.SaveChanges();
        }

        private void dg_Loaded(object sender, RoutedEventArgs e)
        {
            if (_firstLoaded)
            {
                foreach (ChampGenerique current in this.CollectionChamps)
                {
                    if (current is ChampBoolean)
                    {
                        DataGridCheckBoxColumn dataGridCheckBoxColumn = new DataGridCheckBoxColumn();
                        dataGridCheckBoxColumn.Header = current.NomChamp;
                        dataGridCheckBoxColumn.Binding = new Binding(current.NomBDD)
                        {
                            Converter = new ConverterBoolean(),
                            Mode = BindingMode.OneWay,
                            NotifyOnSourceUpdated = true,
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                        };
                        this.dg.Columns.Add(dataGridCheckBoxColumn);
                    }
                    else
                    {
                        if (current is ChampImage)
                        {
                            DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn();
                            dataGridTemplateColumn.Header = current.NomChamp;
                            DataTemplate dataTemplate = new DataTemplate();
                            FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory(typeof(Image));
                            Binding binding = new Binding(current.NomBDD);
                            binding.Mode = BindingMode.OneWay;
                            binding.NotifyOnSourceUpdated = true;
                            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            binding.Converter = new ConverterImage();
                            frameworkElementFactory.SetValue(FrameworkElement.MaxHeightProperty, 40.0);
                            frameworkElementFactory.SetValue(FrameworkElement.MaxWidthProperty, 60.0);
                            frameworkElementFactory.SetValue(Image.SourceProperty, binding);
                            dataTemplate.VisualTree = frameworkElementFactory;
                            dataGridTemplateColumn.CellTemplate = dataTemplate;
                            this.dg.Columns.Add(dataGridTemplateColumn);
                        }
                        else
                        {
                            if (current is ChampDateTime)
                            {
                                DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn();
                                dataGridTemplateColumn.Header = current.NomChamp;
                                DataTemplate dataTemplate = new DataTemplate();
                                FrameworkElementFactory frameworkElementFactory2 = new FrameworkElementFactory(typeof(DatePicker));
                                Binding binding = new Binding(current.NomBDD);
                                binding.Mode = BindingMode.OneWay;
                                binding.NotifyOnSourceUpdated = true;
                                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                                binding.Converter = new ConverterDate();
                                frameworkElementFactory2.SetValue(UIElement.IsEnabledProperty, false);
                                frameworkElementFactory2.SetValue(DatePicker.TextProperty, binding);
                                dataTemplate.VisualTree = frameworkElementFactory2;
                                dataGridTemplateColumn.CellTemplate = dataTemplate;
                                this.dg.Columns.Add(dataGridTemplateColumn);
                            }
                            else
                            {
                                if (current is ChampList)
                                {
                                    DataGridComboBoxColumn dataGridComboBoxColumn = new DataGridComboBoxColumn();
                                    dataGridComboBoxColumn.Header = current.NomChamp;
                                    dataGridComboBoxColumn.ItemsSource = (current as ChampList).SourceList;
                                    Binding binding = new Binding(current.NomBDD);
                                    binding.Mode = BindingMode.OneWay;
                                    binding.NotifyOnSourceUpdated = true;
                                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                                    dataGridComboBoxColumn.SelectedValueBinding = binding;
                                    this.dg.Columns.Add(dataGridComboBoxColumn);
                                }
                                else
                                {
                                    if (current is ChampDecimal || current is ChampNumerique || current is ChampTexte)
                                    {
                                        DataGridTextColumn dataGridTextColumn = new DataGridTextColumn();
                                        dataGridTextColumn.Header = current.NomChamp;
                                        Binding binding = new Binding(current.NomBDD);
                                        binding.Mode = BindingMode.OneWay;
                                        binding.NotifyOnSourceUpdated = true;
                                        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                                        dataGridTextColumn.Binding = binding;
                                        this.dg.Columns.Add(dataGridTextColumn);
                                    }
                                    else
                                        this.dg.Columns.Add(current.CreateColumn());
                                }
                            }
                        }
                    }
                }
                _firstLoaded = false;
            }
        }

        private void ProgressOn(ReferentielControlVM vm)
        {
            vm.Mode = MODE_COMPOSANT_REFERENTIIEL.LOADING;
        }

        private void ProgressOff(ReferentielControlVM vm)
        {
            vm.Mode = MODE_COMPOSANT_REFERENTIIEL.LOADED;
            vm.Mode = MODE_COMPOSANT_REFERENTIIEL.CONSULT;
        }

        #endregion

    }
}
