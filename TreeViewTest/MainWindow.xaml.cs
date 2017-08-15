using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TreeViewTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DirectoryStructureViewModel();
        }

        #endregion


        //private void File_DoubleClick(object sender , System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    var item = (TreeViewItem)sender;
        //    try
        //    {
        //        System.Diagnostics.Process.Start(item.Tag.ToString());
        //    }
        //    catch (System.ComponentModel.Win32Exception)
        //    {
        //        MessageBox.Show("File not found.");
        //    }
        //}

    }
}