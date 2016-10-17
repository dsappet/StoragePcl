using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
using PCLStorage.Exceptions;

namespace StoragePCL
{
    public class FileAccess
    {
        internal static async Task WriteFileAsync(string contents, IFolder rootFolder)
        {
            
            // get hold of the file system
            //IFolder rootFolder =  FileSystem.Current.LocalStorage;

            // create a folder, if one does not exist already
            IFolder folder = await rootFolder.CreateFolderAsync(Constants.StorageSubFolder, CreationCollisionOption.OpenIfExists);

            // create a file, overwriting any existing file
            IFile file = await folder.CreateFileAsync(Constants.StorageFileName, CreationCollisionOption.ReplaceExisting);

            // populate the file with some text
            await file.WriteAllTextAsync(contents);
            // 
        }

        internal static async Task<string> ReadFileAsync(IFolder rootFolder)
        {
            //IFolder rootFolder = FileSystem.Current.LocalStorage;
            //IFolder folder = await rootFolder.GetFolderAsync(Constants.StorageSubFolder);
            // create a folder, if one does not exist already
            IFolder folder = await rootFolder.CreateFolderAsync(Constants.StorageSubFolder, CreationCollisionOption.OpenIfExists);
            if (folder == null) return null;
            // create a file, overwriting any existing file
            IFile file = await folder.CreateFileAsync(Constants.StorageFileName, CreationCollisionOption.OpenIfExists);
            //IFile file = await folder.GetFileAsync(Constants.StorageFileName);
            var contents = await file.ReadAllTextAsync();
            return contents;
        }
    }
}
