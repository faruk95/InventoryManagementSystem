using CreateLinqAndSp.DbContexts;
using CreateLinqAndSp.DTO;
using CreateLinqAndSp.Interface;
using CreateLinqAndSp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PurcheseWork.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace CreateLinqAndSp.Service
{
    public class SalseCommonInterace : ISalseCommonInterace
    {
        private readonly AppDbContext _appDbContext;

        public SalseCommonInterace(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<object> CreatePartnerType(CreateDTOPartnerType obj)

        {
            try
            {
                TblPartnerType partnerType = new TblPartnerType()
                {

                    StrPartnerTypeName = obj.StrPartnerTypeName,
                    IsActive = obj.IsActive,

                };
                await _appDbContext.AddAsync(partnerType);
                await _appDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return ("  " + ex.Message);
            }

            //Inventory tbl = new Inventory();
            //tbl.ItemId = obj.ItemID;
            //tbl.Quantity = obj.ItemQuantity;
            //await _appDbContext.AddAsync(tbl);
            //await _appDbContext.SaveChangesAsync();
            //return 0;

            //var postinventory = await task.fromresult((from a in _appdbcontext.inventories
            //                                           select new inventorydto 
            //                                           {
            //                                           }).tolist());
            //                     await _appdbcontext.inventories.addrangeasync(postinventory);
            //                     await _appdbcontext.savechanges();

            //return postinventory;

            return "success";
        }



        async Task<object> ISalseCommonInterace.CreateSingleCustomerandSuplier(CreatePartnerSingleDTO cpsd)
        {

            try
            {
                TblPartner tp = new TblPartner()
                {
                    IsActive = cpsd.IsActive,
                    IntPartnerTypeId = cpsd.IntPartnerTypeId,
                    StrPartnerName = cpsd.StrPartnerName
                };
                await _appDbContext.AddAsync(tp);
                await _appDbContext.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }
            return "success";
        }

        public async Task<MessageHelper> createListItem(List<CrateItemList> itemList)
        {
            try
            {
                List<CrateItemList> duplicateList = new List<CrateItemList>();
                List<TblItem> DataList = new List<TblItem>();


                //var DistinctData = (from itm in itemList
                //                    group new { itm } by new { itm.StrItemName } into g
                //                    where(g.Count() > 0)                                   
                //                    select (g=>g)
                //                    ).ToList();

                //if (DistinctData.Count() > 0) {
                //    foreach (var item in DistinctData)
                //    {
                //        var data = new TblItem()
                //        {
                //            StrItemName = item.StrItemName,
                //            NumStockQuantity = item.NumStockQuantity,
                //            IsActive = item.IsActive

                //        };
                //        DataList.Add(data);


                //    }
                //}

                // if (itemList.Count() != DistinctData.Count())
                //   throw new Exception("Duplicate Data Found");





                //duplicateList.AddRange(DistinctData);
                //            var group = itemList.GroupBy(g => g.StrItemName).Where(g => g.StrItemName.Count > 0).Select(grp => grp.ToList())
                //.ToList();
                foreach (var item in itemList)
                {
                    var duplicate = (_appDbContext.TblItems
                    .Where(x => x.StrItemName.ToLower() == item.StrItemName.ToLower()).Select(s => s)).FirstOrDefault();//cut FirstorDefault

                    if (duplicate == null)
                    {
                        var data = new TblItem()
                        {
                            StrItemName = item.StrItemName,
                            NumStockQuantity = item.NumStockQuantity,
                            IsActive = item.IsActive

                        };
                        DataList.Add(data);
                    }
                    else
                    {
                        var duplicateData = new CrateItemList()
                        {
                            StrItemName = duplicate.StrItemName,
                            NumStockQuantity = duplicate.NumStockQuantity,
                            IsActive = duplicate.IsActive,
                        };
                        duplicateList.Add(duplicateData);

                    }

                }

                if (duplicateList.Count == 0)
                {

                    await _appDbContext.TblItems.AddRangeAsync(DataList);
                    await _appDbContext.SaveChangesAsync();

                    return new MessageHelper
                    {
                        Message = "Created Successfully",
                        statuscode = 200,

                    };
                }
                else
                {
                    return new MessageHelper
                    {
                        Message = "Duplicate Data found",

                        statuscode = duplicateList.Count,
                        duplicate = duplicateList
                    };
                }
            }
            catch (Exception e)
            {
                throw new Exception("" + e.Message);
            }
        }

        public async Task<MessageHelper> EditItem(List<EditItemsViewModel> edit)
        {
            try
            {

                List<TblItem> newObjList = new List<TblItem>();
                foreach (var item in edit)
                {
                    var DuplicateValue = _appDbContext.TblItems
                        .Where(x => x.IntItemId != item.IntItemId && x.StrItemName.ToLower() == item.StrItemName.ToLower() && x.IsActive == true)
                        .Select(a => a).FirstOrDefault();
                    if (DuplicateValue != null)
                    {
                        throw new Exception($"Already Exits");
                    }

                    var data = _appDbContext.TblItems.Where(x => x.IntItemId == item.IntItemId)
                                               .Select(a => a).FirstOrDefault();
                    data.StrItemName = item.StrItemName;
                    data.NumStockQuantity = (int)item.NumStockQuantity;
                    newObjList.Add(data);

                }
                _appDbContext.TblItems.UpdateRange(newObjList);
                await _appDbContext.SaveChangesAsync();
                return new MessageHelper()
                {
                    Message = "Successfully Update",
                    statuscode = 200,
                };
            }
            catch (Exception e)
            {
                throw new Exception("" + e.Message);
            }
        }

        public async Task<List<CreateDTOPartnerType>> getpartnertype()
        {
            var gpt = await Task.FromResult(
                                            (from a in _appDbContext.TblPartnerTypes
                                            select new CreateDTOPartnerType()
                                            {
                                                StrPartnerTypeName = a.StrPartnerTypeName,
                                                IsActive = a.IsActive,
                                            }
                                            ).ToList()
                                            );

            return gpt;


        }
        public async Task<List<CrateItemList>> GetItemList()
        {
            var GetItemList = await Task.FromResult((from a in _appDbContext.TblItems
                                                     select new CrateItemList()
                                                     {
                                                         StrItemName = a.StrItemName,
                                                         NumStockQuantity = a.NumStockQuantity,
                                                         IsActive = a.IsActive
                                                     }).ToList()

                );
            return GetItemList;
        }

        public async Task<MessageHelper> PurchesSomeItemFromSupplier(List<PurchesDetailDTO> purcheDTO)
        {
            try
            {
                List<TblPurchaseDetail> tblPurchaseDetail = new List<TblPurchaseDetail>();
                foreach (var item in purcheDTO)
                {
                    var PurchesitemList = new TblPurchaseDetail()
                    {
                        IntItemId = item.ItemId,
                        // IntPurchaseId = item.PurchaseId,
                        NumItemQuantity = item.ItemQuantity,
                        NumUnitPrice = item.UnitPrice,
                        IsActive = true
                    };
                    tblPurchaseDetail.Add(PurchesitemList);
                }

                await _appDbContext.AddRangeAsync(tblPurchaseDetail);
                await _appDbContext.SaveChangesAsync();
                return new MessageHelper()
                {
                    Message = "Successfully Update",
                    statuscode = 200,
                };

            }
            catch (Exception)
            {

                throw;
            }

        }

        ///
        public async Task<List<CrateItemList>> createListItemV1(List<CrateItemList> itemList)
        {
            int count = 0;
            List<CrateItemList> duplicateList = new List<CrateItemList>();
            try
            {
                List<TblItem> databaseitmetble = new List<TblItem>();
                foreach (var item in itemList)
                {
                    // var duplicatevalue = new CrateItemList();
                    var duplicate = _appDbContext.TblItems
                        .Where(x => x.StrItemName.ToLower() == item.StrItemName.ToLower()).Select(s => s).FirstOrDefault();

                    if (duplicate != null)
                    {
                        count++;
                        var duplicatesingle = new CrateItemList()
                        {
                            StrItemName = duplicate.StrItemName,
                            NumStockQuantity = duplicate.NumStockQuantity,
                            IsActive = duplicate.IsActive,

                        };
                        duplicateList.Add(duplicatesingle);


                    }
                    else
                    {
                        var data = new TblItem()
                        {
                            StrItemName = item.StrItemName,
                            NumStockQuantity = item.NumStockQuantity,
                            IsActive = item.IsActive

                        };
                        databaseitmetble.Add(data);
                    }

                }
                if (count == 0)
                {
                    //  throw new Exception("Data Already Exist");
                    await _appDbContext.TblItems.AddRangeAsync(databaseitmetble);
                    await _appDbContext.SaveChangesAsync();
                }
                //else throw new Exception("Data Already Exist");

            }
            catch (Exception e)
            {
                throw new Exception("" + e.Message);
            }
            return (duplicateList);
        }

        public async Task<MessageHelper> createListItemFinal(List<CrateItemList> itemList)
        {
            try
            {
                List<TblItem> createList = new List<TblItem>();
                List<string> duplicateItems = new List<string>();
                foreach (var item in itemList)
                {
                    TblItem? tblItem = await _appDbContext.TblItems.Where(x => x.StrItemName.ToLower() == item.StrItemName.ToLower() && x.IsActive == true).FirstOrDefaultAsync();

                    if (tblItem == null)
                    {
                        createList.Add(new TblItem()
                        {
                            StrItemName = item.StrItemName,
                            NumStockQuantity = item.NumStockQuantity,
                            IsActive = true
                        });
                    }
                    else
                    {
                        duplicateItems.Add(tblItem.StrItemName);
                    }
                }

                if (duplicateItems.Count() > 0)
                {
                    throw new Exception($"Duplicate found: {string.Join(", ", duplicateItems)}");
                }
                else
                {
                    await _appDbContext.TblItems.AddRangeAsync(createList);
                    await _appDbContext.SaveChangesAsync();

                    return new MessageHelper()
                    {
                        Message = "Success"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MessageHelper> editListItemFinal(List<CrateItemList> itemList)
        {
            try
            {
                List<TblItem> editList = new List<TblItem>();
                List<string> duplicateItems = new List<string>();
                foreach (var item in itemList)
                {
                    TblItem? tblItem = await _appDbContext.TblItems.Where(x => x.IntItemId != item.ItemId && x.StrItemName.ToLower() == item.StrItemName.ToLower() && x.IsActive == true).FirstOrDefaultAsync();

                    if (tblItem == null)
                    {
                        TblItem? updateItem = await _appDbContext.TblItems.Where(x => x.IntItemId == item.ItemId && x.IsActive == true).FirstOrDefaultAsync();

                        if (updateItem != null)
                        {
                            updateItem.StrItemName = item.StrItemName;
                            updateItem.NumStockQuantity = item.NumStockQuantity;
                            editList.Add(updateItem);
                        }
                    }
                    else
                    {
                        duplicateItems.Add(tblItem.StrItemName);
                    }
                }

                if (duplicateItems.Count() > 0)
                {
                    throw new Exception($"Duplicate found: {string.Join(", ", duplicateItems)}");
                }
                else
                {
                    _appDbContext.TblItems.UpdateRange(editList);
                    await _appDbContext.SaveChangesAsync();

                    return new MessageHelper()
                    {
                        Message = "Success"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //  public async Task<MessageHelper> createListItemFinal(List<CrateItemList> itemList)

        public async Task<MessageHelper> PurchesSomeItemFromSupplierV1(PurchesCommonDTO createDTO)
        {
            try
            {
                TblPurchase Head = new TblPurchase()
                {
                    IsActive = true,
                    DtePurchaseDate = DateTime.Now,
                    IntSupplierId = createDTO.HeaderDTO.IntSupplierId
                };
                await _appDbContext.TblPurchases.AddAsync(Head);
                await _appDbContext.SaveChangesAsync();



                List<TblPurchaseDetail> tblPurchaseDetail = new List<TblPurchaseDetail>();
                foreach (var item in createDTO.RowDetailDTO)
                {
                    var PurchesitemList = new TblPurchaseDetail()
                    {
                        IntItemId = item.ItemId,
                        IntPurchaseId = Head.IntPurchaseId,
                        NumItemQuantity = item.ItemQuantity,
                        NumUnitPrice = item.UnitPrice,
                        IsActive = true
                    };
                    tblPurchaseDetail.Add(PurchesitemList);
                }

                await _appDbContext.AddRangeAsync(tblPurchaseDetail);
                await _appDbContext.SaveChangesAsync();
                return new MessageHelper()
                {
                    Message = "Successfully Update",
                    statuscode = 200,
                };

            }
            catch (Exception)
            {

                throw;
            }

        }



        public async Task<MessageHelper> SalseAllDetailv1(SalseCommonDTO salse)
        {
            try
            {

                long count = 0;
                long flag = 0;
                foreach(var itm in salse.row)
                {
                    var x = await _appDbContext.TblItems.Where(x => x.IntItemId == itm.ItemId 
                                                               && x.IsActive == true
                                                               && itm.ItemQuantity > x.NumStockQuantity)
                                                        .FirstOrDefaultAsync();
                    if(x != null)
                    {
                        count++;
                    }
                }
                if(count == salse.row.Count())
                {
                    throw new Exception("You can not sales anything from here");
                }


                TblSale SalseHead = new TblSale()
                {
                    IsActive = true,
                    DteSalesDate = DateTime.Now,
                    IntCustomerId = salse.Head.IntCustomerId

                };
                await _appDbContext.TblSales.AddAsync(SalseHead);
                await _appDbContext.SaveChangesAsync();

                List<TblSalesDetail> SalesItemList = new List<TblSalesDetail>();
                List<string> OutofStockItems = new List<string>();
                List<TblItem> UpdateQuantity = new List<TblItem>();

                foreach (var item in salse.row)
                {
                    var qty = await _appDbContext.TblItems.Where(x => x.IntItemId == item.ItemId && x.IsActive == true).Select(x => x.NumStockQuantity).FirstOrDefaultAsync();
                    
                    if (qty < item.ItemQuantity)
                    {
                        continue;
                    }
                    
                    var SalseItem = new TblSalesDetail
                    {
                        IntItemId = item.ItemId,
                        IsActive = true,
                        IntSalesId = SalseHead.IntSalesId,
                        NumItemQuantity = item.ItemQuantity,
                        NumUnitPrice = item.UnitPrice
                    };
                    SalesItemList.Add(SalseItem);

                    UpdateQuantity.Add(new TblItem
                    {
                        NumStockQuantity = qty - item.ItemQuantity
                    }
                    );
                }
                // _appDbContext.TblItems.UpdateRange(editList);
                // await _appDbContext.SaveChangesAsync();
                _appDbContext.TblItems.UpdateRange(UpdateQuantity);
                await _appDbContext.SaveChangesAsync();

                await _appDbContext.TblSalesDetails.AddRangeAsync(SalesItemList);
                await _appDbContext.SaveChangesAsync();
                

                return new MessageHelper()
                {
                    Message = "Success"
                };  

            }

            catch (Exception ex)
            {
                throw new Exception("" + ex.Message);
            }
        }
        public async Task<List<PurchesReportDTO>>DailyPurchesReport(DateTime? date)
        {
            try
            {
                List<PurchesReportDTO?> sales = await (from a in _appDbContext.TblPurchaseDetails
                                                                 join b in _appDbContext.TblItems on a.IntItemId equals b.IntItemId
                                                                 join c in _appDbContext.TblPurchases on a.IntPurchaseId equals c.IntPurchaseId
                                                                 where c.DtePurchaseDate.Value.Date == date.Value.Date

                                                                 group new { a, b, c } by new { b.StrItemName } into g
                                                                 select new PurchesReportDTO
                                                                 {
                                                                     StrItemName = g.Key.StrItemName,
                                                                     TotalQuantity = g.Sum(x => x.a.NumItemQuantity),
                                                                     TotalPrice = g.Sum(x => x.a.NumItemQuantity * x.a.NumUnitPrice)
                                                                 }).ToListAsync();               
                return sales;

            }
            catch (Exception ex)
            {
                throw new Exception(""+ex.Message);
            }

        }

        public async Task<List<PurchesReportDTO>> MonthlySalseReport(int Month, int year)
        {
            try
            {   List<PurchesReportDTO?> sales = await (from a in _appDbContext.TblSalesDetails
                                                                       join b in _appDbContext.TblItems on a.IntItemId equals b.IntItemId
                                                                       join c in _appDbContext.TblSales on a.IntSalesId equals c.IntSalesId

                                                                       where c.DteSalesDate.Value.Month == Month && c.DteSalesDate.Value.Year==year// DateTime.Now.Year                                                                        
                                                                       group new { a, b } by new { b.StrItemName,b.IntItemId } into g
                                                                      
                                                                       select new PurchesReportDTO
                                                                       {
                                                                           StrItemName = g.Key.StrItemName,
                                                                           TotalQuantity = g.Sum(x => x.a.NumItemQuantity),
                                                                           TotalPrice = g.Sum(x => x.a.NumItemQuantity * x.a.NumUnitPrice)
                                                                       }).ToListAsync();
             return sales;

            }
            catch (Exception ex)
            {

                throw new Exception("" + ex.Message);
            }
        }

        public async Task<List<DailyPurchaseAndSalesReportDTO>> DailyPurchaseAndSalesReport(DateTime? date)
        {
            try
            {
                List<DailyPurchaseAndSalesReportDTO?> dailyPurchaseAndSales = await (from salserow in _appDbContext.TblSalesDetails
                                                                                     join item in _appDbContext.TblItems on salserow.IntItemId equals item.IntItemId
                                                                                     join salsehead in _appDbContext.TblSales on salserow.IntSalesId equals salsehead.IntSalesId
                                                                                     join purchesrow in _appDbContext.TblPurchaseDetails on item.IntItemId equals purchesrow.IntItemId
                                                                                     join purcheshead in _appDbContext.TblPurchases on purchesrow.IntPurchaseId equals purcheshead.IntPurchaseId
                                                                                     where purcheshead.DtePurchaseDate.Value.Date == date.Value.Date && salsehead.DteSalesDate.Value.Date == date.Value.Date

                                                                                     group new { item, purchesrow, salserow } by new { item.StrItemName, item.IntItemId } into g
                                                                                     select new DailyPurchaseAndSalesReportDTO
                                                                                     {
                                                                                         StrItemName = g.Key.StrItemName,
                                                                                         IntItemId = g.Key.IntItemId,
                                                                                         TotalPurchesPrice = g.Sum(x => x.purchesrow.NumItemQuantity * x.purchesrow.NumUnitPrice),
                                                                                         TotalPurchesQuantity = g.Sum(x => x.purchesrow.NumItemQuantity),


                                                                                         TotalSalsePrice = g.Sum(x => x.salserow.NumItemQuantity * x.salserow.NumUnitPrice),
                                                                                         TotalSalseQuantity = g.Sum(x => x.salserow.NumItemQuantity)


                                                                                     }


                                                                                ).ToListAsync();
                return dailyPurchaseAndSales;

            }
            catch (Exception ex)
            {

                throw new Exception("" + ex.Message);
            }

        }
        public async Task<List<DailyPurchaseAndSalesReportDTO>> DailyPurchaseAndSalesReportsingle(DateTime? date)
        {
            try
            {
                List<DailySalseReportDTO1> dailySales = await (from salserow in _appDbContext.TblSalesDetails
                                                                                      join item in _appDbContext.TblItems on salserow.IntItemId equals item.IntItemId
                                                                                      join salsehead in _appDbContext.TblSales on salserow.IntSalesId equals salsehead.IntSalesId
                                                                                   
                                                                                      where salsehead.DteSalesDate.Value.Date == date.Value.Date

                                                                                      group new { item, salserow } by new { item.StrItemName, item.IntItemId } into g
                                                                                      select new DailySalseReportDTO1
                                                                                      {
                                                                                          StrItemName = g.Key.StrItemName,
                                                                                          IntItemId = g.Key.IntItemId,
                                                                                          TotalSalsePrice = g.Sum(x => x.salserow.NumItemQuantity * x.salserow.NumUnitPrice),
                                                                                          TotalSalseQuantity = g.Sum(x => x.salserow.NumItemQuantity)

                                                                          }).ToListAsync();
                //return dailySales;

                List<DailyPurchesReportDTO1> dailyPurchase=await( from itm in _appDbContext.TblItems
                                                                            join purchesrow in _appDbContext.TblPurchaseDetails on itm.IntItemId equals purchesrow.IntItemId
                                                                            join purcheshead in _appDbContext.TblPurchases on purchesrow.IntPurchaseId equals purcheshead.IntPurchaseId
                                                                            where purcheshead.DtePurchaseDate.Value.Date == date.Value.Date
                                                                            group new { itm, purchesrow } by new { itm.StrItemName, itm.IntItemId } into g
                                                                            select new DailyPurchesReportDTO1
                                                                            {
                                                                                StrItemName = g.Key.StrItemName,
                                                                                IntItemId = g.Key.IntItemId,
                                                                                TotalPurchesPrice = g.Sum(x => x.purchesrow.NumItemQuantity * x.purchesrow.NumUnitPrice),
                                                                                TotalPurchesQuantity = g.Sum(x => x.purchesrow.NumItemQuantity),
                                                                            }).ToListAsync();
                //return dailyPurchase;
                //   List<DailyPurchaseAndSalesReportDTO?> dailySalesAndPurches ei gula variable
                List<DailyPurchaseAndSalesReportDTO?> dailySalesAndPurches =  (from salse in dailySales
                                                                                    join Purches in dailyPurchase
                                                                                    on salse.IntItemId equals Purches.IntItemId
                                                                                    select new DailyPurchaseAndSalesReportDTO {
                                                                                        IntItemId = salse.IntItemId,
                                                                                        StrItemName= salse.StrItemName,
                                                                                        TotalSalsePrice=salse.TotalSalsePrice,
                                                                                        TotalSalseQuantity=salse.TotalSalseQuantity,
                                                                                        TotalPurchesQuantity=Purches.TotalPurchesQuantity,
                                                                                        TotalPurchesPrice=Purches.TotalPurchesPrice
                                                                                    }).ToList();

                return dailySalesAndPurches;

                 
            }
            catch (Exception ex)
            {

                throw new Exception("" + ex.Message);
            }

        }

        public async Task<List<ReportWithGivenColumnDTO>> FromDailyPurchaseAndSalesReportsingleTo10()
        {
            try
            {
                List<ReportWithsalseDTO> dailySales = await (from salserow in _appDbContext.TblSalesDetails
                                                             join item in _appDbContext.TblItems on salserow.IntItemId equals item.IntItemId
                                                             join salsehead in _appDbContext.TblSales on salserow.IntSalesId equals salsehead.IntSalesId


                                                             group new { salserow, salsehead } by new { salsehead.DteSalesDate.Value.Year, salsehead.DteSalesDate.Value.Month } into g
                                                             select new ReportWithsalseDTO
                                                             {
                                                                 Year = g.Key.Year,
                                                                 MonthName = g.Key.Month,
                                                                 TotalSalsePrice = g.Sum(x => x.salserow.NumItemQuantity * x.salserow.NumUnitPrice),


                                                             }).ToListAsync();


                List<ReportWithPurchesDTO> dailyPurchase = await (from itm in _appDbContext.TblItems
                                                                  join purchesrow in _appDbContext.TblPurchaseDetails on itm.IntItemId equals purchesrow.IntItemId
                                                                  join purcheshead in _appDbContext.TblPurchases on purchesrow.IntPurchaseId equals purcheshead.IntPurchaseId

                                                                  //     where purcheshead.DtePurchaseDate.Value.Date == date.Value.Date
                                                                  group new { purchesrow, purcheshead } by new { purcheshead.DtePurchaseDate.Value.Year, purcheshead.DtePurchaseDate.Value.Month } into g
                                                                  select new ReportWithPurchesDTO
                                                                  {
                                                                      Year = g.Key.Year,
                                                                      MonthName = g.Key.Month,
                                                                      TotalPurchesPrice = g.Sum(x => x.purchesrow.NumItemQuantity * x.purchesrow.NumUnitPrice),


                                                                  }).ToListAsync();
                //return dailyPurchase;
                //   List<DailyPurchaseAndSalesReportDTO?> dailySalesAndPurches ei gula variable
                List<ReportWithGivenColumnDTO?> dailySalesAndPurches = (from salse in dailySales
                                                                        join Purches in dailyPurchase on salse.Year equals Purches.Year
                                                                        where salse.MonthName==Purches.MonthName

                                                                        select new ReportWithGivenColumnDTO
                                                                        {
                                                                    MonthName = salse.MonthName,
                                                                    Year = salse.Year,
                                                                    TotalPurchesPrice = Purches.TotalPurchesPrice,
                                                                    TotalSalsePrice = salse.TotalSalsePrice,
                                                                    Status= salse.TotalSalsePrice > Purches.TotalPurchesPrice ? "Profit":"Loss",
    //                                                               
                                                                        }).ToList();

                return dailySalesAndPurches;
                 
            }
            catch (Exception ex)
            {

                throw new Exception("" + ex.Message);
            }

        }




        //public async Task<List<ReportWithGivenColumnDTO>> ReportWithGivenColumn()
        //{
        //    try
        //    {
        //        ReportWithGivenColumnDTO Report= await (from salserow in _appDbContext.TblSalesDetails
        //                                                join item in _appDbContext.TblItems on salserow.IntItemId equals item.IntItemId
        //                                                join salsehead in _appDbContext.TblSales on salserow.IntSalesId equals salsehead.IntSalesId
        //                                                join purchesrow in _appDbContext.TblPurchaseDetails on item.IntItemId equals purchesrow.IntItemId
        //                                                join purcheshead in _appDbContext.TblPurchases on purchesrow.IntPurchaseId equals purcheshead.IntPurchaseId
        //                                              //  where purcheshead.DtePurchaseDate.Value.Date == date.Value.Date && salsehead.DteSalesDate.Value.Date == date.Value.Date
        //                                                group new { purcheshead,salserow} by new { purcheshead.DtePurchaseDate.Value.Year, purcheshead.DtePurchaseDate.Value.Month } into g
        //                                                select new ReportWithGivenColumnDTO
        //                                                {
        //                                                    MonthName=g.Key.DtePurchaseDate,
        //                                                    Year=g.Key.Year,
        //                                                    TotalPurchesAmount=g.Sum()


        //                                                }
        //                                                )
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public string GetMonthName(int month)
        {

            DateTime date = new DateTime(2010, month, 1);
            return date.ToString("MMMM");

        }

    }
}
