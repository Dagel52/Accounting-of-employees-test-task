using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Accounting_of_employees_test_task
{
    public class JsonFileService : IFileService
    {
        public List<Worker> Open(string filename)
        {
            List<Worker> workers = new List<Worker>();
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<Worker>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                workers = jsonFormatter.ReadObject(fs) as List<Worker>;
            }

            return workers;
        }

        public void Save(string filename, List<Worker> workersList)
        {
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<Worker>));
            using FileStream fs = new FileStream(filename, FileMode.Create);
            jsonFormatter.WriteObject(fs, workersList);
        }
    }
}
