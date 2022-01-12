namespace Accounting_of_employees_test_task
{
    public interface IDialogService
    {
        void ShowMessage(string message);   
        string FilePath { get; set; }   
        bool OpenFileDialog();  
        bool SaveFileDialog(); 
    }
}