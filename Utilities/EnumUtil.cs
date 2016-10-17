using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoragePCL.Enums;

namespace Storage.Utilities
{
    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }

    public static class EnumExtensionMethods
    {
        public static string ToDescription(this EnumStorageStatusCode en)
        {
            switch (en)
            {
                case EnumStorageStatusCode.Success:
                    return "Success";
                case EnumStorageStatusCode.Failure:
                    return "Failure";
                case EnumStorageStatusCode.DidNotSave:
                    return "Did not save";
                case EnumStorageStatusCode.KeyExists:
                    return "Key already exists";
                case EnumStorageStatusCode.KeyNotFound:
                    return "Ley was not found";
                case EnumStorageStatusCode.KeyIsNull:
                    return "Passed key is null, not allowed";
                case EnumStorageStatusCode.ValueIsNull:
                    return "Passed value is null, not allowed";
                default:
                    return string.Empty;
                    throw new ArgumentOutOfRangeException(nameof(en), en, null);
            }
        }
    }
}
