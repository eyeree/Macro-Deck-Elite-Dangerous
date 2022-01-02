using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;

namespace EliteDangerousMacroDeckPlugin.Actions
{
    internal class VariableCache
    {

        private static Dictionary<string, Variable> _variables = new Dictionary<string, Variable>();

        private static T Get<T>(string name, string expectedType, T defaultValue, Func<string, T> parse)
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

        public static bool GetBool(string name, bool defaultValue = false)
        {
            return Get(name, "bool", defaultValue, Boolean.Parse);
        }

        public static string GetString(string name, string defaultValue = "")
        {
            return Get(name, "string", defaultValue, value => value);
        }

        public static float GetFloat(string name, float defaultValue = 0.0f)
        {
            return Get(name, "float", defaultValue, float.Parse);
        }

        public static int GetInt(string name, int defaultValue = 0)
        {
            return Get(name, "int", defaultValue, int.Parse);
        }

    }
}
