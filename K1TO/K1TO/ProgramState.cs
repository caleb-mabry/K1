using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1TO
{
    class ProgramState
    {
        public string currentlySelectedFile;

        public static List<string> supportedExtensions()
        {
            List<String> supportedExtensions = new List<string>();
            // Get all archive types
            var interfaceType = typeof(K1TO.Interfaces.ArchiveInformation);
            var archiveTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x).ToList();

            // Add extensions to state
            foreach (var archiveType in archiveTypes)
            {
                var plugin = (K1TO.Interfaces.ArchiveInformation)Activator.CreateInstance(archiveType);
                supportedExtensions.Append(plugin.Extension);
            }
            return supportedExtensions;
        }
    }
}
