using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace EliteDangerousMacroDeckPlugin.Actions
{
    internal class VariableCache
    {

        private static Dictionary<string, Variable> _variables = new Dictionary<string, Variable>();

        private static T get<T>(string name, string expectedType, T defaultValue, Func<string, T> parse)
        {

            _variables.TryGetValue(name, out var variable);
            if(variable == null)
            {
                variable = SuchByte.MacroDeck.Variables.VariableManager.Variables.Find(variable => variable.Name == name);
                if(variable == null)
                {
                    return defaultValue;
                }
            }

            if(!variable.Type.Equals(expectedType))
            {
                return defaultValue;
            }

            return parse(variable.Value);

        }

        public static bool getBool(string name, bool defaultValue = false)
        {
            return get(name, "bool", defaultValue, Boolean.Parse);
        }

        public static string getString(string name, string defaultValue = "")
        {
            return get(name, "string", defaultValue, value => value);
        }

    }
}
