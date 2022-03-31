using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using YamlDotNet.Serialization;

namespace App
{
    public class ExportSettingInfo
    {
        public List<string> ParsedPathArray { get; private set; } = new List<string>();
        public List<string> ImportPathArray { get; private set; } = new List<string>();
    }
    
    public class ExportPassInfo
    {
        public string Type { get; set; }
        public string OutputPathCC { get; set; }
        public ExportSettingInfo SelfExportSetting { get; set; }
    }
    
    public class AppConfig
    {
        // private members 
        private static AppConfig Instance;
        private const string AppConfigFilePath = "AppConfig.yaml";

        // public members
        public static Assembly AppAssembly { get; private set; }
        public static bool ForceUpdate { get; private set; }

        // info deserialized from yaml directly
        public string ToolsDirectory { get; set; }
        public string ProtoFilesRootDirectory { get; set; }
        public List<ExportPassInfo> PassArray { get; private set; } = new List<ExportPassInfo>();

        // public methods
        public static AppConfig CreateAppConfig(string[] args, Assembly assembly)
        {
            if (Instance != null)
            {
                return Instance;
            }

            AppAssembly = assembly;

            var cfgFilePath = AppConfigFilePath;
            var showHelp = false;
            
            var argSet = new NDesk.Options.OptionSet()
            {
                {
                    "cfg=", 
                    $"ths {{*.yaml}} for app config. \n default is: {AppConfigFilePath}",
                    v => cfgFilePath = v
                },
                {
                    "force-update", 
                    "force parse all .proto files",
                    v => ForceUpdate = v!=null
                },
                {
                    "h|help",
                    "show this message and exit",
                    v => showHelp = v != null
                }
            };
            argSet.Parse(args);

            if (showHelp)
            {
                argSet.WriteOptionDescriptions(Console.Out);
                Environment.Exit(0);
            }

            if (!File.Exists(cfgFilePath))
            {
                Log.Error($"can not find configure file in : {cfgFilePath}");
                Environment.Exit(0);
            }
            
            Log.Info($"arguments: force-update={ForceUpdate}");
            Log.Info($"loading configure file: {Path.GetFullPath(cfgFilePath)} ...");
            
            // deserialize *.yaml to AppConfig
            var fileContent = File.ReadAllText(cfgFilePath);
            var deserializer = new DeserializerBuilder().Build();
            var config = deserializer.Deserialize<AppConfig>(fileContent);
                
            Instance = config;
            return config;
        }
    }
}