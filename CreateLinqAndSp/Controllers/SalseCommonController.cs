using CreateLinqAndSp.DTO;
using CreateLinqAndSp.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurcheseWork.Helper;

namespace CreateLinqAndSp.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SalseCommonController : ControllerBase
    {
        private readonly ISalseCommonInterace _salseCommonInterace;

        public SalseCommonController(ISalseCommonInterace salseCommonInterace)
        {
            _salseCommonInterace = salseCommonInterace;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePartnerType(CreateDTOPartnerType obj)
        {
            var cpt= await _salseCommonInterace.CreatePartnerType(obj);
            return Ok(cpt);


        }
        [HttpPost]
        [Route("/SingleCustomer")]
        public async Task<IActionResult>CreateSingleCustomerAndSupplier(CreatePartnerSingleDTO CPSD)
        {
            var singleCustomer = await _salseCommonInterace.CreateSingleCustomerandSuplier(CPSD);
            return Ok(singleCustomer);
        }
        [HttpPost]
        public async Task<IActionResult> createListItem(List<CrateItemList> itemList)
        {
            var item = await _salseCommonInterace.createListItem(itemList);
            return Ok(item);

        }
        [HttpPut]
        public async Task<MessageHelper> EditItem(List<EditItemsViewModel> edit) {

            return await _salseCommonInterace.EditItem(edit);



        }
        [HttpGet]
        public async Task<List<CreateDTOPartnerType>> getpartnertype()
        {
            return await _salseCommonInterace.getpartnertype();
        }
        [HttpGet]
        public async Task<List<CrateItemList>> GetItemList()
        {
            return await _salseCommonInterace.GetItemList();
        }
        [HttpPost]
        public async Task<MessageHelper> PurchesSomeItemFromSupplier(List<PurchesDetailDTO> purcheDTO)
        {
            return await _salseCommonInterace.PurchesSomeItemFromSupplier(purcheDTO);     
               
                  

        }
        [HttpPost]
        public async Task<MessageHelper> PurchesSomeItemFromSupplierV1(PurchesCommonDTO createDTO)
        {
            return await _salseCommonInterace.PurchesSomeItemFromSupplierV1(createDTO);
        }
        [HttpPost]
        public async Task<MessageHelper> SalseAllDetailv1(SalseCommonDTO Salse)
        {

            return await _salseCommonInterace.SalseAllDetailv1(Salse);
        }

        [HttpGet()]
        public async Task<List<PurchesReportDTO>> DailyPurchesReport(DateTime? dateTime)
        {
            return await _salseCommonInterace.DailyPurchesReport(dateTime);
        }
        [HttpGet()]
        public async Task<List<PurchesReportDTO>> MonthlySalseReport(int month_digit, int year)
        {
            return await _salseCommonInterace.MonthlySalseReport(month_digit, year);
        }
        [HttpGet()]
        public async Task<List<DailyPurchaseAndSalesReportDTO>> DailyPurchaseAndSalesReport(DateTime? date)
        {
            return await _salseCommonInterace.DailyPurchaseAndSalesReport(date);
        }

        [HttpGet()]
        public async Task<List<DailyPurchaseAndSalesReportDTO>> DailyPurchaseAndSalesReportsingle(DateTime? date)
        {
            return await _salseCommonInterace.DailyPurchaseAndSalesReportsingle(date);
        }
        [HttpGet]
        public async Task<List<ReportWithGivenColumnDTO>> FromDailyPurchaseAndSalesReportsingleTo10() { return await _salseCommonInterace.FromDailyPurchaseAndSalesReportsingleTo10(); }

        //DailySalseReport(int dateTime)
    }
}
