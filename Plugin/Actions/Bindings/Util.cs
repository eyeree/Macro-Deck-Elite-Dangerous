using System.IO;
using System.Threading;

namespace EliteDangerousMacroDeckPlugin.Actions.Bindings
{
    internal class Util
    {
        public static FileStream WaitForFile(string path)
        {

            FileStream stream = null;

            var maxTries = 6;
            while (stream == null)
            {
                try
                {
                    stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
                catch (IOException)
                {
                    if (--maxTries == 0)
                    {
                        return null;
                    }
                    Thread.Sleep(100);
                }
            }

            return stream;

        }
    }
}
