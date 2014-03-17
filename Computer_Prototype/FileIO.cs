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
        private static int MAX_SAVED_DATA = 2;
        private static string FILE_NAME = AppDomain.CurrentDomain.BaseDirectory + "Player Stats.dat";
        private StreamReader reader;
        public FileIO()
        {
            /* 
             * This is just to test whether or not the file already exists.
             * If the file doesn't exist, it is created.
             */
            try
            {
                reader = new StreamReader(FILE_NAME);
                reader.Close();
            }
            catch (FileNotFoundException)
            {
                StreamWriter writer = new StreamWriter(FILE_NAME);
                writer.Close();
            }
        }

        /*
         * Receives a string formatted as follow: "w x y z"
         * where:
         * w is an int from 0 to 100 representing the right arm's results
         * x is an int from 0 to 100 representing the left arm's results
         * y is an int from 0 to 100 representing the right leg's results
         * z is an int from 0 to 100 representing the left leg's results
        */
        public void Save(string _result)
        {
            /* 
             * Gets what's in the file and stores it in a LinkedList, line by line.
             * The older the content, the further down the LL it will be stored.
            */
            reader = new StreamReader(FILE_NAME);
            LinkedList<string> content = new LinkedList<string>();
            do
            {
                content.AddLast(reader.ReadLine());
            } while (reader.Peek() != -1);
            /* 
             * Add the _result to be saved to the linked list.
             * It's the newest addition, so it goes at the beginning.
            */ 
            content.AddFirst(_result);
            // Don't need the reader anymore.
            reader.Close();

            // Now, we put all that back in the file.
            StreamWriter writer = new StreamWriter(FILE_NAME);
            int i = 0;
            foreach (string s in content)
            {
                // Stops the loop if we already saved the maximum number of line.
                if (i == MAX_SAVED_DATA)
                {
                    break;
                }
                writer.WriteLine(s);
                // content.RemoveFirst();
                ++i;
            }
            writer.Close();
        }

        public void Save(int _rA, int _lA, int _rL, int _lL)
        {
            string toSave = Convert.ToString(_rA) + ' ' + Convert.ToString(_lA) + ' ' + Convert.ToString(_rL) + ' ' + Convert.ToString(_lL);
            Save(toSave);
        }

        public void Save(int[] _result)
        {
            string toSave = Convert.ToString(_result[0]) + ' ' + Convert.ToString(_result[1]) + ' ' + Convert.ToString(_result[2]) + ' ' + Convert.ToString(_result[3]);
            Save(toSave);
        }


        private int[] parseInArray(string _s)
        {
            string[] word = _s.Split(' ');
            int[] result = new int[word.Length];
            int i = 0;
            foreach (string w in _s.Split(' '))
            {
                result[i] = Convert.ToInt32(w);
                ++i;
            }
            return result;
        }
    }
}
