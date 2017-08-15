using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace TreeViewTest
{ 
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The item Type
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The path to the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// Name of this directory item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }

        /// <summary>
        /// List of all children contained within this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Constructor

        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            // Create commands
            ExpandCommand = new RelayCommand(Expand);

            // Set path and type
            FullPath = fullPath;
            Type = type;

            ClearChildren();
        }

        #endregion

        #region Helper Functions

        /// <summary>
        /// Indicates if item can be expanded
        /// </summary>
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        public bool IsExpanded
        {
            get => Children?.Count(f => f != null) > 0;
            set
            {
                // if UI wants to expand
                if (value == true)
                    // find all children
                    Expand();
                else
                    this.ClearChildren();
            }
        }

        /// <summary>
        /// Clear all children from the list
        /// </summary>
        private void ClearChildren()
        {
            // clear items
            Children = new ObservableCollection<DirectoryItemViewModel>();

            // add dummy item if not file, to allow expand icon
            if (Type != DirectoryItemType.File)
                Children.Add(null);
        }

        #endregion

        /// <summary>
        /// Expands this directory and finds all children
        /// </summary>
        private void Expand()
        {
            // a file can't be expanded
            if (Type == DirectoryItemType.File)
                return;

            // when expanded, find all children
            Children = new ObservableCollection<DirectoryItemViewModel>
                (DirectoryStructure.GetDirectoryContents(FullPath).Select
                (content => new DirectoryItemViewModel(content.FullPath,content.Type)));
        }
    }
}
