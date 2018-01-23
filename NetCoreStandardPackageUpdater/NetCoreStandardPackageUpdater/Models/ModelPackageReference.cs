using System.Xml.Serialization;

namespace NetCoreStandardPackageUpdater.Models
{
    /// <summary>
    /// Model class for Package Reference
    /// </summary>
    /// <author>
    /// Aaron Coppock
    /// </author>
    /// <date>
    /// 1/21/2018
    /// </date>
    [XmlRoot(ElementName = "PackageReference")]
    public class ModelPackageReference
    {
        [XmlAttribute(AttributeName = "Include")]
        public string Include
        {
            get;
            set;
        }

        [XmlAttribute(AttributeName = "Version")]
        public string Version
        {
            get;
            set;
        }
    }
}