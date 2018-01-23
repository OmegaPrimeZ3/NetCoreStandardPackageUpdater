using System.Xml.Serialization;

namespace NetCoreStandardPackageUpdater.Models
{
    /// <summary>
    /// Model class for a Folder
    /// </summary>
    /// <author>
    /// Aaron Coppock
    /// </author>
    /// <date>
    /// 1/21/2018
    /// </date>
    [XmlRoot(ElementName = "Folder")]
    public class ModelFolder
    {
        [XmlAttribute(AttributeName = "Include")]
        public string Include
        {
            get; set;
        }
    }
}