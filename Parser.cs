using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLStorage;

namespace StoragePCL
{
    public class Parser
    {
        internal static async Task<List<Item>> ParseFileAsync(IFolder root)
        {
            try
            {
                var contents = await FileAccess.ReadFileAsync(root);
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(contents);
                return items;
            }
            catch (Exception ex)
            {
                // Log error

            }
            return null;

        }
    }
}
