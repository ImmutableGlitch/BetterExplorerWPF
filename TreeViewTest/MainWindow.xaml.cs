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
        }

        #endregion

        #region On Loaded
        private void Window_Loaded(object sender , RoutedEventArgs e)
        {
            // Grab logical drives as TreeItems and set their header and tag
            foreach (var drive in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem()
                {
                    Header = drive ,
                    Tag = drive
                };

                // Add a dummy item to the drives to produce an expandable TreeItem
                item.Items.Add(null);
                item.Expanded += Folder_Expanded;

                // Add the TreeItem to the form
                folderView.Items.Add(item);
            }
        }
        #endregion

        #region Folder Expanded
        private void Folder_Expanded(object sender , RoutedEventArgs e)
        {
            #region Initial Checks

            // Get the TreeItem source
            var item = (TreeViewItem)sender;
            // If it doesn't contain the dummy item
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            //Remove the dummy item
            item.Items.Clear();
            #endregion


            #region Get Folders

            var fullPath = (string)item.Tag;

            // Try to get the sub dirs from the folder
            var directories = new List<string>();
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch (System.Exception)
            {
                throw;
            }

            // For each sub dir found, create a new TreeItem
            directories.ForEach(directoryPath =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(directoryPath) ,
                    Tag = directoryPath
                };

                // Add dummy item so subItem can be expandible
                subItem.Items.Add(null);
                subItem.Expanded += Folder_Expanded;
                item.Items.Add(subItem);
            });
            #endregion

            #region Get Files

            var files = new List<string>();
            // Try and find files within the parent TreeItem
            try
            {
                var f = Directory.GetFiles(fullPath);
                if (f.Length > 0)
                    files.AddRange(f);
            }
            catch (Exception)
            {

                throw;
            }

            files.ForEach(filePath =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(filePath),
                    Tag = filePath
                };

                subItem.MouseDoubleClick += File_DoubleClick;

                item.Items.Add(subItem);
            });

            #endregion

            this.Title = $"{fullPath} ({item.Items.Count} items)";
        }

        private void File_DoubleClick(object sender , System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (TreeViewItem)sender;
            try
            {
                System.Diagnostics.Process.Start(item.Tag.ToString());
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("File not found.");
            }
        }

        #endregion

        #region Helper Functions
        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            var normalisedPath = path.Replace('/' , '\\');
            // Index of last backslash
            var lastIndex = path.LastIndexOf('\\');

            if (lastIndex <= 0)
                return path;

            return path.Substring(lastIndex + 1);
        }
        #endregion

    }
}