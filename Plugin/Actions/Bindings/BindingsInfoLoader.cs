using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace EliteDangerousMacroDeckPlugin.Actions.Bindings
{
    public class BindingsInfoLoader
    {

        public static readonly BindingsInfo EmptyBindings = new BindingsInfo();

        private static readonly XmlSerializer _bindingsSerializer = new XmlSerializer(typeof(BindingsInfo));

        public event EventHandler<BindingsInfo> Changed;
        public BindingsInfo Bindings { get; private set; }

        private readonly FileSystemWatcher _fileSystemWatcher;
        private readonly string _filePath;

        public BindingsInfoLoader(string filePath)
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
            BindingsInfo bindings = EmptyBindings;
            try
            {
                using var stream = Util.WaitForFile(_filePath);
                using var reader = new StreamReader(stream);
                bindings = (BindingsInfo)_bindingsSerializer.Deserialize(reader);
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