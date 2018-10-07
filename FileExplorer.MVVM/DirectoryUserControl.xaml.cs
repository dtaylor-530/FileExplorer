using FileExplorer.ViewModel;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileExplorer.MVVM
{
    /// <summary>
    /// Interaction logic for DirectoryUserControl.xaml
    /// </summary>
    public partial class DirectoryUserControl : UserControl
    {
        public DirectoryUserControl(Func<string,object> func)
        {
            InitializeComponent();
            usercontrol.DataContext = new DirectoriesViewModel(func);
        }
    }




    public class DirectoriesViewModel 
    {


        private IList<DirectoryViewModel> selectedItems = new List<DirectoryViewModel>();
        //private Dispatcher _dispatcher;
        public IList<DirectoryViewModel> RootDirectories { get; private set; }
        public ReactiveProperty<UtilityWpf.ViewModel.InteractiveCollectionViewModel<FileViewModel>> Files { get; private set; } = new ReactiveProperty<UtilityWpf.ViewModel.InteractiveCollectionViewModel<FileViewModel>>();

        public ReadOnlyReactiveProperty<object> Output { get; }

        public ReactiveProperty<List<DirectoryViewModel>> SelectedItems { get; } = new ReactiveProperty<List<DirectoryViewModel>>();


        public DirectoriesViewModel(Func<string, object> func)
        {
            this.RootDirectories = new List<DirectoryViewModel>();
            foreach (var di in System.IO.Directory.GetDirectories(""))
            {
                try { this.RootDirectories.Add(new DirectoryViewModel(di, a => System.IO.Path.GetFileNameWithoutExtension(a))); }
                catch { }
            }

            Output = Files
                .Where(so => so != null)
                .Select(_ => _.Selected.Where(o => o != null)
                .Select(_d => (_d.FilePath)))
                .Switch()
                .Select(_ => func(_))
                .ToReadOnlyReactiveProperty();

            SelectedItems.Subscribe(value =>
            {
                selectedItems = value as List<DirectoryViewModel>;
                     var yt = value.Select(_ => new DirectoryInfo(_.FilePath)).ToArray();
                       var x = PathHelper.GetFilesInSubDirectories(yt);
                       var fvms = x.Select(_ => new FileViewModel(_, PathHelper.FileMap)).ToArray();
                       Files.Value = new UtilityWpf.ViewModel.InteractiveCollectionViewModel<FileViewModel>(fvms, Application.Current.Dispatcher );
            });
        }



        //public List<DirectoryViewModel> SelectedItems
        //{
        //    get
        //    {
        //        return selectedItems;
        //    }
        //    set
        //    {
        //        selectedItems = value as List<DirectoryViewModel>;
        //        var yt = value.Select(_ => new DirectoryInfo(_.DirectoryPath)).ToArray();
        //        var x = BL.Class1.GetFilesInSubDirectories(yt);
        //        var fvms = x.Select(_ => new FileViewModel(_, BL.Class1.FileMap)).ToArray();
        //        Files.Value = new UtilityWpf.ViewModel.SelectableCollectionViewModel<FileViewModel>(fvms, _dispatcher);
        //    }
        //    //OnPropertyChanged(() => (Files));
        //}
    }
}
