using ComposantReferentiel.Champ;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ComposantReferentiel
{
    /// <summary>
    /// Logique d'interaction pour ControlChamps.xaml
    /// </summary>
    public partial class ControlChamps : UserControl
    {
        #region Attribut(s)

        //private MODE_COMPOSANT_REFERENTIIEL mode;

        #endregion

        #region Propriété(s)

        #region DependencyProperty

        #region Objet courant du composant
        public static readonly DependencyProperty CurrentElementProperty = DependencyProperty.Register("CurrentElement", typeof(object), typeof(ControlChamps), new PropertyMetadata(null, new PropertyChangedCallback(ControlChamps.CurrentElementChanged)));
        public object CurrentElement
        {
            get
            {
                return base.GetValue(ControlChamps.CurrentElementProperty);
            }
            set
            {
                base.SetValue(ControlChamps.CurrentElementProperty, value);
            }
        }
        private static void CurrentElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //ControlChamps controlChamps = d as ControlChamps;
            //if (controlChamps != null)
            //    foreach (ChampGenerique current in controlChamps.CollectionChamps)
            //        current.DataContext = e.NewValue;
        }
        #endregion

        #region Champs
        public static readonly DependencyProperty CollectionChampsProperty = DependencyProperty.Register("CollectionChamps", typeof(Champs), typeof(ControlChamps), new PropertyMetadata(new Champs(), new PropertyChangedCallback(ControlChamps.CollectionChampsChanged)));
        public Champs CollectionChamps
        {
            get
            {
                return (Champs)base.GetValue(ControlChamps.CollectionChampsProperty);
            }
            set
            {
                base.SetValue(ControlChamps.CollectionChampsProperty, value);
            }
        }
        private static void CollectionChampsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ControlChamps controlChamps = d as ControlChamps;
            if (controlChamps != null)
            {
                controlChamps.BuildChampControl(e.NewValue as Champs);
                controlChamps.ModeChanged(controlChamps.Mode);
            }
        }
        #endregion

        #region Mode du composant
                public MODE_COMPOSANT_REFERENTIIEL Mode
        {
            get { return (MODE_COMPOSANT_REFERENTIIEL)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(MODE_COMPOSANT_REFERENTIIEL), typeof(ControlChamps), new PropertyMetadata(MODE_COMPOSANT_REFERENTIIEL.Unknow, ModeChanged));

        private static void ModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ControlChamps control = d as ControlChamps;
            if (control != null && e.NewValue is MODE_COMPOSANT_REFERENTIIEL)
                control.ModeChanged((MODE_COMPOSANT_REFERENTIIEL)e.NewValue);
        }
        #endregion

        #endregion

        #endregion

        #region Event

        public delegate void Update(ChampGenerique champ, String nameInfo, params object[] args);
        public event Update Event;

        protected void Change(ChampGenerique champ, String nameInfo, params object[] args)
        {
            // Check if there are any Subscribers
            if (Event != null)
            {
                // Call the Event
                Event(champ, nameInfo, args);
            }
        }

        #endregion

        #region Constructeur(s)

        public ControlChamps()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Méthode(s)

        public void BuildChampControl(Champs c)
        {
            this.InitGrid();
            int num = 0;
            foreach (ChampGenerique current in c)
            {
                // Abonnement du control au lost focus du champ
                current.Event += Event_LostFocus;
                // Data Context du champ est lié à l'objet courant de la grille.
                Binding binding = new Binding("CurrentElement");
                binding.NotifyOnSourceUpdated = true;
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                binding.Mode = BindingMode.TwoWay;
                binding.Source = this;
                current.SetBinding(UserControl.DataContextProperty, binding);
                // Titre du champ
                Label label = new Label();
                label.Content = current.NomChamp;
                label.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);
                this.GridRoot.RowDefinitions.Add(new RowDefinition());
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, num);
                this.GridRoot.Children.Add(label);
                // UserControl = Champ
                current.SetValue(FrameworkElement.MarginProperty, new Thickness(15.0, 5.0, 5.0, 10.0));
                current.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);
                Grid.SetColumn(current, 1);
                Grid.SetRow(current, num);
                this.GridRoot.Children.Add(current);
                num++;
            }
        }

        private void InitGrid()
        {
            this.GridRoot.Children.Clear();
            this.GridRoot.ColumnDefinitions.Clear();
            this.GridRoot.RowDefinitions.Clear();
            this.GridRoot.ColumnDefinitions.Add(new ColumnDefinition());
            this.GridRoot.ColumnDefinitions.Add(new ColumnDefinition());
        }

        public void ModeChanged(MODE_COMPOSANT_REFERENTIIEL mode)
        {
            foreach (ChampGenerique current in this.CollectionChamps)
                current.ModeChanged(mode);
        }

        protected void Event_LostFocus(ChampGenerique champ, String information, params object[] args)
        {
            if(information.Equals(ChampGenerique.BEFORE_UPDATE))
                Change(champ, information, args);
        }

        #endregion
    }
}
