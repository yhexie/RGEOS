using System;
namespace RGeos.Desktop
{
    interface IRgFileFormat
    {
        void ReadFile();
    }
    //策略模式读取文件
    public class RgFileFormat : IRgFileFormat
    {
        public string Name;
        public string FormatString;
        public RgFileFormat()
        {
        }
        public RgFileFormat(string aName, string aFormatString)
        {
            Name = aName;
            FormatString = aFormatString;
        }
        public virtual void ReadFile()
        {
        }

    }
}
