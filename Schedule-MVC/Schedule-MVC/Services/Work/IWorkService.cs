namespace Schedule_MVC.Services.Work
{
    public interface IWorkService
    {
        bool IsAcceptable(DateTime date, int classId);
    }
}
