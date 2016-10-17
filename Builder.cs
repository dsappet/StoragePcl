using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLStorage;

namespace StoragePCL
{
    public class Builder
    {
        internal static async Task BuildAndWriteFileAsync(List<Item> items, IFolder root)
        {
            try
            {
                var json = JsonConvert.SerializeObject(items);
                await FileAccess.WriteFileAsync(json,root);
            }
            catch (Exception ex)
            {
                // log error
               
            }
        }
    }
}
