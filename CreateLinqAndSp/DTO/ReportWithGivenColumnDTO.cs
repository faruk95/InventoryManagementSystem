namespace CreateLinqAndSp.DTO
{
    public class ReportWithGivenColumnDTO
    {
        public int Year { get; set; }
        public int MonthName { get; set; }
        public Decimal? TotalSalsePrice { get; set; }
        public Decimal? TotalPurchesPrice { get; set; }
        public string Status { get; set; }

    }
    public class ReportWithsalseDTO
    {
        public int Year { get; set; }
        public int MonthName { get; set; }
        public Decimal? TotalSalsePrice { get; set; }
     


    }
    public class ReportWithPurchesDTO
    {
        public int Year { get; set; }
        public int MonthName { get; set; }
       
        public Decimal? TotalPurchesPrice { get; set; }
  

    }

}
