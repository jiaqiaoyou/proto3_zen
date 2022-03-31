using System;
using System.IO;
using System.Reflection;
using System.Text;
using App;

namespace proto_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var assembly = Assembly.GetExecutingAssembly();
            var cfg = AppConfig.CreateAppConfig(args, assembly);

            if (string.IsNullOrEmpty(cfg.ProtoFilesRootDirectory))
            {
                Log.Error($"proto root path not valid : {cfg.ProtoFilesRootDirectory}");
                return;
            }
            if (!Directory.Exists(cfg.ProtoFilesRootDirectory))
            {
                Log.Error($"proto root path not exist : {cfg.ProtoFilesRootDirectory}");
                return;
            }

            foreach (var passInfo in cfg.PassArray)
            {
                switch (passInfo.Type)
                {
                    case "Zen":
                    {
                        
                    } break;
                }
            }
        }
    }
}