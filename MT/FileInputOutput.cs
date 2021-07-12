using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;

namespace MT
{
    class FileInputOutput
    {
        private readonly string PATH;

        public FileInputOutput(string path)
        {
            PATH = path;
        }
        public BindingList<Task> LoadData()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<Task>();
            }
            using (var reader = File.OpenText(PATH))
            {
                try
                {
                    var fileText = reader.ReadToEnd();
                    BindingList<Task> temp = JsonConvert.DeserializeObject<BindingList<Task>>(fileText);
                    return JsonConvert.DeserializeObject<BindingList<Task>>(fileText);
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public void SaveDate(object taskData)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(taskData);
                writer.Write(output);
            }
        }
    }
}
