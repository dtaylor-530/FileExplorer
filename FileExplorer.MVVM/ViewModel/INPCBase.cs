using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace FileExplorer.ViewModel
{
    public abstract class INPCBase : INotifyPropertyChanged
    {
        //#region INotifyPropertyChanged Implementation
        ///// <summary>
        ///// Occurs when any properties are changed on this object.
        ///// </summary>
        //public event PropertyChangedEventHandler PropertyChanged;


        ///// <summary>
        /////  raises the PropertyChanged event for one to many properties.
        ///// </summary>
        ///// <param name="propertyNames">The names of the properties that changed.</param>
        //public virtual void NotifyChanged(params string[] propertyNames)
        //{
        //    foreach (string name in propertyNames)
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        //}

        ///// <summary>
        /////  raises the PropertyChanged event for single property
        /////  propertyname can be left null (e.g OnPropertyChanged()) if called from body of property 
        ///// </summary>
        //public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}


        //#endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var body = propertyExpression.Body as MemberExpression;
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(body.Member.Name));
        }
    }

}
