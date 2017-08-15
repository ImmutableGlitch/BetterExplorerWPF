
using System.Collections.ObjectModel;
using System.Linq;

namespace TreeViewTest
{
    /// <summary>
    /// The VM for the applications main treeView
    /// </summary>
    class DirectoryStructureViewModel : BaseViewModel
    {
        /// <summary>
        /// A list of all the system directories
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        public DirectoryStructureViewModel()
        {
            // Get the logical drives of the system
            var children = DirectoryStructure.GetLogicalDrives();

            // Create view models
            Items = new ObservableCollection<DirectoryItemViewModel>
                (children.Select(drive => new DirectoryItemViewModel(drive.FullPath,DirectoryItemType.Drive)));
        }
    }
}
