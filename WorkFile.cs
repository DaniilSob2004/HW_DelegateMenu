using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;


namespace HW_DelegateMenu
{
    public class WorkFile
    {
        private string filename;

        private static bool CheckNull(string value)
        {
            return value == null;
        }

        private static bool IsValidFile(string fileName)
        {
            return File.Exists($"{fileName}");
        }


        public WorkFile(string filename) => FileName = filename;

        public string FileName
        {
            get => filename;
            set
            {
                // проверка на null
                if (CheckNull(value)) throw new Exception("Error, nullReference in variable file value!");

                // проверка существует ли файл
                if (!IsValidFile(value)) throw new Exception("No such file!");

                // проверка является ли этот файл .txt
                Match match = (new Regex(@"\.txt$")).Match(value);
                if (!match.Success) throw new Exception("This is not a .txt file!");

                filename = value;
            }
        }

        public string Read()
        {
            string value;

            using (var fs = new FileStream($"{filename}", FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                value = sr.ReadToEnd();
                sr.Close();
            }

            return value;
        }

        public void Write(string value)
        {
            // проверка на null
            if (CheckNull(value)) throw new Exception("Error, nullReference in variable file value!");

            using (var fs = new FileStream($"{filename}", FileMode.Open, FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(value);
                sw.Dispose();
            }
        }
    }
}
