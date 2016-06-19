using GeneralServices.PatternBuilder;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace ComposantReferentiel.RapportErreur
{
    /// <summary>
    /// Construit la hiérarchie des rapports d'erreur quand ce sont des problèmes d'intégrité en base
    /// </summary>
    public class BuilderRapportOracleException : IBuilder
    {
        /// <summary>
        /// Liste des rapports retourné par GetResult()
        /// </summary>
        private List<IBuilt> listBuilt = new List<IBuilt>();

        /// <summary>
        /// Permet de construire les rapports d'erreurs.
        /// </summary>
        /// <param name="build">Exception permettant d'avoir les différentes erreurs. Doit être du type DbUpdateException</param>
        public void BuildPart(object build)
        {
            DbUpdateException ex = build as DbUpdateException;
            if (ex != null)
            {
                // Chaque DbEntityEntry correspond à un rapport d'erreur sur une entité
                foreach (DbEntityEntry current in ex.Entries)
                {
                    Rapport rapport = new Rapport(current.Entity.ToString());
                    rapport.Add(new Erreur
                    {
                        //Title = ex.InnerException.InnerException.Message
                        Title = ex.InnerException.Message
                    });
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
