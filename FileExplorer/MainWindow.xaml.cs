using FileExplorer.ViewModel;
using PropertyTools.Wpf;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;



namespace FileExplorer.Terminal
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
       
            this.InitializeComponent();
            this.DataContext = new ViewModel();
        }

    }

    public class ViewModel : INPCBase
    {
        public System.Collections.ObjectModel.ObservableCollection<DirectoryViewModel> RootDirectories { get; } = new System.Collections.ObjectModel.ObservableCollection<DirectoryViewModel>();
        public IEnumerable<string> Files { get; private set; }



        public ViewModel()
        {
 
            foreach (var di in DriveInfo.GetDrives().Skip(1).Take(1))
            {
                try
                {
                    this.RootDirectories.Add(new DirectoryViewModel(di.RootDirectory.FullName));
                }
                catch
                {

                }
            }
        }




        public System.Collections.IList SelectedItems
        {
            //get
            //{
            //   return selectedItems;
            //}
            set
            {
                selectedItems = value as List<PathViewModel>;
                if (value != null & value.Count > 0)
                {
                    var files = (value.Cast<PathViewModel>());
                    try
                    {
                        Files = files.SelectMany(_ => System.IO.Directory.GetFiles(_.DirectoryPath));
                        OnPropertyChanged(() => (Files));
                    }
                    catch
                    { }
                  
                }
            }

        }


        private List<PathViewModel> selectedItems =  new List<PathViewModel>();



    }





}
