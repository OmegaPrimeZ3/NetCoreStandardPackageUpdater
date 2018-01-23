using System.Collections.Generic;
using System.Xml.Serialization;

namespace NetCoreStandardPackageUpdater.Models
{
    /// <summary>
    /// Model for Project file
    /// </summary>
    /// <author>
    /// Aaron Coppock
    /// </author>
    /// <date>
    /// 1/21/2018
    /// </date>
    [XmlRoot(ElementName="Project")]
    public class ModelProject
    {
        [XmlElement(ElementName = "PropertyGroup")]
        public List<ModelPropertyGroup> PropertyGroup
        {
            get; set;
        }

        [XmlElement(ElementName = "ItemGroup")]
        public List<ModelItemGroup> ItemGroup
        {
            get; set;
        }

        [XmlAttribute(AttributeName = "Sdk")]
        public string Sdk
        {
            get; set;
        }
    }
}