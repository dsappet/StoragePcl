using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoragePCL
{
    internal class Item
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsEncrypted { get; set; }

        public Item(string key, string value, bool isEncrypted = false)
        {
            Key = key;
            Value = value;
            IsEncrypted = isEncrypted;
        }
    }
}
