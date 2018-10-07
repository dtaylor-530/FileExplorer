//using System;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.IO;
//using System.Linq;
//using System.Linq.Expressions;

//namespace FileExplorer.ViewModel
//{
  
//    public class DirectoryViewModel : FileSystemInfo //, INotifyPropertyChanged
//    {
//        public string DirectoryPath { get { return base.FullPath; } }

//        public string Directory
//        {
//            get
//            {
//                return System.IO.Path.GetDirectoryName(DirectoryPath);
//            }
//        }


//        public override string Name
//        {
//             get
//            {
//                var name= _map(DirectoryPath);
//                return String.IsNullOrEmpty(name) ? this.DirectoryPath : name;
//            }
 
//        }
//        //public new string Name
//        //{
//        //    set
//        //    {
//        //        this.DirectoryPath = Path.Combine(Directory, value);
//        //    }

//        //}

//        public DirectoryViewModel(string path,Func<string,string> map= null)
//        {
//            //this.DirectoryPath = path;

//            base.FullPath = path;
//            Func<string,string> dmap = (a) => System.IO.Path.GetDirectoryName(a);
//            _map = map ?? dmap;

//        }




//        private ObservableCollection<DirectoryViewModel> subDirectories;

//        //public ObservableCollection<DirectoryViewModel> SubDirectories
//        //{
//        //    get
//        //    {
//        //        if (subDirectories == null)
//        //        {
//        //            subDirectories = new ObservableCollection<DirectoryViewModel>();
//        //            try
//        //            {
//        //                foreach (var dir in System.IO.Directory.GetDirectories(DirectoryPath))
//        //                {
//        //                    subDirectories.Add(new DirectoryViewModel(dir));
//        //                }
//        //            }
//        //            catch
//        //            {

//        //            }
//        //        }
//        //        return subDirectories;
//        //    }
//        //}


//        public ObservableCollection<DirectoryViewModel> SubDirectories
//        {
//            get { subDirectories = subDirectories ?? this.Create(_map); return subDirectories; }
//        }

//        public bool HasItems
//        {
//            get { return SubDirectories.Count > 0; }
//        }

//        //private bool _isExpanded;
//        //private bool _isSelected;
//        private Func<string, string> _map;

//        public string DisplayName { get; set; }

//        //public bool IsExpanded
//        //{
//        //    get { return _isExpanded; }
//        //    set
//        //    {
//        //        //if (value != _isExpanded)
//        //        //{
//        //        _isExpanded = value;
//        //        OnPropertyChanged(() => IsExpanded);
//        //        //}
//        //    }
//        //}

//        //public virtual bool IsSelected
//        //{
//        //    get { return _isSelected; }
//        //    set
//        //    {
//        //        //if (value != _isSelected)
//        //        //{
//        //        _isSelected = value;
//        //        OnPropertyChanged(() => IsSelected);
//        //        //}
//        //    }
//        //}



//        public override bool Exists => throw new NotImplementedException();

//        public override string ToString()
//        {
//            return DirectoryPath;
//        }

//        public override void Delete()
//        {
//            throw new NotImplementedException();
//        }




//        //public event PropertyChangedEventHandler PropertyChanged;
//        //public void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
//        //{
//        //    var body = propertyExpression.Body as MemberExpression;
//        //    PropertyChangedEventHandler handler = PropertyChanged;
//        //    if (handler != null) handler(this, new PropertyChangedEventArgs(body.Member.Name));
//        //}
//    }






//    static class FileHelper
//    {

//        public static bool HasItems(this DirectoryViewModel dvm)
//        {

//            return dvm.SubDirectories.Count > 0;

//        }



//        public static ObservableCollection<DirectoryViewModel> Create(this DirectoryViewModel dvm, Func<string, string> map = null)
//        {
//            try
//            {
//                return new ObservableCollection<DirectoryViewModel>(
//                    System.IO.Directory.GetDirectories(dvm.DirectoryPath).Where(_ =>
//                _ != null).Select(dir => new DirectoryViewModel(dir,map)));
//            }
//            catch
//            {
//                //Console.WriteLine("error adding directories");
//            }
//            return null;
//        }

//    }

//}