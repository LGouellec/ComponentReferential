using ComposantReferentiel;
using GeneralServices.ViewModel;
using SampleComposantRef.StubData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SampleComposantRef.ViewModels
{
    public class SampleVM : ViewModelBase, IComposantModification
    {
        #region IComposantModification Membres

        public void AfterCreate(ref GeneralServices.Model.IDTO o)
        {
            // Call After creating new dto, if you want alterate object
            // For exemple DATE_CREATED, DATE_MODIFY, USER
        }

        public object BeforeUpdate(GeneralServices.Model.IDTO o)
        {
            // Call before updating , if you want alterate object
            // For exemple DATE_MODIFY, USER
            return o;
        }

        public ComposantRefentiel.BLL.IBLLReferentiel GetBLLReferentiel()
        {
            // Return BLL for component : Data Access 
            return new GetDatas();
        }

        public ParamExportCSV GetParam()
        {
            // Param for export or import CSV 
            return new ParamExportCSV();
        }

        public void CloseReferentielComposant(ReferentielControl control)
        {
            // This component have a button Close, if you want use it. Just close main window to control
            Window w = FindParentControl.FindParent<Window>(control);
            w.Close();
        }

        #endregion
    }
}
