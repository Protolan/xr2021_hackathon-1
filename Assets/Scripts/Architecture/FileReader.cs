using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Architecture
{
    public class FileReader
    {
        private readonly string[] _lines;

        public FileReader(string filePath)
        {
            if (File.Exists(filePath))
            {
                _lines = File.ReadAllLines(filePath);
                foreach (var line in _lines) 
                    Debug.Log(line);
            }
            else
                Debug.LogError("File not exist");
        }

        public Dictionary<string, string> CreateTextDictionary()
        {
            if (_lines == null) return null;
            string[] separatingStrings = { "<>", "<", ">" };
            var dictionary = _lines.
                    Select(line => line.Split(separatingStrings,  System.StringSplitOptions.RemoveEmptyEntries)).
                    ToDictionary(words => words[0], words => words[1]);
            return dictionary;
        }
        public Dictionary<string, string> CreateButtonDictionary()
        {
            if (_lines == null) return null;
            string[] separatingStrings = { "<>", "<", ">" };
            var dictionary = _lines.
                    Select(line => line.Split(separatingStrings,  System.StringSplitOptions.RemoveEmptyEntries)).
                    ToDictionary(words => words[0], words => words[1]);
            return dictionary;
        }
        


    }
}