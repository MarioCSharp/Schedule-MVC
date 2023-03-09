using Schedule_MVC.Data;

namespace Schedule_MVC.Services.Work
{
    public class WorkService : IWorkService
    {
        private ApplicationDbContext _context;
        public WorkService(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public bool IsAcceptable(DateTime date, int classId)
        {
            var count = 0;

            foreach (var work in _context.Works.Where(x => x.ClassId == classId))
            {
                if (DatesAreInTheSameWeek(date, work.Date))
                {
                    count++;
                }

                if (count >= 2)
                {
                    return false;
                }
            }

            return true;
        }

        private bool DatesAreInTheSameWeek(DateTime date1, DateTime date2)
        {
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            var d1 = date1.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date1));
            var d2 = date2.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date2));

            return d1 == d2;
        }
    }
}
