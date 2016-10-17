using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoragePCL.Enums
{
    public enum EnumStorageStatusCode
    {
        Success = 1,
        Failure = 2,
        DidNotSave = 3,
        KeyExists = 4,
        KeyNotFound = 5,
        KeyIsNull = 6,
        ValueIsNull = 7,
    }
}
