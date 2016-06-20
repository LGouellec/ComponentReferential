using ComposantRefentiel.BLL;
using GeneralServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleComposantRef.StubData
{
    public class GetDatas : IBLLReferentiel
    {
        public static List<Data> All()
        {
            return new List<Data>()
            {
                new Data(){FirstName = "TOTO", LastName = "TOTO", Age = 12, Profil = null},
                new Data(){FirstName = "TITI", LastName = "TITI", Age = 13, Profil = null},
                new Data(){FirstName = "TATA", LastName = "TATA", Age = 90, Profil = null},
                new Data(){FirstName = "TUTU", LastName = "TUTU", Age = 25, Profil = null},
                new Data(){FirstName = "TETE", LastName = "TETE", Age = 2, Profil = null}
            };
        }

        #region IBLLReferentiel Membres

        public List<GeneralServices.Model.IDTO> SelectAllBusiness()
        {
            return All().ToList<IDTO>();
        }

        public int SaveChanges(List<GeneralServices.Model.IDTO> list)
        {
            // If you are connection data base, use this method for persist DTO in database
            return 1;
        }

        public void RollBack()
        {
            // Rollback for respect pattern UOW
        }

        public int Remove(object element)
        {
            // Remove in database this element
            return 0;
        }

        public GeneralServices.Model.IDTO CreateElement()
        {
            return new Data();
        }

        public IEnumerable<System.Data.Entity.Validation.DbEntityValidationResult> GetValidationErrors()
        {
            // If you are connection data base, use this method for get errors save in database
            return null;
        }

        #endregion
    }
}
