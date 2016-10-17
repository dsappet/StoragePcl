using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
using StoragePCL.Enums;

namespace StoragePCL
{
    public class Storage
    {
        private List<Item> Items;
        private string AppDirectory;
        private IFolder rootFolder;
        public Storage(IFolder root)
        {
            rootFolder = root;
            //AppDirectory = dir;
            Items = new List<Item>();
            // Open and parse the file 
            // Load into Items or set it to a new. 

        }

        public async Task LoadAsync()
        {
            Items = await Parser.ParseFileAsync(rootFolder) ?? new List<Item>();
        }
        // Add creates a new key
        public async Task<EnumStorageStatusCode> InsertAsync(string key, string value, bool encryptValue = false)
        {
            if (string.IsNullOrWhiteSpace(key)) return EnumStorageStatusCode.KeyIsNull;
            if (value == null) return EnumStorageStatusCode.ValueIsNull;
            key = key.ToLower();
            if (encryptValue)
            {
                // Encrypt the value here
                value = Utilities.Security.Encrypt(value);
            }
            Items.Add(new Item(key, value, encryptValue));

            // Save the file here
            await Builder.BuildAndWriteFileAsync(Items, rootFolder);

            return EnumStorageStatusCode.Success;
        }

        public async Task<EnumStorageStatusCode> UpdateAsync(string key, string value, bool isEncrypted = false)
        {
            if (string.IsNullOrWhiteSpace(key)) return EnumStorageStatusCode.KeyIsNull;
            if (value == null) value = "";
            key = key.ToLower();
            var index = Items.FindIndex(x => x.Key == key);
            if (index == -1) return EnumStorageStatusCode.KeyNotFound;
            //var item = Items.Find(x => x.Key == key);
            //if(item == null) return EnumStorageStatusCode.KeyNotFound;

            if (isEncrypted)
            {
                Items[index].IsEncrypted = true;
                // ENCRYPT THE VALUE
                value = Utilities.Security.Encrypt(value);
            }
            Items[index].Value = value;

            // save the file (async)
            await Builder.BuildAndWriteFileAsync(Items, rootFolder);

            return EnumStorageStatusCode.Success;
        }

        public async Task<EnumStorageStatusCode> InsertOrUpdateAsync(string key, string value, bool isEncrypted = false)
        {
            if (string.IsNullOrWhiteSpace(key)) return EnumStorageStatusCode.KeyIsNull;
            if (value == null) value = "";
            key = key.ToLower();
            var index = Items.FindIndex(x => x.Key == key);
            if (index == -1)
            {
               return await InsertAsync(key,value,isEncrypted);
            }
            //var item = Items.Find(x => x.Key == key);
            //if(item == null) return EnumStorageStatusCode.KeyNotFound;

            if (isEncrypted)
            {
                Items[index].IsEncrypted = true;
                // ENCRYPT THE VALUE 
                value = Utilities.Security.Encrypt(value);
            }

            Items[index].Value = value;

            // save the file (async)
            await Builder.BuildAndWriteFileAsync(Items, rootFolder);

            return EnumStorageStatusCode.Success;
        }

        public string Get(string key, out EnumStorageStatusCode statusCode)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                statusCode =  EnumStorageStatusCode.KeyIsNull;
                return null;
            }
            key = key.ToLower();
            var index = Items.FindIndex(x => x.Key == key);
            if (index == -1)
            {
                statusCode = EnumStorageStatusCode.KeyNotFound;
                return null;
            }

            if (Items[index].IsEncrypted)
            {
                // decrypt the value
                var decryptedValue = Utilities.Security.Decrypt(Items[index].Value);
                statusCode = EnumStorageStatusCode.Success;
                return decryptedValue;
            }
            else
            {
                statusCode = EnumStorageStatusCode.Success;
                return Items[index].Value;
            }
        }

        public string Get(string key)
        {
            EnumStorageStatusCode code;
            var result = Get(key, out code);
            return result;
        }

        public EnumStorageStatusCode Delete(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return EnumStorageStatusCode.KeyIsNull;
                
            }
            key = key.ToLower();
            var index = Items.FindIndex(x => x.Key == key);
            if (index == -1)
            {
                return EnumStorageStatusCode.KeyNotFound;
                
            }
            Items.RemoveAt(index);
            // save the file (async)
            Builder.BuildAndWriteFileAsync(Items, rootFolder).RunSynchronously();

            return EnumStorageStatusCode.Success;

        }
    }
}
