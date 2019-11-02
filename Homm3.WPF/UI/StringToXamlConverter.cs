using System;
using System.Globalization;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace Homm3.WPF
{
	//From: https://stackoverflow.com/a/22026985
	internal class StringToXamlConverter : IValueConverter
	{
		private const string highlightStartTag = "<b>";
		private const string highlightEndTag = "</b>";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string input = value as string;
			if (input != null)
			{
				var textBlock = new TextBlock();
				textBlock.TextWrapping = TextWrapping.Wrap;

				while (input.IndexOf("<b>") != -1)
				{
					int startIndex = input.IndexOf(highlightStartTag);
					int endIndex = input.IndexOf(highlightEndTag);

					//up to start is normal
					textBlock.Inlines.Add(new Run(input.Substring(0, startIndex)));
					
					//between start and end is highlighted
					string innerString = input.Substring(startIndex + highlightStartTag.Length, endIndex - (startIndex + highlightStartTag.Length));
					textBlock.Inlines.Add(new Run(innerString) { FontWeight = FontWeights.Bold });

					//the rest of the string (after the end tag)
					input = input.Substring(endIndex + highlightEndTag.Length);
				}

				if (input.Length > 0)
				{
					textBlock.Inlines.Add(new Run(input));
				}
				return textBlock;
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException("This converter cannot be used in two-way binding.");
		}

	}
}
