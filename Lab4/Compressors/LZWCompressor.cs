using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Compressors
{
    public class LZWCompressor : ICompressor
    {
        private readonly string Path;

        public LZWCompressor(string path)
        {
            Path = path;
        }

        public string ShowCompress(string text)
        {
            throw new NotImplementedException();
        }

        public string Compress(byte[] array, string currentName, string newName)
        {
            string text = ConvertToString(array);
            string lzw = Convert.ToChar(currentName.Length) + currentName + ShowCompress(text);
            string path = Path + "\\" + newName + ".huff";
            using StreamWriter writer = new StreamWriter(path, false);
            writer.Write(lzw);
            writer.Close();
            var comp = new Compression { OriginalName = currentName, CompressedFileName = newName + ".huff", CompressedFilePath = path };
            comp.CompressionRatio = Math.Round(Convert.ToDouble(lzw.Length) / Convert.ToDouble(text.Length), 4);
            comp.CompressionFactor = Math.Round(Convert.ToDouble(text.Length) / Convert.ToDouble(lzw.Length), 4);
            comp.ReductionPercentage = Math.Round(100 * (Convert.ToDouble(text.Length) - Convert.ToDouble(lzw.Length)) / Convert.ToDouble(text.Length), 4);
            using StreamWriter writer1 = new StreamWriter(Path + "\\Compressions.txt", true);
            writer1.WriteLine(comp.ToFixedString());
            return path;
        }

        private char ConvertToChar(string binary)
        {
            int value = 0;
            while (binary.Length > 0)
            {
                value *= 2;
                value += int.Parse(binary.Substring(0, 1));
                binary = binary.Remove(0, 1);
            }
            return Convert.ToChar(value);
        }

        private string ConvertToBinary(char value)
        {
            int binary = Convert.ToInt32(value);
            string aux = "";
            for (int i = 0; i < 8; i++)
            {
                aux = binary % 2 + aux;
                binary /= 2;
            }
            return aux;
        }

        public string Decompress(string text)
        {
            int titleLength = Convert.ToInt32(text[0]);
            string title = text.Substring(1, titleLength);
            text = text.Remove(0, titleLength + 1);
            var letters = Convert.ToInt32(text[0]);
            string final = "";
            string path = Path + "\\" + title;
            using var file = new FileStream(path, FileMode.Create);
            file.Write(ConvertToByteArray(final), 0, final.Length);
            return path;
        }

        public List<Compression> GetCompressions()
        {
            var file = new FileStream(Path + "\\Compressions.txt", FileMode.OpenOrCreate);
            using StreamReader reader = new StreamReader(file, Encoding.ASCII);
            List<Compression> compressions = new List<Compression>();
            Compression aux = new Compression();
            while (!reader.EndOfStream)
                compressions.Add((Compression)aux.CreateFromFixedText(reader.ReadLine()));
            if (compressions.Count > 0)
                return compressions;
            else
                return null;
        }

        private string ConvertToString(byte[] array)
        {
            string text = "";
            foreach (var item in array)
                text += Convert.ToString(Convert.ToChar(item));
            return text;
        }

        private byte[] ConvertToByteArray(string text)
        {
            byte[] array = new byte[text.Length];
            for (int i = 0; i < text.Length; i++)
                array[i] = Convert.ToByte(text[i]);
            return array;
        }
    }
}
