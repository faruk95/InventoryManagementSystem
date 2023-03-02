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
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography;

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
                foreach (var itm in salse.row)
                {
                    var x = await _appDbContext.TblItems.Where(x => x.IntItemId == itm.ItemId
                                                               && x.IsActive == true
                                                               && itm.ItemQuantity > x.NumStockQuantity)
                                                        .FirstOrDefaultAsync();
                    if (x != null)
                    {
                        count++;
                    }
                }
                if (count == salse.row.Count())
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
        public async Task<List<PurchesReportDTO>> DailyPurchesReport(DateTime? date)
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
                throw new Exception("" + ex.Message);
            }

        }

        public async Task<List<PurchesReportDTO>> MonthlySalseReport(int Month, int year)
        {
            try
            {
                List<PurchesReportDTO?> sales = await (from a in _appDbContext.TblSalesDetails
                                                       join b in _appDbContext.TblItems on a.IntItemId equals b.IntItemId
                                                       join c in _appDbContext.TblSales on a.IntSalesId equals c.IntSalesId

                                                       where c.DteSalesDate.Value.Month == Month && c.DteSalesDate.Value.Year == year// DateTime.Now.Year                                                                        
                                                       group new { a, b } by new { b.StrItemName, b.IntItemId } into g

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

                List<DailyPurchesReportDTO1> dailyPurchase = await (from itm in _appDbContext.TblItems
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
                List<DailyPurchaseAndSalesReportDTO?> dailySalesAndPurches = (from salse in dailySales
                                                                              join Purches in dailyPurchase
                                                                              on salse.IntItemId equals Purches.IntItemId
                                                                              select new DailyPurchaseAndSalesReportDTO
                                                                              {
                                                                                  IntItemId = salse.IntItemId,
                                                                                  StrItemName = salse.StrItemName,
                                                                                  TotalSalsePrice = salse.TotalSalsePrice,
                                                                                  TotalSalseQuantity = salse.TotalSalseQuantity,
                                                                                  TotalPurchesQuantity = Purches.TotalPurchesQuantity,
                                                                                  TotalPurchesPrice = Purches.TotalPurchesPrice
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
                                                                        where salse.MonthName == Purches.MonthName

                                                                        select new ReportWithGivenColumnDTO
                                                                        {
                                                                            MonthName = salse.MonthName,
                                                                            Year = salse.Year,
                                                                            TotalPurchesPrice = Purches.TotalPurchesPrice,
                                                                            TotalSalsePrice = salse.TotalSalsePrice,
                                                                            Status = salse.TotalSalsePrice > Purches.TotalPurchesPrice ? "Profit" : "Loss",
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
        #region -----------------------Physical test----------
        public async Task<MessageHelper> CreatePhysialTestHeaderwithRow(PhysicalTestDTO createCmnDTO)
        {
            try
            {
                TblVrmDailyPhysicalTestHeader header = new TblVrmDailyPhysicalTestHeader()
                {
                    IntBusinessUnitId = createCmnDTO.IntBusinessUnitId,
                    DteDate = createCmnDTO.DteDate,
                    IntShiftId = createCmnDTO.IntShiftId,
                    StrShiftName = createCmnDTO.StrShiftName,
                    //  TmTime=createCmnDTO.TmTime,
                    IntVrmid = createCmnDTO.IntVrmid,
                    StrVrmname = createCmnDTO.StrVrmname,
                    IntItemTypeId = createCmnDTO.IntItemTypeId,
                    StrItemTypeName = createCmnDTO.StrItemTypeName,
                    NumInitialTime = createCmnDTO.NumInitialTime,
                    NumFinalTime = createCmnDTO.NumFinalTime,
                    StrRemark = createCmnDTO.StrRemark,
                    IntCreatedBy = createCmnDTO.IntCreatedBy,
                    IntUpdatedBy = createCmnDTO.IntUpdatedBy,
                    DteCreatedAt = createCmnDTO.DteCreatedAt,
                    DteUpdateAt = DateTime.Now,
                    IsActive = true
                };
                await _appDbContext.TblVrmDailyPhysicalTestHeaders.AddAsync(header);
                await _appDbContext.SaveChangesAsync();

                List<TblVrmDailyPhysicalTestRow> createList = new List<TblVrmDailyPhysicalTestRow>();

                var x = _appDbContext.TblVrmDailyPhysicalTestElementconfigs.Where(x => x.IntBusinessUnitId == header.IntBusinessUnitId).Count();

                //  List<string> duplicateItems = new List<string>();
                foreach (var item in createCmnDTO.row)
                {
                    for (int i = 0; i < x; i++)
                    {
                        TblVrmDailyPhysicalTestRow Row = new TblVrmDailyPhysicalTestRow()
                        {
                            IntTestElementId = item.IntTestElementId,
                            IntDailyPhysicalTestId = header.IntDailyPhysicalTestId,
                            NumTestElementValue = item.NumTestElementValue,
                            IsActive = true,
                            IntCreatedBy = item.IntCreatedBy,
                            DteCreatedAt = DateTime.Now

                        };
                        createList.Add(Row);
                    }

                }
                await _appDbContext.TblVrmDailyPhysicalTestRows.AddRangeAsync(createList);
                await _appDbContext.SaveChangesAsync();

                return new MessageHelper()
                {
                    Message = "Success",
                    statuscode = 200
                };

            }
            catch (Exception)
            {
                throw;
            }
        }

        //public async Task<PhysicalTestDTO> GetPhysicalTest(long? BusinessunitId, long? ShiftId, long? MillId, DateTime? Fromdate, DateTime? Todate, long PageNo, long PageSize, string? Search)
        //{
        //    var data = await Task.FromResult
        //        (
        //        from header in _appDbContext.TblVrmDailyPhysicalTestHeaders
        //        join config in _appDbContext.TblVrmDailyPhysicalTestElementconfigs on header.IntBusinessUnitId equals config.IntBusinessUnitId
        //        select new PhysicalTestDTO
        //        {
        //            DteDate = header.DteDate,
        //            IntShiftId = header.IntShiftId,
        //            StrShiftName = header.StrShiftName,
        //            IntBusinessUnitId = header.IntBusinessUnitId,
        //            IntVrmid = header.IntVrmid,
        //            StrVrmname = header.StrVrmname,
        //            IntItemTypeId = header.IntItemTypeId,
        //            StrItemTypeName = header.StrItemTypeName,
        //            IntCreatedBy = header.IntCreatedBy,
        //            NumInitialTime = header.NumInitialTime,
        //            NumFinalTime = header.NumFinalTime,
        //            StrRemark = header.StrRemark,
        //            IsActive = true,
        //            IntUpdatedBy = header.IntUpdatedBy,
        //            DteCreatedAt = header.DteCreatedAt,
        //            //config.IntTestElementId,
        //            //config.StrTestElementName,
        //            //config.IntUoMid,
        //            //config.StrUoMname,


        //            row = (from r in _appDbContext.TblVrmDailyPhysicalTestRows
        //                   where r.IntDailyPhysicalTestId == header.IntDailyPhysicalTestId
        //                   select new PhysicalTestRow
        //                   {


        //                   }).ToList()
        //        } 
        //        );
        //    return data;


        //}


        #endregion

        #region ===================  CREATR UPDATE DELETE =================

        public async Task<MessageHelper> CreateECommerceOnlineDeleveryHeader(ECommerceOnlineDTO ECOnlineDTO)
        {
            // var OnlineDeliveryHeader= await _appDbContext.TblEcommerceOnlineDeliveryHeaders.Where<>

            TblEcommerceOnlineDeliveryHeader header = new TblEcommerceOnlineDeliveryHeader
            {
                IntDeliveryId = ECOnlineDTO.Header.IntDeliveryId,
                StrDeliveryCode = ECOnlineDTO.Header.StrDeliveryCode,
                IntAccountId = ECOnlineDTO.Header.IntAccountId,
                StrAccountCode = ECOnlineDTO.Header.StrAccountCode,
                StrAccountName = ECOnlineDTO.Header.StrAccountName,
                IntBusinessUnitId = ECOnlineDTO.Header.IntBusinessUnitId,
                StrBusinessUnitCode = ECOnlineDTO.Header.StrBusinessUnitCode,
                StrBusinessUnitName = ECOnlineDTO.Header.StrBusinessUnitName,
                StrBusinessUnitAddress = ECOnlineDTO.Header.StrBusinessUnitAddress,
                IntBusinessAreaId = ECOnlineDTO.Header.IntBusinessAreaId,
                StrBusinessAreaCode = ECOnlineDTO.Header.StrBusinessAreaCode,
                StrBusinessAreaName = ECOnlineDTO.Header.StrBusinessAreaName,
                IntSalesOrganizationId = ECOnlineDTO.Header.IntSalesOrganizationId,
                StrSalesOrganizationCode = ECOnlineDTO.Header.StrSalesOrganizationCode,
                StrSalesOrganizationName = ECOnlineDTO.Header.StrSalesOrganizationName,
                IntDistributionChannelId = ECOnlineDTO.Header.IntDistributionChannelId,
                StrDistributionChannelName = ECOnlineDTO.Header.StrDistributionChannelName,
                IntDeliveryTypeId = ECOnlineDTO.Header.IntDeliveryTypeId,
                StrDeliveryTypeName = ECOnlineDTO.Header.StrDeliveryTypeName,
                IntShipToPartnerId = ECOnlineDTO.Header.IntShipToPartnerId,
                StrShipToPartnerName = ECOnlineDTO.Header.StrShipToPartnerName,
                IntSoldToPartnerId = ECOnlineDTO.Header.IntSoldToPartnerId,
                StrSoldToPartnerName = ECOnlineDTO.Header.StrSoldToPartnerName,
                StrSoldToPartnerCode = ECOnlineDTO.Header.StrSoldToPartnerCode,
                StrShipToPartnerAddress = ECOnlineDTO.Header.StrShipToPartnerAddress,
                IntTransportZoneId = ECOnlineDTO.Header.IntTransportZoneId,
                StrTransportZoneName = ECOnlineDTO.Header.StrTransportZoneName,
                DteDeliveryDate = ECOnlineDTO.Header.DteDeliveryDate,
                IntShipPointId = ECOnlineDTO.Header.IntShipPointId,
                StrShipPointName = ECOnlineDTO.Header.StrShipPointName,
                StrAddress = ECOnlineDTO.Header.StrAddress,
                IntPlantId = ECOnlineDTO.Header.IntPlantId,
                StrPlantName = ECOnlineDTO.Header.StrPlantName,
                IntNumberOfTotalItem = ECOnlineDTO.Header.IntNumberOfTotalItem,
                IsPickingEcommerceOnlineted = ECOnlineDTO.Header.IsPickingEcommerceOnlineted,
                IsPackingEcommerceOnlineted = ECOnlineDTO.Header.IsPackingEcommerceOnlineted,
                IsShipmentEcommerceOnlineted = ECOnlineDTO.Header.IsShipmentEcommerceOnlineted,
                IsInventoryEcommerceOnlineted = ECOnlineDTO.Header.IsInventoryEcommerceOnlineted,
                IsBillingEcommerceOnlineted = ECOnlineDTO.Header.IsBillingEcommerceOnlineted,
                IsTax = ECOnlineDTO.Header.IsTax,
                NumTotalDeliveryQuantity = ECOnlineDTO.Header.NumTotalDeliveryQuantity,
                NumTotalDeliveryValue = ECOnlineDTO.Header.NumTotalDeliveryValue,
                NumHeaderDiscountValue = ECOnlineDTO.Header.NumHeaderDiscountValue,
                NumGrossDiscount = ECOnlineDTO.Header.NumGrossDiscount,
                NumTotalNetValue = ECOnlineDTO.Header.NumTotalNetValue,
                StrSellerName = ECOnlineDTO.Header.StrSellerName,
                StrSellerAddress = ECOnlineDTO.Header.StrSellerAddress,
                StrVatChallanNo = ECOnlineDTO.Header.StrVatChallanNo,
                IntWarehouseId = ECOnlineDTO.Header.IntWarehouseId,
                StrWarehouseName = ECOnlineDTO.Header.StrWarehouseName,
                StrWarehouseAddress = ECOnlineDTO.Header.StrWarehouseAddress,
                IntInventoryPgijvid = ECOnlineDTO.Header.IntInventoryPgijvid,
                IntInventoryCogsjvid = ECOnlineDTO.Header.IntInventoryCogsjvid,
                IsRtmChallanReceive = ECOnlineDTO.Header.IsRtmChallanReceive,
                IntActionBy = ECOnlineDTO.Header.IntActionBy,
                DteLastActionDateTime = ECOnlineDTO.Header.DteLastActionDateTime,
                DteServerDateTime = ECOnlineDTO.Header.DteServerDateTime,
                IsActive = ECOnlineDTO.Header.IsActive,
                DteCollectionDate = ECOnlineDTO.Header.DteCollectionDate,
                IsPaymentReceived = ECOnlineDTO.Header.IsPaymentReceived,
                NumPaymentAmount = ECOnlineDTO.Header.NumPaymentAmount,
                DtePaymentDate = ECOnlineDTO.Header.DtePaymentDate,
                IsCashPayment = ECOnlineDTO.Header.IsCashPayment,
                NumCashAmount = ECOnlineDTO.Header.NumCashAmount,
                NumCreditAmount = ECOnlineDTO.Header.NumCreditAmount,
                NumMfsamount = ECOnlineDTO.Header.NumMfsamount,
                NumCashReturn = ECOnlineDTO.Header.NumCashReturn,
                StrCardNo = ECOnlineDTO.Header.StrCardNo,
                NumShippingCharge = ECOnlineDTO.Header.NumShippingCharge,
                IntBankId = ECOnlineDTO.Header.IntBankId,
                StrBankName = ECOnlineDTO.Header.StrBankName,
                NumPoint = ECOnlineDTO.Header.NumPoint,
                IsHold = ECOnlineDTO.Header.IsHold,
                NumCardAmount = ECOnlineDTO.Header.NumCardAmount,
                IntPaymentMethodId = ECOnlineDTO.Header.IntPaymentMethodId,
                StrPaymentMethodName = ECOnlineDTO.Header.StrPaymentMethodName,
                IntSalesReferenceId = ECOnlineDTO.Header.IntSalesReferenceId,
                IsOnline = ECOnlineDTO.Header.IsOnline,
                IntJournalId = ECOnlineDTO.Header.IntJournalId,
                StrJournalCode = ECOnlineDTO.Header.StrJournalCode,
                IsPrint = ECOnlineDTO.Header.IsPrint,
                NumMfscommission = ECOnlineDTO.Header.NumMfscommission,
                NumBankCommission = ECOnlineDTO.Header.NumBankCommission,
                IsDirectSales = ECOnlineDTO.Header.IsDirectSales,
                NumFractionAmount = ECOnlineDTO.Header.NumFractionAmount,
                NumSalesReturnAmount = ECOnlineDTO.Header.NumSalesReturnAmount,
                NumRecoveryAmount = ECOnlineDTO.Header.NumRecoveryAmount,
                IsCampainSales = ECOnlineDTO.Header.IsCampainSales,
                IntBillId = ECOnlineDTO.Header.IntBillId,
                NumBillAmount = ECOnlineDTO.Header.NumBillAmount,
                IntReturnCount = ECOnlineDTO.Header.IntReturnCount,
                NumBillReceiveAmount = ECOnlineDTO.Header.NumBillReceiveAmount,
                NumDueBillAmount = ECOnlineDTO.Header.NumDueBillAmount,

            };
            await _appDbContext.TblEcommerceOnlineDeliveryHeaders.AddAsync(header);
            await _appDbContext.SaveChangesAsync();
            List<TblEcommerceOnlineDeliveryRow> rows = new List<TblEcommerceOnlineDeliveryRow>();
            foreach (var item in ECOnlineDTO.Row)
            {
                TblEcommerceOnlineDeliveryRow row = new TblEcommerceOnlineDeliveryRow
                {
                    IntRowId = item.IntRowId,
                    IntDeliveryId = item.IntDeliveryId,
                    StrDeliveryCode = item.StrDeliveryCode,
                    IntSalesOrderId = item.IntSalesOrderId,
                    StrSalesOrderCode = item.StrSalesOrderCode,
                    IntSalesOrderRowId = item.IntSalesOrderRowId,
                    IntSalesOrderRowSequenceNo = item.IntSalesOrderRowSequenceNo,
                    IntItemId = item.IntItemId,
                    StrItemSalesCode = item.StrItemSalesCode,
                    StrItemSalesName = item.StrItemSalesName,
                    StrItemCode = item.StrItemCode,
                    StrItemName = item.StrItemName,
                    IntUom = item.IntUom,
                    StrUom = item.StrUom,
                    NumQuantity = item.NumQuantity,
                    NumItemPrice = item.NumItemPrice,
                    NumDeliveryValue = item.NumDeliveryValue,
                    NumTotalDiscountValue = item.NumTotalDiscountValue,
                    NumTotalShipingValue = item.NumTotalShipingValue,
                    NumTotalTax = item.NumTotalTax,
                    NumNetValue = item.NumNetValue,
                    IntLocationId = item.IntLocationId,
                    StrLocationName = item.StrLocationName,
                    StrSpecification = item.StrSpecification,
                    IsFreeItem = item.IsFreeItem,
                    IsActive = item.IsActive,
                    StrShipToPartnerContactNo = item.StrShipToPartnerContactNo,
                    NumMrp = item.NumMrp,
                    NumCogs = item.NumCogs,
                    IntReferenceId = item.IntReferenceId,
                    IntDlvrowId = item.IntDlvrowId,
                    IntParentDlvhid = item.IntParentDlvhid,
                    IsSalesReturn = item.IsSalesReturn,
                    NumReturnQuantity = item.NumReturnQuantity,
                    NumMrpdiscount = item.NumMrpdiscount,
                    IntRecordForSalesId = item.IntRecordForSalesId,
                    IsNewSales = item.IsNewSales,

                };
                rows.Add(row);
            }
            await _appDbContext.TblEcommerceOnlineDeliveryRows.AddRangeAsync(rows);
            await _appDbContext.SaveChangesAsync();
            return new MessageHelper
            { Key = 200,
                Message = "success",
            };
        }

        public async Task<TblEcommerceOnlineDeliveryHeader> LandingECommerceOnlineDelevereyHeader(long ID)
        {
            var data = _appDbContext.TblEcommerceOnlineDeliveryHeaders.Where(x => x.IntDeliveryId == ID).FirstOrDefault();
            var dataa = await Task.FromResult(from a in _appDbContext.TblEcommerceOnlineDeliveryHeaders
                               where a.IntDeliveryId == ID
                               select new EcommerceOnlineDeliveryHeaderDTO
                               {
                                   IntDeliveryId = a.IntDeliveryId,
                                   StrDeliveryCode = a.StrDeliveryCode,
                                   IntAccountId = a.IntAccountId,
                                   StrAccountCode = a.StrAccountCode,
                                   StrAccountName = a.StrAccountName,
                                   IntBusinessUnitId = a.IntBusinessUnitId,
                                   StrBusinessUnitCode = a.StrBusinessUnitCode,
                                   StrBusinessUnitName = a.StrBusinessUnitName,
                                   StrBusinessUnitAddress = a.StrBusinessUnitAddress,
                                   IntBusinessAreaId = a.IntBusinessAreaId,
                                   StrBusinessAreaCode = a.StrBusinessAreaCode,
                                   StrBusinessAreaName = a.StrBusinessAreaName,
                                   IntSalesOrganizationId = a.IntSalesOrganizationId,
                                   StrSalesOrganizationCode = a.StrSalesOrganizationCode,
                                   StrSalesOrganizationName = a.StrSalesOrganizationName,
                                   IntDistributionChannelId = a.IntDistributionChannelId,
                                   StrDistributionChannelName = a.StrDistributionChannelName,
                                   IntDeliveryTypeId = a.IntDeliveryTypeId,
                                   StrDeliveryTypeName = a.StrDeliveryTypeName,
                                   IntShipToPartnerId = a.IntShipToPartnerId,
                                   StrShipToPartnerName = a.StrShipToPartnerName,
                                   IntSoldToPartnerId = a.IntSoldToPartnerId,
                                   StrSoldToPartnerName = a.StrSoldToPartnerName,
                                   StrSoldToPartnerCode = a.StrSoldToPartnerCode,
                                   StrShipToPartnerAddress = a.StrShipToPartnerAddress,
                                   IntTransportZoneId = a.IntTransportZoneId,
                                   StrTransportZoneName = a.StrTransportZoneName,
                                   DteDeliveryDate = a.DteDeliveryDate,
                                   IntShipPointId = a.IntShipPointId,
                                   StrShipPointName = a.StrShipPointName,
                                   StrAddress = a.StrAddress,
                                   IntPlantId = a.IntPlantId,
                                   StrPlantName = a.StrPlantName,
                                   IntNumberOfTotalItem = a.IntNumberOfTotalItem,
                                   IsPickingEcommerceOnlineted = a.IsPickingEcommerceOnlineted,
                                   IsPackingEcommerceOnlineted = a.IsPackingEcommerceOnlineted,
                                   IsShipmentEcommerceOnlineted = a.IsShipmentEcommerceOnlineted,
                                   IsInventoryEcommerceOnlineted = a.IsInventoryEcommerceOnlineted,
                                   IsBillingEcommerceOnlineted = a.IsBillingEcommerceOnlineted,
                                   IsTax = a.IsTax,
                                   NumTotalDeliveryQuantity = a.NumTotalDeliveryQuantity,
                                   NumTotalDeliveryValue = a.NumTotalDeliveryValue,
                                   NumHeaderDiscountValue = a.NumHeaderDiscountValue,
                                   NumGrossDiscount = a.NumGrossDiscount,
                                   NumTotalNetValue = a.NumTotalNetValue,
                                   StrSellerName = a.StrSellerName,
                                   StrSellerAddress = a.StrSellerAddress,
                                   StrVatChallanNo = a.StrVatChallanNo,
                                   IntWarehouseId = a.IntWarehouseId,
                                   StrWarehouseName = a.StrWarehouseName,
                                   StrWarehouseAddress = a.StrWarehouseAddress,
                                   IntInventoryPgijvid = a.IntInventoryPgijvid,
                                   IntInventoryCogsjvid = a.IntInventoryCogsjvid,
                                   IntSalesJvid = a.IntSalesJvid,
                                   IsRtmChallanReceive = a.IsRtmChallanReceive,
                                   IntActionBy = a.IntActionBy,
                                   DteLastActionDateTime = a.DteLastActionDateTime,
                                   DteServerDateTime = a.DteServerDateTime,
                                   IsActive = a.IsActive,
                                   DteCollectionDate = a.DteCollectionDate,
                                   IsPaymentReceived = a.IsPaymentReceived,
                                   NumPaymentAmount = a.NumPaymentAmount,
                                   DtePaymentDate = a.DtePaymentDate,
                                   IsCashPayment = a.IsCashPayment,
                                   NumCashAmount = a.NumCashAmount,
                                   NumCreditAmount = a.NumCreditAmount,
                                   NumMfsamount = a.NumMfsamount,
                                   NumCashReturn = a.NumCashReturn,
                                   StrCardNo = a.StrCardNo,
                                   NumShippingCharge = a.NumShippingCharge,
                                   IntBankId = a.IntBankId,
                                   StrBankName = a.StrBankName,
                                   NumPoint = a.NumPoint,
                                   IsHold = a.IsHold,
                                   NumCardAmount = a.NumCardAmount,
                                   IntPaymentMethodId = a.IntPaymentMethodId,
                                   StrPaymentMethodName = a.StrPaymentMethodName,
                                   IntSalesReferenceId = a.IntSalesReferenceId,
                                   IsOnline = a.IsOnline,
                                   IntJournalId = a.IntJournalId,
                                   StrJournalCode = a.StrJournalCode,
                                   IsPrint = a.IsPrint,
                                   NumMfscommission = a.NumMfscommission,
                                   NumBankCommission = a.NumBankCommission,
                                   IsDirectSales = a.IsDirectSales,
                                   NumFractionAmount = a.NumFractionAmount,
                                   NumSalesReturnAmount = a.NumSalesReturnAmount,
                                   NumRecoveryAmount = a.NumRecoveryAmount,
                                   IsCampainSales = a.IsCampainSales,
                                   IntBillId = a.IntBillId,
                                   NumBillAmount = a.NumBillAmount,
                                   IntReturnCount = a.IntReturnCount,
                                   NumBillReceiveAmount = a.NumBillReceiveAmount,
                                   NumDueBillAmount = a.NumDueBillAmount,
                                   Status = a.Status,
                                   Row = (from r in _appDbContext.TblEcommerceOnlineDeliveryRows
                                          where r.IntDeliveryId == a.IntDeliveryId
                                          select new EcommerceOnlineDeliveryRowDTO
                                          {
                                              IntRowId = r.IntRowId,
                                              IntDeliveryId = r.IntDeliveryId,
                                              StrDeliveryCode = r.StrDeliveryCode,
                                              IntSalesOrderId = r.IntSalesOrderId,
                                              StrSalesOrderCode = r.StrSalesOrderCode,
                                              IntSalesOrderRowId = r.IntSalesOrderRowId,
                                              IntSalesOrderRowSequenceNo = r.IntSalesOrderRowSequenceNo,
                                              IntItemId = r.IntItemId,
                                              StrItemSalesCode = r.StrItemSalesCode,
                                              StrItemSalesName = r.StrItemSalesName,
                                              StrItemCode = r.StrItemCode,
                                              StrItemName = r.StrItemName,
                                              IntUom = r.IntUom,
                                              StrUom = r.StrUom,
                                              NumQuantity = r.NumQuantity,
                                              NumItemPrice = r.NumItemPrice,
                                              NumDeliveryValue = r.NumDeliveryValue,
                                              NumTotalDiscountValue = r.NumTotalDiscountValue,
                                              NumTotalShipingValue = r.NumTotalShipingValue,
                                              NumTotalTax = r.NumTotalTax,
                                              NumNetValue = r.NumNetValue,
                                              IntLocationId = r.IntLocationId,
                                              StrLocationName = r.StrLocationName,
                                              StrSpecification = r.StrSpecification,
                                              IsFreeItem = r.IsFreeItem,
                                              IsActive = r.IsActive,
                                              StrShipToPartnerContactNo = r.StrShipToPartnerContactNo,
                                              NumMrp = r.NumMrp,
                                              NumCogs = r.NumCogs,
                                              IntReferenceId = r.IntReferenceId,
                                              IntDlvrowId = r.IntDlvrowId,
                                              IntParentDlvhid = r.IntParentDlvhid,
                                              IsSalesReturn = r.IsSalesReturn,
                                              NumReturnQuantity = r.NumReturnQuantity,
                                              NumMrpdiscount = r.NumMrpdiscount,
                                              IntRecordForSalesId = r.IntRecordForSalesId,
                                              IsNewSales = r.IsNewSales,


                                          }).ToList()

                               });
            return data;
        }
        #endregion

    }
}
