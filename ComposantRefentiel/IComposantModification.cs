using ComposantRefentiel.BLL;
using GeneralServices.Model;
using System.Windows.Controls;

namespace ComposantReferentiel
{
    /// <summary>
    /// Cette interface doit etre implémenté par le controleur qui va "s'occuper" du composant.
    /// 
    /// Elle définit deux méthodes qui servent à avoir la main à instant t sur un objet permettant de le modifier :
    /// 
    /// AfterCreate(object o) -> Permet de modifier cet objet juste après sa création en vue de modifier par exemple les champs RESP_MISE_A_JOUR, DATE_CREATION, DATE_MODIF ou autres champ.
    /// Il est obligatoire de modifier tous les champs booleans en les mettant à 'N' dans cette méthode en attendant de trouver une solution plus propre.
    /// 
    /// BeforeUpdate(object o) -> Permet de modifier cet objet juste avant de faire un UPDATE dessus en base. Notament modifier les chams RESP_MISE_A_JOUR et DATE_MODIF.
    /// 
    /// Si vous n'avez pas de modification à faire sur ces objets, implémenter l'interface en laissant les corps des méthodes vides.
    /// 
    /// Cette interface a été créer pour laisser la main un peu à l'utilisateur et pour aussi pouvoir mettre à jour 
    /// ces champs de traçabilité qui se sont pas affichés dans la grille des données.
    /// </summary>
    public interface IComposantModification
    {
        /// <summary>
        /// Est appelé après la création de l'élément. Permet de modifier certains attributs de l'objet en paramètre.
        /// </summary>
        /// <param name="o">Objet à modifié</param>
        void AfterCreate(ref IDTO o);

        /// <summary>
        /// Est appelé avant la mise à jour de l'élément. Permet de modifier certains attributs de l'objet en paramètre.
        /// </summary>
        /// <param name="o">Objet à modifié</param>
        object BeforeUpdate(IDTO o);

        /// <summary>
        /// Permet de faire connaître au composant référentiel le repository correspondant à la table en base de données ainsi que la BLL.
        /// </summary>
        /// <returns>Retourne la BLL du composant référentiel</returns>
        IBLLReferentiel GetBLLReferentiel();

        /// <summary>
        /// Retourne les paramétres du fichier CSV du composant
        /// </summary>
        /// <returns>Contient le séparateur et le header du fichier</returns>
        ParamExportCSV GetParam();

        /// <summary>
        /// Est appélé quand l'utilisateur vient de cliquer sur le bouton Fermer du composant
        /// </summary>
        /// <param name="control">Control Graphique à fermer.</param>
        void CloseReferentielComposant(ReferentielControl control);
    }
}
