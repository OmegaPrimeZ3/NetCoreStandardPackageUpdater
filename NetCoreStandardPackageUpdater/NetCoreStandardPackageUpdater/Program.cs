using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using CommandLine;
using CommandLine.Text;
using NetCoreStandardPackageUpdater.CommandOptions;
using NetCoreStandardPackageUpdater.Models;

namespace NetCoreStandardPackageUpdater
{
    class Program
    {
        private static int _exitCode;

        static int Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandOptionUpdate>(args)
                .MapResult(
                HandleUpdateCommand,
                errs => 1);
            
#if DEBUG
            Console.ReadLine();
#endif

            return _exitCode;
        }

        /// <summary>
        /// Method to handle Update Command
        /// </summary>
        /// <author>
        /// Aaron Coppock
        /// </author>
        /// <date>
        /// 1/22/2018
        /// </date>
        /// <param name="command"></param>
        private static int HandleUpdateCommand(CommandOptionUpdate command)
        {
            try
            {
                // Check if the file path is valid and that the file extension is as expected
                if (!File.Exists(command.FilePath))
                {
                    Console.WriteLine("No CSPROJ file located at the supplied path");
                    _exitCode = 1;
                    return 1;
                }

                if (Path.GetExtension(command.FilePath).ToLower() != ".csproj")
                {
                    Console.WriteLine("File at located at the supplied path is not the correct type");
                    _exitCode = 1;
                    return 1;
                }

                // Get the file data
                string fileData = File.ReadAllText(command.FilePath);

                // Convert to object
                XmlSerializer serializer = new XmlSerializer(typeof(ModelProject));
                MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(fileData));
                ModelProject project = (ModelProject)serializer.Deserialize(memStream);

                // Iterate and Update
                foreach (ModelItemGroup itemGroup in project.ItemGroup)
                {
                    foreach (ModelPackageReference packageReference in itemGroup.PackageReference)
                    {
                        string referenceName = packageReference.Include;
                        string referenceVersion = packageReference.Version;

                        if (string.IsNullOrWhiteSpace(referenceName))
                        {
                            continue;
                        }

                        // To update a package, the package must first be removed
                        string removeCommandArgs = $"remove {command.FilePath} package {referenceName}";

                        System.Diagnostics.Process removeProcess = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo removeStartInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                            FileName = "dotnet",
                            RedirectStandardOutput = true,
                            Arguments = removeCommandArgs
                        };

                        removeProcess.StartInfo = removeStartInfo;
                        removeProcess.Start();
                        removeProcess.WaitForExit(10000);

                        string removeResponseCode = removeProcess.StandardOutput.ReadLine();

                        if (removeResponseCode == null || 
                            !removeResponseCode.Contains("info : Removing PackageReference for package"))
                        {
                            Console.WriteLine($"Error removing package reference {referenceName} to project {command.FilePath}");
                            _exitCode = 1;
                            return 1;
                        }

                        Console.WriteLine($"Removed package {referenceName}-{referenceVersion} from project {command.FilePath}");

                        // There is a specific version to use or get the latest
                        var addCommandArgs = referenceVersion.Contains("[")
                            ? $"add {command.FilePath} package {referenceName} -v {referenceVersion}"
                            : $"add {command.FilePath} package {referenceName}";

                        System.Diagnostics.Process addProcess = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo addStartInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                            FileName = "dotnet",
                            RedirectStandardOutput = true,
                            Arguments = addCommandArgs
                        };

                        Console.WriteLine($"Executing Add: {addStartInfo.FileName} {addStartInfo.Arguments}");

                        addProcess.StartInfo = addStartInfo;
                        addProcess.Start();

                        List<string> addResponse = new List<string>();

                        while (!addProcess.StandardOutput.EndOfStream)
                        {
                            addResponse.Add(addProcess.StandardOutput.ReadLine());
                        }
                        
                        if (!addResponse.Any(s=>s.Contains($"info : PackageReference for package '{referenceName}'")))
                        {
                            Console.WriteLine($"Error adding package reference {referenceName} to project {command.FilePath}");
                            _exitCode = 1;
                            return 1;
                        }

                        foreach (string response in addResponse)
                        {
                            Console.WriteLine(response);
                        }

                        Console.WriteLine($"Added package {referenceName} for project {command.FilePath}");
                        addResponse.Clear();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _exitCode = 1;
                return 1;
            }
            
            return 0;
        }
    }
}
