using System.Collections.Generic;

namespace Accounting_of_employees_test_task
{
    public interface IFileService
    {
        List<Worker> Open(string filename);
        void Save(string filename, List<Worker> workersList);
    }
}
