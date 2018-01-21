using System.Xml.Serialization;

namespace NetCoreStandardPackageUpdater.Models
{
    /// <summary>
    /// Model class for Property Group
    /// </summary>
    /// <author>
    /// Aaron Coppock
    /// </author>
    /// <date>
    /// 1/21/2018
    /// </date>
    [XmlRoot(ElementName = "PropertyGroup")]
    public class ModelPropertyGroup
    {
        [XmlElement(ElementName = "AppendTargetFrameworkToOutputPath")]
        public string AppendTargetFrameworkToOutputPath
        {
            get;
            set;
        }

        [XmlElement(ElementName = "TargetFramework")]
        public string TargetFramework
        {
            get;
            set;
        }

        [XmlElement(ElementName = "AssetTargetFallback")]
        public string AssetTargetFallback
        {
            get;
            set;
        }

        [XmlElement(ElementName = "GeneratePackageOnBuild")]
        public string GeneratePackageOnBuild
        {
            get;
            set;
        }

        [XmlElement(ElementName = "PackageId")]
        public string PackageId
        {
            get;
            set;
        }

        [XmlElement(ElementName = "Authors")]
        public string Authors
        {
            get;
            set;
        }

        [XmlElement(ElementName = "Company")]
        public string Company
        {
            get;
            set;
        }

        [XmlElement(ElementName = "PackageProjectUrl")]
        public string PackageProjectUrl
        {
            get;
            set;
        }

        [XmlElement(ElementName = "Description")]
        public string Description
        {
            get;
            set;
        }

        [XmlElement(ElementName = "Copyright")]
        public string Copyright
        {
            get;
            set;
        }

        [XmlElement(ElementName = "AssemblyVersion")]
        public string AssemblyVersion
        {
            get;
            set;
        }

        [XmlElement(ElementName = "FileVersion")]
        public string FileVersion
        {
            get;
            set;
        }

        [XmlElement(ElementName = "Version")]
        public string Version
        {
            get;
            set;
        }
    }
}