// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AdvancedPaste.Converters;
using Microsoft.UI.Xaml.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedPaste.UnitTests.ConvertersTests;

[TestClass]
public sealed class HexColorToBrushConverterTests
{
    private HexColorToBrushConverter _converter;

    [TestInitialize]
    public void Setup()
    {
        _converter = new HexColorToBrushConverter();
    }

    [TestMethod]
    public void TestConvert_ValidSixDigitHex_ReturnsBrush()
    {
        Windows.UI.Color result = HexColorToBrushConverter.ConvertHexColorToRGB("#FFBFAB");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(SolidColorBrush));

        var color = (Windows.UI.Color)result;
        Assert.AreEqual(255, color.R);
        Assert.AreEqual(191, color.G);
        Assert.AreEqual(171, color.B);
        Assert.AreEqual(255, color.A);
    }

    [TestMethod]
    public void TestConvert_ValidThreeDigitHex_ReturnsBrush()
    {
        var result = _converter.Convert("#abc", typeof(SolidColorBrush), null, null);
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(SolidColorBrush));

        var brush = result as SolidColorBrush;

        // #abc should expand to #aabbcc
        Assert.AreEqual(170, brush.Color.R); // 0xaa
        Assert.AreEqual(187, brush.Color.G); // 0xbb
        Assert.AreEqual(204, brush.Color.B); // 0xcc
        Assert.AreEqual(255, brush.Color.A);
    }

    [TestMethod]
    public void TestConvert_NullOrEmpty_ReturnsNull()
    {
        Assert.IsNull(_converter.Convert(null, typeof(SolidColorBrush), null, null));
        Assert.IsNull(_converter.Convert(string.Empty, typeof(SolidColorBrush), null, null));
        Assert.IsNull(_converter.Convert("   ", typeof(SolidColorBrush), null, null));
    }

    [TestMethod]
    public void TestConvert_InvalidHex_ReturnsNull()
    {
        Assert.IsNull(_converter.Convert("#GGGGGG", typeof(SolidColorBrush), null, null));
        Assert.IsNull(_converter.Convert("FFBFAB", typeof(SolidColorBrush), null, null));
        Assert.IsNull(_converter.Convert("#12345", typeof(SolidColorBrush), null, null));
    }

    [TestMethod]
    [ExpectedException(typeof(System.NotSupportedException))]
    public void TestConvertBack_ThrowsNotSupportedException()
    {
        _converter.ConvertBack(null, typeof(string), null, null);
    }
}
