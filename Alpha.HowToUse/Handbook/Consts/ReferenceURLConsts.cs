using System;

namespace Alpha.HowToUse.Handbook.Consts
{
    public static class ReferenceURLConsts
    {
        public const string CustomerSettingInformation = "[CustomerSettingInformation]";

        public static bool IsReference(string refURL)
        {
            return string.Equals(refURL, CustomerSettingInformation, StringComparison.OrdinalIgnoreCase);
        }
    }
}