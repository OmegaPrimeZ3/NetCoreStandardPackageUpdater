using System.Collections.Generic;
using System.Xml.Serialization;

namespace NetCoreStandardPackageUpdater.Models
{
    /// <summary>
    /// Model class for an Item Group
    /// </summary>
    /// <author>
    /// Aaron Coppock
    /// </author>
    /// <date>
    /// 1/21/2018
    /// </date>
    [XmlRoot(ElementName = "ItemGroup")]
    public class ModelItemGroup
    {
        [XmlElement(ElementName = "PackageReference")]
        public List<ModelPackageReference> PackageReference
        {
            get;
            set;
        }

        [XmlElement(ElementName = "Folder")]
        public ModelFolder Folder
        {
            get;
            set;
        }
    }
}