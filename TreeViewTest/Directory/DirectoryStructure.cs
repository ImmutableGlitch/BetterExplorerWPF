using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TreeViewTest
{
    /// <summary>
    /// A helper class to query info about dirs
    /// </summary>
    public static class DirectoryStructure
    {
        public static List<DirectoryItem> GetLogicalDrives()
        {
            // Select all logical drives, convert them to DirectoryItems and return a list of those DirectoryItems
            return Directory.GetLogicalDrives().Select(
                drive => new DirectoryItem { FullPath = drive , Type = DirectoryItemType.Drive }).ToList();
        }
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            // Create empty list
            var items = new List<DirectoryItem>();

            #region Get Folders
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
            }
            catch (System.Exception)
            {
                throw;
            }
            #endregion

            #region Get Files
            
            try
            {
                var f = Directory.GetFiles(fullPath);
                if (f.Length > 0)
                    items.AddRange(f.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
            }
            catch{}
            #endregion

            return items;
        }
        /// <summary>
        /// Returns only the file or folder name without slashes
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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

    }
}
