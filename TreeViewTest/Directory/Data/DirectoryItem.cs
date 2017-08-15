namespace TreeViewTest
{
    /// <summary>
    /// Info about a directory item such as a drive,folder,file
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// The item Type
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The path to the item
        /// </summary>
        public string FullPath {get; set;}

        /// <summary>
        /// Name of this directory item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }
    }
}
