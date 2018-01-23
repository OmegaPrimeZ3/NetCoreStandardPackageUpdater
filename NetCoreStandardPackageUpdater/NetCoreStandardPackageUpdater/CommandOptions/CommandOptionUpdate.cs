using CommandLine;

namespace NetCoreStandardPackageUpdater.CommandOptions
{
    /// <summary>
    /// Command option for handling an update request
    /// </summary>
    /// <author>
    /// Aaron Coppock
    /// </author>
    /// <date>
    /// 1/22/2018
    /// </date>
    public class CommandOptionUpdate
    {
        [Option('u', "update", Required = true, HelpText = "Path to csproj file to update")]
        public string FilePath
        {
            get;
            set;
        }
    }
}