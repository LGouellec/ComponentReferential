using GeneralServices.PatternBuilder;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace ComposantReferentiel.RapportErreur
{
    /// <summary>
    /// Construit la hiérarchie des rapports d'erreur quand ce sont des problèmes de commit en base
    /// </summary>
    public class BuilderRapportValidation : IBuilder
    {
        /// <summary>
        /// Liste des rapports retourné par GetResult()
        /// </summary>
        private List<IBuilt> listBuilt = new List<IBuilt>();

        /// <summary>
        /// Permet de construire les rapports d'erreurs.
        /// </summary>
        /// <param name="build">Exception permettant d'avoir les différentes erreurs. Doit être du type  List[DbEntityValidationResult]</param>
        public void BuildPart(object build)
        {
            List<DbEntityValidationResult> list = build as List<DbEntityValidationResult>;
            if (list != null)
            {
                // Chaque DbEntityValidationResult correspond à un rapport d'erreur sur une entité
                foreach (DbEntityValidationResult current in list)
                {
                    Rapport rapport = new Rapport(current.Entry.Entity.ToString());
                    // Chaque DbValidationError correspond à un message d'erreur sur un champ de l'entité
                    foreach (DbValidationError current2 in current.ValidationErrors)
                    {
                        rapport.Add(new Erreur
                        {
                            Title = current2.ErrorMessage
                        });
                    }
                    this.listBuilt.Add(rapport);
                }
            }
        }

        /// <summary>
        /// Retourne la liste des rapports d'erreurs correctement construit
        /// </summary>
        /// <returns>Retourne la liste des rapports d'erreurs</returns>
        public List<IBuilt> GetResult()
        {
            return this.listBuilt;
        }
    }
}
