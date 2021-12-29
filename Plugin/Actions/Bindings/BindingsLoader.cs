using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

namespace EliteDangerousMacroDeckPlugin.Actions.Bindings
{
    public class BindingsLoader
    {

        public static readonly Bindings EmptyBindings = new Bindings();

        private static readonly XmlSerializer _bindingsSerializer = new XmlSerializer(typeof(Bindings));

        public event EventHandler<Bindings> Changed;
        public Bindings Bindings { get; private set; }

        private readonly FileSystemWatcher _fileSystemWatcher;
        private readonly string _filePath;

        public BindingsLoader(string filePath)
        {
            
            _filePath = filePath;
            Bindings = EmptyBindings;

            Load();

            _fileSystemWatcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(filePath),
                Filter = Path.GetFileName(filePath),
                NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite
            };
            _fileSystemWatcher.Changed += (s, e) => Load();
            _fileSystemWatcher.EnableRaisingEvents = true;

        }

        public virtual void Stop()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
            Changed = null;
        }

        private void Load()
        {
            Bindings bindings = EmptyBindings;
            try
            {
                using var stream = Util.WaitForFile(_filePath);
                using var reader = new StreamReader(stream);
                bindings = (Bindings)_bindingsSerializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Could not load Elite Dangerous bindings from file \"{_filePath}\". {ex.Message}");
            }
            Bindings = bindings;
            Changed?.Invoke(this, bindings);
        }

    }
}