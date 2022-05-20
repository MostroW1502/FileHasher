using System.Xml.Serialization;

namespace FileHasher.XMLExport
{
    [XmlType(TypeName ="File")]
    public class FileHasherExportedFile
    {
        [XmlElement(DataType = "string")]
        public string FileName { get; set; }
        [XmlElement(DataType = "string")]
        public string Path { get; set; }
        [XmlIgnore]
        public uint CRC32 { get; set; }
        [XmlElement(ElementName = "CRC32")]
        public string CRC32HexValue { get { return CRC32.ToString("X8"); } }
        [XmlElement(DataType = "hexBinary")]
        public byte[] MD5 { get; set; }
        [XmlElement(DataType = "hexBinary")]
        public byte[] SHA1 { get; set; }
        [XmlElement(DataType = "hexBinary")]
        public byte[] SHA256 { get; set; }
        [XmlElement(DataType = "hexBinary")]
        public byte[] SHA384 { get; set; }
        [XmlElement(DataType = "hexBinary")]
        public byte[] SHA512 { get; set; }
    }
}