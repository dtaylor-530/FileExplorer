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
                    Files = files.SelectMany(_ => System.IO.Directory.GetFiles(_.DirectoryPath));
                    OnPropertyChanged(() => (Files));
                }
            }

        }





        private List<PathViewModel> selectedItems =  new List<PathViewModel>();



    }






    //public static class AttachedProperties
    //{
    //    #region AttachedProperties.SelectedItems Attached Property
    //    public static System.Collections.IList GetSelectedItems(ListBox obj)
    //    {
    //        return (System.Collections.IList)obj.GetValue(SelectedItemsProperty);
    //    }

    //    public static void SetSelectedItems(ListBox obj, System.Collections.IList value)
    //    {
    //        obj.SetValue(SelectedItemsProperty, value);
    //    }

    //    public static readonly DependencyProperty
    //        SelectedItemsProperty =
    //            DependencyProperty.RegisterAttached(
    //                "SelectedItems",
    //                typeof(System.Collections.IList),
    //                typeof(AttachedProperties),
    //                new PropertyMetadata(null,
    //                    SelectedItems_PropertyChanged));

    //    private static void SelectedItems_PropertyChanged(
    //        DependencyObject d,
    //        DependencyPropertyChangedEventArgs e)
    //    {
    //        var lb = d as ListBox;
    //        System.Collections.IList coll = e.NewValue as System.Collections.IList;

    //        //  If you want to go both ways and have changes to 
    //        //  this collection reflected back into the listbox...
    //        if (coll is System.Collections.Specialized.INotifyCollectionChanged)
    //        {
    //            (coll as System.Collections.Specialized.INotifyCollectionChanged)
    //                .CollectionChanged += (s, e3) =>
    //                {
    //                //  Haven't tested this branch -- good luck!
    //                if (null != e3.OldItems)
    //                        foreach (var item in e3.OldItems)
    //                            lb.SelectedItems.Remove(item);
    //                    if (null != e3.NewItems)
    //                        foreach (var item in e3.NewItems)
    //                            lb.SelectedItems.Add(item);
    //                };
    //        }

    //        if (null != coll)
    //        {
    //            if (coll.Count > 0)
    //            {
    //                //  Minor problem here: This doesn't work for initializing a 
    //                //  selection on control creation. 
    //                //  When I get here, it's because I've initialized the selected 
    //                //  items collection that I'm binding. But at that point, lb.Items 
    //                //  isn't populated yet, so adding these items to lb.SelectedItems 
    //                //  always fails. 
    //                //  Haven't tested this otherwise -- good luck!
    //                lb.SelectedItems.Clear();
    //                foreach (var item in coll)
    //                    lb.SelectedItems.Add(item);
    //            }

    //            lb.SelectionChanged += (s, e2) =>
    //            {
    //                if (null != e2.RemovedItems)
    //                    foreach (var item in e2.RemovedItems)
    //                        coll.Remove(item);
    //                if (null != e2.AddedItems)
    //                    foreach (var item in e2.AddedItems)
    //                        coll.Add(item);
    //            };
    //        }
    //    }
    //    #endregion AttachedProperties.SelectedItems Attached Property
    //}


}
