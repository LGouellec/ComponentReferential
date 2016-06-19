using GeneralServices.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposantRefentiel.BLL
{
    public interface IBLLReferentiel
    {
        /// <summary>
        /// Doit retourner l'ensemble des DTO affichables sur l'écran référentiel.
        /// </summary>
        /// <returns></returns>
        List<IDTO> SelectAllBusiness();

        /// <summary>
        /// Doit sauvegarder les changements
        /// </summary>
        /// <param name="list">Listes des DTO affichés à l'écran</param>
        /// <returns>Retourne le nombre d'enregistrés impactés</returns>
        int SaveChanges(List<IDTO> list);

        /// <summary>
        /// Doit annuler les modifications effectuées par l'utilisateur
        /// </summary>
        void RollBack();

        /// <summary>
        /// Doit supprimer l'élément passé en paramètres
        /// </summary>
        /// <param name="element">Element à supprimer</param>
        /// <returns>Retourne le nombre d'enregistrés impactés</returns>
        int Remove(object element);

        /// <summary>
        /// Doit créer un élément DTO vide
        /// </summary>
        /// <returns>Retourne l'élement crée</returns>
        IDTO CreateElement();

        /// <summary>
        /// Doit retourner les possibles erreures rencontrés par la DAO lors de la sauvegarde
        /// </summary>
        /// <returns></returns>
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
    }
}
