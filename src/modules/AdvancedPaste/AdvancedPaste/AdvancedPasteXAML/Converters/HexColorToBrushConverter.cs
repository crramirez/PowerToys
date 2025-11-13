// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace AdvancedPaste.Converters
{
    public sealed partial class HexColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is not string hexColor || string.IsNullOrWhiteSpace(hexColor))
            {
                return null;
            }

            try
            {
                // Remove # if present
                var cleanHex = hexColor.TrimStart('#');

                // Expand 3-digit hex to 6-digit (#ABC -> #AABBCC)
                if (cleanHex.Length == 3)
                {
                    cleanHex = $"{cleanHex[0]}{cleanHex[0]}{cleanHex[1]}{cleanHex[1]}{cleanHex[2]}{cleanHex[2]}";
                }

                if (cleanHex.Length == 6)
                {
                    var r = System.Convert.ToByte(cleanHex.Substring(0, 2), 16);
                    var g = System.Convert.ToByte(cleanHex.Substring(2, 2), 16);
                    var b = System.Convert.ToByte(cleanHex.Substring(4, 2), 16);

                    return new SolidColorBrush(Windows.UI.Color.FromArgb(255, r, g, b));
                }
            }
            catch
            {
                // Invalid color format - return null
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotSupportedException();
    }
}
