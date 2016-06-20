using GeneralServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SampleComposantRef
{
    public class Data : IDTO
    {

        private String lastName;

        public String LastName
        {
            get { return lastName; }
            set { lastName = value; RaisePropertyChanged(() => LastName); }
        }

        private String firstName;

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; RaisePropertyChanged(() => FirstName); }
        }

        private Int32 age;

        public Int32 Age
        {
            get { return age; }
            set { age = value; RaisePropertyChanged(() => Age); }
        }

        private byte[] profil;

        public byte[] Profil
        {
            get { return profil; }
            set { profil = value; RaisePropertyChanged(() => Profil); }
        }
     

        #region IDTO Membres

        public T GetObject<T>(object param) where T : class
        {
            throw new NotImplementedException();
        }

        public void SetObject<T>(T t, object param) where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        #region INotifyPropertyChanged Membres

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            OnPropertyChanged((propertyExpression.Body as MemberExpression).Member.Name);
        }

        #endregion

        #region ISerializableCSV Membres

        public List<object> exposeAttributes()
        {
            // Use it for export data in component referential
            return new List<object>();
        }

        public string exposeHeader(string separateur)
        {
            // Header for CSV file export or import
            return String.Empty;
        }

        public bool importAttributes(string[] data)
        {
            // Import CSV data
            return true;
        }

        #endregion
    }
}
