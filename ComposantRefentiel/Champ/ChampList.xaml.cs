using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace ComposantReferentiel.Champ
{
    /// <summary>
    /// Logique d'interaction pour ChampList.xaml
    /// </summary>
    public partial class ChampList : ChampGenerique
    {
        #region Attribut(s)


        #endregion

        #region Propriété(s)

        #region DependencyProperty

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(string[]), typeof(ChampList), new PropertyMetadata(new string[100], new PropertyChangedCallback(ChampList.SourceListChanged)));
        public string[] SourceList
        {
            get
            {
                return (string[])base.GetValue(ChampList.SourceProperty);
            }
            set
            {
                base.SetValue(ChampList.SourceProperty, value);
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
                    if (this.combobox.SelectedItem.ToString().Equals(string.Empty))
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

        public ChampList()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Méthode(s)

        private static void SourceListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChampList champList = d as ChampList;
            if (champList != null)
            {
                if (!champList.SourceList.Contains(String.Empty))
                {
                    List<String> l = champList.SourceList.ToList();
                    l.Insert(0, String.Empty);
                    champList.SourceList = l.ToArray();
                }

                champList.combobox.ItemsSource = new List<string>(champList.SourceList);
            }
        }

        public override void PlugBinding()
        {
            this.combobox.SetBinding(Selector.SelectedItemProperty, base.NomBDD);
        }

        #endregion

    }
}
