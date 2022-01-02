using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace EliteDangerousMacroDeckPlugin.Variables
{
    public class VariableManagerBase
    {

        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();
        private readonly MacroDeckPlugin _plugin;
        private readonly HashSet<string> _marked = new HashSet<string>();

        protected VariableManagerBase(MacroDeckPlugin plugin)
        {
            _plugin = plugin;
        }

        protected void Mark()
        {
            Debug.WriteLine(">>>>> Mark");
            _marked.Clear();
        }

        protected void Sweep()
        {
            Debug.WriteLine(">>>>> Sweep Started");
            string[] keys = new string[_values.Keys.Count];
            _values.Keys.CopyTo(keys, 0);
            foreach (var name in keys)
            {
                if (!_marked.Contains(name))
                {
                    Clear(name);
                }
            }
            Debug.WriteLine(">>>>> Sweep Finished");
        }

        protected void Clear(string name)
        {
            // TODO: can Macro Deck support deleting variables?
            if (_values.TryGetValue(name, out object value))
            {
                var type = _variableType[value.GetType()];
                switch (type)
                {
                    case VariableType.String:
                        Set(name, "");
                        break;

                    case VariableType.Bool:
                        Set(name, false);
                        break;

                    case VariableType.Integer:
                        Set(name, 0);
                        break;

                    case VariableType.Float:
                        Set(name, 0.0f);
                        break;

                    default:
                        throw new NotSupportedException();
                }
            }
        }

        private readonly Dictionary<Type, VariableType> _variableType = new Dictionary<Type, VariableType>
        {
            { typeof(string), VariableType.String },
            { typeof(int), VariableType.Integer },
            { typeof(float), VariableType.Float },
            { typeof(bool), VariableType.Bool }
        };

        private void InternalSet(string name, object value, bool save)
        {
            Debug.Assert(_variableType.ContainsKey(value.GetType()), $"Unsupported variable type: {value.GetType().Name}");
            _marked.Add(name);
            var type = _variableType[value.GetType()];
            var found = _values.TryGetValue(name, out object current);
            if (!found || current != null && !current.Equals(value) || current == null && value != null)
            {
                _values[name] = value;
                Debug.WriteLine(">>>>> Changed variable {0} to {1}.", name, value);
                SuchByte.MacroDeck.Variables.VariableManager.SetValue(name, value, type, _plugin, save);
            }
            else
            {
                //Debug.WriteLine(">>>>> Unchanged variable {0} is {1}.", name, value);
            }
        }

        protected string SnakeCase(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }

            var builder = new StringBuilder(text.Length + Math.Min(2, text.Length / 5));
            var previousCategory = default(UnicodeCategory?);

            for (var currentIndex = 0; currentIndex < text.Length; currentIndex++)
            {
                var currentChar = text[currentIndex];
                if (currentChar == '_')
                {
                    builder.Append('_');
                    previousCategory = null;
                    continue;
                }

                var currentCategory = char.GetUnicodeCategory(currentChar);
                switch (currentCategory)
                {

                    case UnicodeCategory.UppercaseLetter:
                    case UnicodeCategory.TitlecaseLetter:
                        if (previousCategory == UnicodeCategory.SpaceSeparator ||
                            previousCategory == UnicodeCategory.LowercaseLetter ||
                            previousCategory != UnicodeCategory.DecimalDigitNumber &&
                            previousCategory != null &&
                            currentIndex > 0 &&
                            currentIndex + 1 < text.Length &&
                            char.IsLower(text[currentIndex + 1]))
                        {
                            builder.Append('_');
                        }

                        currentChar = char.ToLower(currentChar, CultureInfo.InvariantCulture);
                        break;

                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.DecimalDigitNumber:
                        if (previousCategory == UnicodeCategory.SpaceSeparator)
                        {
                            builder.Append('_');
                        }
                        break;

                    default:
                        if (previousCategory != null)
                        {
                            previousCategory = UnicodeCategory.SpaceSeparator;
                        }
                        continue;
                }

                builder.Append(currentChar);
                previousCategory = currentCategory;
            }

            return builder.ToString();
        }

        protected string TitleCase(string text)
        {
            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(text);
        }

        protected void Set(string name, string value, bool save = false)
        {

            if (value == null)
            {
                value = "";
            }

            InternalSet(name, value, save);

        }

        protected void Set(string name, int value, bool save = false)
        {
            InternalSet(name, value, save);
        }
        protected void Set(string name, long value, bool save = false)
        {
            // TODO: change Macro Deck to use long instead of int for VariableType.Integer?
            Set(name, value.ToString(), save);
        }

        protected void Set(string name, float value, bool save = false)
        {
            InternalSet(name, value, save);
        }

        protected void Set(string name, double value, bool save = false)
        {
            // TODO: change Macro Deck to use double instead of float for VariableType.Float?
            Set(name, value.ToString(), save);
        }

        protected void Set(string name, bool value, bool save = false)
        {
            InternalSet(name, value, save);
        }

        protected void Set<Flags>(string name, Flags flags, Flags flagsSet, bool save = false) where Flags : Enum
        {
            Set(name, AreFlagsSet(flags, flagsSet), save);
        }

        protected void Set<Flags>(string name, Flags flags, Flags flagsSet, Flags flagsClear, bool save = false) where Flags : Enum
        {
            Set(name, AreFlagsSet(flags, flagsSet, flagsClear), save);
        }

        protected void Set(string name, DateTime value, bool save = false)
        {
            // TODO: change Macro Deck to use long instead of int for VariableType.Integer and
            // then use ticks instead?
            Set(name, value.ToString(), save);
        }

        protected void Save(string name, string value)
        {
            Set(name, value, true);
        }


        protected void Save(string name, int value)
        {
            Set(name, value, true);
        }

        protected void Save(string name, long value)
        {
            Set(name, value, true);
        }

        protected void Save(string name, float value)
        {
            Set(name, value, true);
        }

        protected void Save(string name, double value)
        {
            Set(name, value, true);
        }

        protected void Save(string name, bool value)
        {
            Set(name, value, true);
        }

        protected void Save<Flags>(string name, Flags flags, Flags flagsSet) where Flags : Enum
        {
            Set(name, flags, flagsSet, true);
        }

        protected void Save<Flags>(string name, Flags flags, Flags flagsSet, Flags flagsClear) where Flags : Enum
        {
            Set(name, flags, flagsSet, flagsClear, true);
        }

        protected void Save(string name, DateTime value)
        {
            Set(name, value, true);
        }

        protected static bool AreFlagsSet<Flags>(Flags flags, Flags flagsSet) where Flags : Enum
        {
            ulong bits = Convert.ToUInt64(flags);
            ulong bitsSet = Convert.ToUInt64(flagsSet);
            return (bits & bitsSet) == bitsSet;
        }

        protected static bool AreFlagsSet<Flags>(Flags flags, Flags flagsSet, Flags flagsClear) where Flags : Enum
        {
            ulong bits = Convert.ToUInt64(flags);
            ulong bitsSet = Convert.ToUInt64(flagsSet);
            ulong bitsClear = Convert.ToUInt64(flagsClear);
            return (bits & bitsSet) == bitsSet && (bits & bitsClear) == 0;
        }

    }


}