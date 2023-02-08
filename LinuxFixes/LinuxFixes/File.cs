using System;
using System.IO;

namespace LinuxFixes
{
    public static class File
    {
        public static void WriteAllTextUTF8(string filename, string content, bool bomMark)
        {

            if (System.IO.File.Exists(filename))
                System.IO.File.Delete(filename);

            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding(bomMark);

            byte[] data = encoder.GetBytes(content);

            using (FileStream fs = new FileStream(
                filename, FileMode.CreateNew,
                FileAccess.Write, FileShare.None))
            {
                fs.Write(data, 0, data.Length);
                fs.Close();
            }
        }
        public static void WriteBytesAll(string filename, byte[] content)
        {
            if (System.IO.File.Exists(filename))
                System.IO.File.Delete(filename);

            using (FileStream fs = new FileStream(
                filename, FileMode.CreateNew,
                FileAccess.Write, FileShare.None))
            {
                fs.Write(content,0,content.Length);
                fs.Close();
            }
        }
    }
}
