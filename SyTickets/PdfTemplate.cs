using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SyTickets
{
    [XmlRoot(ElementName = "image")]
    public class PdfImage
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "x")]
        public string X { get; set; }
        [XmlAttribute(AttributeName = "y")]
        public string Y { get; set; }
        [XmlAttribute(AttributeName = "path")]
        public string Path { get; set; }
    }

    [XmlRoot(ElementName = "textarea")]
    public class Textarea
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "x")]
        public string X { get; set; }
        [XmlAttribute(AttributeName = "y")]
        public string Y { get; set; }
        [XmlAttribute(AttributeName = "width")]
        public string Width { get; set; }
        [XmlAttribute(AttributeName = "height")]
        public string Height { get; set; }
        [XmlAttribute(AttributeName = "truetypefont")]
        public string Truetypefont { get; set; }
        [XmlAttribute(AttributeName = "fontsize")]
        public string Fontsize { get; set; }
        [XmlAttribute(AttributeName = "align")]
        public string Align { get; set; }
        [XmlAttribute(AttributeName = "valign")]
        public string Valign { get; set; }
        [XmlAttribute(AttributeName = "delimeter")]
        public string Delimeter { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "barcode")]
    public class Barcode
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "x")]
        public string X { get; set; }
        [XmlAttribute(AttributeName = "y")]
        public string Y { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
        [XmlAttribute(AttributeName = "height")]
        public string Height { get; set; }
        [XmlAttribute(AttributeName = "angle")]
        public string Angle { get; set; }
        [XmlAttribute(AttributeName = "widthRatio")]
        public string WidthRatio { get; set; }
    }

    [XmlRoot(ElementName = "page")]
    public class Page
    {
        [XmlElement(ElementName = "image")]
        public List<PdfImage> Image { get; set; }
        [XmlElement(ElementName = "textarea")]
        public List<Textarea> Textarea { get; set; }
        [XmlElement(ElementName = "barcode")]
        public Barcode Barcode { get; set; }
        [XmlAttribute(AttributeName = "size")]
        public string Size { get; set; }
        [XmlAttribute(AttributeName = "orientation")]
        public string Orientation { get; set; }
        [XmlAttribute(AttributeName = "margains")]
        public string Margains { get; set; }
        [XmlAttribute(AttributeName = "textareaborders")]
        public string Textareaborders { get; set; }
    }

    [XmlRoot(ElementName = "pdf")]
    public class Pdf
    {
        [XmlElement(ElementName = "page")]
        public Page Page { get; set; }
    }

}