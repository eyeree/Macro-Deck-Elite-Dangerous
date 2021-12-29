using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace EliteDangerousMacroDeckPlugin.Actions.Bindings
{
    public enum BindingType : int
    {
        General = 0,
        Ship = 1,
        Srv = 2,
        OnFoot = 3
    }

    internal class BindingsManager
    {

        private readonly string _defaultBindingsPath;
        private readonly FileSystemWatcher _startPresetWatcher;
        private readonly string _startPresetPath;

        private readonly Dictionary<string, BindingsLoader> _bindingsLoaders = new Dictionary<string, BindingsLoader>();

        public Bindings General { get; private set; }
        public Bindings Ship { get; private set; }
        public Bindings Srv { get; private set; }
        public Bindings Foot { get; private set; }

        public BindingsManager()
        {

            General = Ship = Srv = Foot = BindingsLoader.EmptyBindings;

            try
            {

                _defaultBindingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Frontier Developments\Elite Dangerous\Options\Bindings");
                if (!Directory.Exists(_defaultBindingsPath))
                {
                    throw new InvalidOperationException($"Bindings directory \"{_defaultBindingsPath}\" does not exist.");
                }

                _startPresetPath = Path.Combine(_defaultBindingsPath, "StartPreset.4.start");
                if (!File.Exists(_startPresetPath))
                {
                    _startPresetPath = Path.Combine(_defaultBindingsPath, "StartPreset.start");
                }
                if (!File.Exists(_startPresetPath))
                {
                    throw new InvalidOperationException($"Could not find a file named \"StartPreset.4.start\" or \"StartPreset.start\" in bindings directory \"{_defaultBindingsPath}\".");
                }

                _startPresetWatcher = new FileSystemWatcher();
                _startPresetWatcher.Path = Path.GetDirectoryName(_startPresetPath);
                _startPresetWatcher.Filter = Path.GetFileName(_startPresetPath);
                _startPresetWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;
                _startPresetWatcher.Changed += (s, e) =>
                    {
                        try
                        {
                            ClearBindings();
                            LoadBindings();
                        }
                        catch (Exception ex)
                        {
                            Trace.TraceError($"Elite Dangerous bindings could not be loaded. {ex}");
                        }
                    };

                LoadBindings();

                _startPresetWatcher.EnableRaisingEvents = true;

            }
            catch (Exception ex)
            {
                Trace.TraceError($"Elite Dangerous bindings could not be loaded. {ex}");
            }


        }

        private void LoadBindings()
        {

            Trace.TraceInformation($"Loading Elite Dangerous bindings specified by \"{_startPresetPath}\".");

            var bindingsNames = GetBindingsNames();

            if (bindingsNames.Length == 4)
            {
                LoadPostOdysseyBindings(bindingsNames);
            }
            else if (bindingsNames.Length == 1)
            {
                LoadPreOdysseyBindings(bindingsNames[0]);
            }
            else
            {
                throw new InvalidOperationException($"The \"{_startPresetPath}\" file content was not recognized.");
            }

        }

        private void LoadPreOdysseyBindings(string bindingsName)
        {
            LoadBindings(bindingsName, bindings =>
                {
                    General = bindings;
                    Ship = bindings;
                    Srv = bindings;
                    Foot = bindings;
                }
            );
        }

        private void LoadPostOdysseyBindings(string[] bindingsNames)
        {
            LoadBindings(bindingsNames[0], bindings => General = bindings);
            LoadBindings(bindingsNames[1], bindings => Ship = bindings);
            LoadBindings(bindingsNames[2], bindings => Srv = bindings);
            LoadBindings(bindingsNames[3], bindings => Foot = bindings);
        }

        private void LoadBindings(string bindingsName, Action<Bindings> onChanged)
        {

            _bindingsLoaders.TryGetValue(bindingsName, out var bindingsLoader);
            if(bindingsLoader == null)
            {               
                var filePath = GetBindingsFilePath(bindingsName);
                bindingsLoader = new BindingsLoader(filePath);
                _bindingsLoaders.Add(bindingsName, bindingsLoader);
            }

            bindingsLoader.Changed += (source, bindings) => onChanged(bindings);

            onChanged(bindingsLoader.Bindings);

        }

        private void ClearBindings()
        {
            foreach (var loader in _bindingsLoaders.Values)
            {
                loader.Stop();
            }
            _bindingsLoaders.Clear();
        }

        private string GetBindingsFilePath(string bindingName)
        {
            var filePath = TryGetBindingFilePath(_defaultBindingsPath, bindingName);
            if (filePath == null)
            {
                filePath = TryGetBindingFilePath(SteamPath.FindSteamEliteDirectory(), bindingName);
                if (filePath == null)
                {
                    filePath = TryGetBindingFilePath(EpicPath.FindEpicEliteDirectory(), bindingName);
                    if (filePath == null)
                    {
                        throw new InvalidOperationException($"Could not find a file for bindings \"{bindingName}\".");
                    }
                }
            }
            Trace.TraceInformation($"Using file \"{filePath}\" for bindings \"{bindingName}\".");
            return filePath;
        }

        private string TryGetBindingFilePath(string directoryPath, string bindingName)
        {
            var filePath = Path.Combine(directoryPath, bindingName + ".4.0.binds");
            if (!File.Exists(filePath))
            {
                Trace.TraceInformation($"Binding file \"{filePath}\" does not exist");
                filePath = filePath.Replace(".4.0.binds", ".3.0.binds");
                if (!File.Exists(filePath))
                {
                    Trace.TraceInformation($"Binding file \"{filePath}\" does not exist");
                    filePath = filePath.Replace(".3.0.binds", ".binds");
                    if (!File.Exists(filePath))
                    {
                        Trace.TraceInformation($"Binding file \"{filePath}\" does not exist");
                        return null;
                    }
                }
            }
            return filePath;
        }

        private string[] GetBindingsNames()
        {
            try
            {
                using (var stream = Util.WaitForFile(_startPresetPath))
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    var content = reader.ReadToEnd();
                    if (string.IsNullOrEmpty(content))
                    {
                        throw new InvalidOperationException($"The file is empty.");
                    }
                    return content.Split('\n');
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Could not get binding names from file \"{_startPresetPath}\".", e);
            }
        }

    }

}
