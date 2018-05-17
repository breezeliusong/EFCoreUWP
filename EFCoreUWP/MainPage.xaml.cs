//using EFCoreModel;
using EFCoreModelLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EFCoreUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new NewBloggingContext())
            {
                Blogs.ItemsSource = db.Blogs.ToList();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new NewBloggingContext())
            {
                var blog = new EFCoreModelLibrary.Blog { Url = NewBlogUrl.Text };
                db.Blogs.Add(blog);
                db.SaveChanges();

                var db2 = new BloggingContext();
                foreach(var item in db2.Blogs)
                {
                    var Newblog = new EFCoreModelLibrary.Blog { Url = item.Url };
                    db.Blogs.Add(Newblog);
                    db.SaveChanges();
                }
                //var blog2 = new EFCoreModelLibrary.Blog { Url = NewBlogUrl.Text+"DB1" };

                //db2.Blogs.Add(blog2);
                //db2.SaveChanges();

                ObservableCollection<EFCoreModelLibrary.Blog> list = new ObservableCollection<EFCoreModelLibrary.Blog>(db.Blogs.ToList());
                //foreach(var item in db2.Blogs.ToList())
                //{
                //    list.Add(item);
                //}

                Blogs.ItemsSource = list;
            }
        }
    }
}
