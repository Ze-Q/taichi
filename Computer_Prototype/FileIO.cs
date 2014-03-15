using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Prototype
{
    class FileIO
    {
        private static string FILE_NAME = AppDomain.CurrentDomain.BaseDirectory + "Player Stats.dat";
        public FileIO()
        {
            try
            {
                StreamReader reader = new StreamReader(FILE_NAME);
            }
            catch (FileNotFoundException)
            {
                StreamWriter writer = new StreamWriter(FILE_NAME);
                writer.WriteLine("Is this working?");
                writer.Close();
            }
        }
    }
}
