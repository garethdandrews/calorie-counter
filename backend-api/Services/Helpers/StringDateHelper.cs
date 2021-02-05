using System;

namespace backend_api.Services.Helpers
{
    public static class StringDateHelper
    {
        public static DateTime ConverStringDateToDate(string stringDate)
        {
            DateTime date;
            if (DateTime.TryParse(stringDate, out date))
                string.Format("{0:d/MM/yyyy}", date);
            else
                throw new ArgumentException("Invalid date. Must be in the format dd/mm/yyyy or dd-mm-yyyy");
            return date;
        }
    }
}