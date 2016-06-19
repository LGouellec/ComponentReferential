using ComposantReferentielV2.Converter;
using Microsoft.Win32;
using GeneralServices.ViewModel;
using System;
using System.IO;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;

namespace ComposantReferentiel.Champ.ChampVM
{
    public class ChampImageVM : ViewModelBase
    {
        #region Attribut(s)


        #endregion

        #region Propriété(s)

        #region Command

        /// <summary>
        /// Command permettant d'ajouter / modifier l'image
        /// </summary>
        public ICommand AddImage
        {
            get;
            private set;
        }

        /// <summary>
        /// Command permettant de supprimer l'image
        /// </summary>
        public ICommand EraseImage
        {
            get;
            private set;
        }

        #endregion

        #endregion

        #region Constructeur(s)

        /// <summary>
        /// Constructeur sans paramétre
        /// </summary>
        public ChampImageVM()
        {
            this.AddImage = new RelayCommand(ExAddImage, CanExAddImage);
            this.EraseImage = new RelayCommand(ExEraseImage, CanExEraseImage);
        }

        #endregion

        #region Méthode(s)

        #region Event

        private void ExAddImage(object obj)
        {
            ChampImage champImage = obj as ChampImage;
            if (champImage != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                // Construit la liste des extensions souhaitées
                string text = this.BuildExtensionFormat(champImage);
                openFileDialog.Filter = ((!string.IsNullOrEmpty(text)) ? text : "All files (*.*)|*.*");
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        Stream stream;
                        if ((stream = openFileDialog.OpenFile()) != null)
                        {
                            using (stream)
                            {
                                byte[] array = new byte[stream.Length];
                                stream.Read(array, 0, (int)stream.Length);
                                champImage.img.Source = (new ConverterImage().Convert(array, null, null, null) as ImageSource);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
            }
        }

        private void ExEraseImage(object obj)
        {
            ChampImage champImage = obj as ChampImage;
            champImage.img.Source = null;
        }

        private bool CanExEraseImage(object arg)
        {
            // On peut supprimer que si il y a déjà une image dans le champ
            ChampImage champImage = arg as ChampImage;
            return champImage != null && champImage.img.Source != null;
        }

        private bool CanExAddImage(object arg)
        {
            // On peut ajouter une image que si l'utilisateur a sélectionné une entité dans la grille
            ChampImage champImage = arg as ChampImage;
            return champImage != null && champImage.DataContext != null;
        }

        #endregion

        /// <summary>
        /// Construit sous forme d'une chaine de caractères correctement formatées les extensions possibles
        /// </summary>
        /// <param name="champ">Champ Image</param>
        /// <returns>Retourne une chaine correctement formattée</returns>
        private string BuildExtensionFormat(ChampImage champ)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string text = string.Empty;
            if (champ.ListExtension != null)
            {
                string[] listExtension = champ.ListExtension;
                for (int i = 0; i < listExtension.Length; i++)
                {
                    string text2 = listExtension[i];
                    stringBuilder.Append(text2).Append(" Files (*.").Append(text2.ToLower()).Append(")|*.").Append(text2.ToLower()).Append("|");
                }
                text = stringBuilder.ToString();
                text = text.Substring(0, text.Length - 1);
            }
            return text;
        }

        #endregion
    }
}
