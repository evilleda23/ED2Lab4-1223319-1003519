using System;
using System.Collections.Generic;
using System.Text;

namespace Compressors
{
    public class Compression : IFixedSizeText
    {
        public string OriginalName { get; set; }
        public string CompressedFileName { get; set; }
        public string CompressedFilePath { get; set; }
        public double CompressionRatio { get; set; }
        public double CompressionFactor { get; set; }
        public double ReductionPercentage { get; set; }

        public int TextLength => ToFixedString().Length;

        public IFixedSizeText CreateFromFixedText(string text)
        {
            if (text.Trim() != "")
            {
                Compression item = new Compression();
                item.OriginalName = text.Substring(0, 50).Trim();
                text = text.Remove(0, 51);
                if (item.OriginalName == "")
                    item.OriginalName = null;
                item.CompressedFileName = text.Substring(0, 50).Trim();
                text = text.Remove(0, 51);
                if (item.CompressedFileName == "")
                    item.CompressedFileName = null;
                item.CompressedFilePath = text.Substring(0, 100).Trim();
                text = text.Remove(0, 101);
                if (item.CompressedFilePath == "")
                    item.CompressedFilePath = null;
                item.CompressionRatio = double.Parse(text.Substring(0, 9));
                text = text.Remove(0, 10);
                item.CompressionFactor = double.Parse(text.Substring(0, 9));
                text = text.Remove(0, 10);
                item.ReductionPercentage = double.Parse(text);
                return item;
            }
            else
                return null;
        }

        public string ToFixedString()
        {
            string text = "";
            if (OriginalName != null)
                text += string.Format("{0, -50}", OriginalName) + "|";
            else
                text += new string(' ', 50) + "|";
            if (CompressedFileName != null)
                text += string.Format("{0, -50}", CompressedFileName) + "|";
            else
                text += new string(' ', 50) + "|";
            if (CompressedFilePath != null)
                text += string.Format("{0, -100}", CompressedFilePath) + "|";
            else
                text += new string(' ', 11) + "|";
            text += CompressionRatio.ToString("0000.0000") + "|";
            text += CompressionFactor.ToString("0000.0000") + "|";
            text += ReductionPercentage.ToString("00.0000");
            return text;
        }
    }
}
