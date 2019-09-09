using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IslaamAdmin.Models
{
    public class JSONReadWrite
    {
        public JSONReadWrite() { }

        public string Read(string fileName, string location)
        {
            string root = "";
            var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            root,
            location,
            fileName);
            Debug.WriteLine(Directory.GetCurrentDirectory());
            string jsonResult;

            using (StreamReader streamReader = new StreamReader(path))
            {
                jsonResult = streamReader.ReadToEnd();
            }
            return jsonResult;
        }

        public void Write(string fileName, string location, string jSONString)
        {
            string root = "";
            var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            root,
            location,
            fileName);

            using (var streamWriter = File.CreateText(path))
            {
                streamWriter.Write(jSONString);
            }
        }
    }
}
