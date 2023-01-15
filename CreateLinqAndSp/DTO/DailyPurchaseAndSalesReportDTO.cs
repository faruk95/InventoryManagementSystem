namespace CreateLinqAndSp.DTO
{
    public class DailyPurchaseAndSalesReportDTO
    {
        public int? IntItemId { get; set; }
        public string? StrItemName { get; set; }
        public decimal? TotalSalsePrice { get; set; }
        public decimal? TotalSalseQuantity { get; set; }
        public decimal? TotalPurchesPrice { get; set; }

        public decimal? TotalPurchesQuantity { get; set; }

        //  public DailyPurchaseReportDTO PurchesD { get; set; }
        // public DailySalseReportDTO SalseD { get; set; }

    }
    public class DailySalseReportDTO
    {
        public int? IntItemId { get; set; }
        public string? StrItemName { get; set; }
        public decimal? TotalSalsePrice { get; set; }
        public decimal? TotalSalseQuantity { get; set; } 
    }
    public class  DailyPurchaseReportDTO
{
        public int? IntItemId { get; set; }
        public string? StrItemName { get; set; }
        public decimal? TotalPurchesPrice { get; set; }

        public decimal? TotalPurchesQuantity { get; set; }
    }
}
