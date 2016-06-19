using ReadAndWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposantReferentiel
{
    /// <summary>
    /// Paramètres du fichier CSV généré par le composant
    /// </summary>
    public class ParamExportCSV
    {
        #region Attribut(s)


        #endregion

        #region Propriété(s)

        /// <summary>
        /// Separateur CSV du fichier généré automatiquement par le composant
        /// </summary>
        public String Separateur
        {
            get;
            set;
        }

        /// <summary>
        /// Chemin du fichier généré
        /// </summary>
        public String Path
        {
            get;
            set;
        }

        /// <summary>
        /// Header du fichier généré automatiquement par le composant
        /// </summary>
        public HeaderCSV Header
        {
            get;
            set;
        }

        #endregion

        #region Constructeur(s)


        #endregion

        #region Méthode(s)

        /// <summary>
        /// Permet de retourner le header sous forme d'une chaine de caractères correctement formatée avec le Separateur
        /// </summary>
        /// <returns></returns>
        public String GetHeaderBody()
        {
            StringBuilder sb = new StringBuilder();

            foreach (String s in Header.Headers)
                sb.Append(s).Append(Separateur);

            return sb.ToString();
        }

        #endregion

    }
}
