using CreateLinqAndSp.DTO;
using Microsoft.AspNetCore.Mvc;
using PurcheseWork.Helper;

namespace CreateLinqAndSp.Interface
{
    public interface ISalseCommonInterace
    {
        // Task<object> CreatePartnerType(CreateDTOPartnerType obj);
        Task<object> CreatePartnerType(CreateDTOPartnerType obj);
        //Task<object> CreateSingleCustomerandSuplier(CreatePartnerSingleDTO cpsd);
        Task<object> CreateSingleCustomerandSuplier(CreatePartnerSingleDTO cpsd);
        Task<MessageHelper> createListItem(List<CrateItemList> itemList);

        public Task<MessageHelper> EditItem(List<EditItemsViewModel> edit);


        //get
        Task<List<CreateDTOPartnerType>> getpartnertype();
        Task<List<CrateItemList>> GetItemList();
        Task<MessageHelper> PurchesSomeItemFromSupplier(List<PurchesDetailDTO> purcheDTO);
        Task<MessageHelper> PurchesSomeItemFromSupplierV1(PurchesCommonDTO createDTO);

        Task<MessageHelper> SalseAllDetailv1(SalseCommonDTO salse);
        Task<List<PurchesReportDTO>> DailyPurchesReport(DateTime? dateTime);
        Task<List<PurchesReportDTO>> MonthlySalseReport(int dateTime, int year);

        Task<List<DailyPurchaseAndSalesReportDTO>> DailyPurchaseAndSalesReport(DateTime? date);
        Task<List<DailyPurchaseAndSalesReportDTO>> DailyPurchaseAndSalesReportsingle(DateTime? date);
        Task<List<ReportWithGivenColumnDTO>> FromDailyPurchaseAndSalesReportsingleTo10();
        Task<MessageHelper> CreatePhysialTestHeaderwithRow(PhysicalTestDTO createCmnDTO);
    }
}
