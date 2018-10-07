using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace FileExplorer.ViewModel
{

    public class PathViewModel : FileSystemInfo
    {
        private Func<string, string> _map;

        public string FilePath { get { return base.FullPath; } }

        public string Directory { get { return System.IO.Path.GetDirectoryName(FilePath); } }

        public override string Name
        {
            get
            {
                var name = _map(FilePath);
                return String.IsNullOrEmpty(name) ? this.FilePath : name;
            }

        }

        public PathViewModel(string path, Func<string, string> map )
        {
            base.FullPath = path;
            _map = map ;

        }

        public string DisplayName { get; set; }

        public override bool Exists => throw new NotImplementedException();

        public override string ToString() => FilePath;

        private bool _isExpanded;
        private bool _isSelected;


        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                //if (value != _isExpanded)
                //{
                _isExpanded = value;
                OnPropertyChanged(() => IsExpanded);
                //}
            }
        }

        public virtual bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                //if (value != _isSelected)
                //{
                _isSelected = value;
                OnPropertyChanged(() => IsSelected);
                //}
            }
        }


        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var body = propertyExpression.Body as MemberExpression;
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(body.Member.Name));
        }


        private ObservableCollection<DirectoryViewModel> subDirectories;

        public ObservableCollection<DirectoryViewModel> SubDirectories
        {
            get
            {
                if (subDirectories == null)
                {
                    subDirectories = new ObservableCollection<DirectoryViewModel>();
                    try
                    {
                        foreach (var dir in System.IO.Directory.GetDirectories(DirectoryPath))
                        {
                            subDirectories.Add(new DirectoryViewModel(dir));
                        }
                    }
                    catch
                    {

                    }
                }
                return subDirectories;
            }
        }
        public bool HasItems
        {
            get { return SubDirectories.Count > 0; }
        }

        public string DirectoryPath { get { return base.FullPath; } }

        //public ObservableCollection<DirectoryViewModel> SubDirectories
        //{
        //    get { subDirectories = subDirectories ?? this.Create(_map); return subDirectories; }
        //}


    }

    static class FileHelper
    {

        public static bool HasItems(this PathViewModel dvm)
        {

            return dvm.SubDirectories.Count > 0;

        }



        public static ObservableCollection<PathViewModel> Create(this PathViewModel dvm, Func<string, string> map = null)
        {
            try
            {
                return new ObservableCollection<PathViewModel>(
                    System.IO.Directory.GetDirectories(dvm.DirectoryPath).Where(_ =>
                _ != null).Select(dir => new PathViewModel(dir, map)));
            }
            catch
            {
                //Console.WriteLine("error adding directories");
            }
            return null;
        }

    }




public class FileViewModel :PathViewModel
    {
    

        public FileViewModel(string path, Func<string, string> map = null):base(path, map ?? ((a) => System.IO.Path.GetFileName(a)))
        {
   
        }

    }

    public class DirectoryViewModel:PathViewModel
    {

        public DirectoryViewModel(string path, Func<string, string> map = null):base(path, map ?? ((a) => System.IO.Path.GetDirectoryName(a)))
        {

        }

    }







}