// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Windows.UI;
using ManagedCommon;

namespace AdvancedPaste.Converters
{
    public static class HexColorConverterHelper
    {
        public static Color? ConvertHexColorToRgb(string hexColor)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hexColor))
                {
                    return null;
                }

                // Remove # if present
                string cleanHex = hexColor.TrimStart('#');

                // Expand 3-digit hex to 6-digit (#ABC -> #AABBCC)
                if (cleanHex.Length == 3)
                {
                    cleanHex = $"{cleanHex[0]}{cleanHex[0]}{cleanHex[1]}{cleanHex[1]}{cleanHex[2]}{cleanHex[2]}";
                }

                if (cleanHex.Length == 6)
                {
                    var r = Convert.ToByte(cleanHex.Substring(0, 2), 16);
                    var g = Convert.ToByte(cleanHex.Substring(2, 2), 16);
                    var b = Convert.ToByte(cleanHex.Substring(4, 2), 16);

                    return Color.FromArgb(255, r, g, b);
                }
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Invalid hex color format " + hexColor, ex.Message);
            }

            return null;
        }
    }
}
