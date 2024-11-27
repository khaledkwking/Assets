using AssetsManament.ViewModels;
using Infrastructure.DAL.Model.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Infrastructure.DAL
{


    public class SharedBusinessrRepository : BaseRepository
    {

        public SharedBusinessrRepository(AssetsEntitiesNew _context) : base(_context)
        {

        }

        public List<view_ItemCategoryTree> GetItemCategoryList()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_ItemCategoryTree
                     select obj);

                return result.ToList<view_ItemCategoryTree>();
            }
        }
        public ItemCategoryViewModel GetCategoryDetails(int Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                return
                    (from obj in DC.D_ItemsCategory
                     where obj.Code == Code
                     select new ItemCategoryViewModel
                     {
                         Code = obj.Code,
                         Cat_ParentId = obj.Cat_ParentId,
                         TitleEn = obj.TitleEn,
                         TitleAr = obj.TitleAr,
                         FinanceRefCode = obj.FinanceRefCode,
                         ServicePeriod = obj.ServicePeriod,
                         ScrapPrice = obj.ScrapPrice,
                     }).FirstOrDefault();

            }
        }

        public List<ItemViewModel> GetCategoryItemList(int Parentid)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                   from obj in DC.sp_CategoryItemList(Parentid)
                   select new ItemViewModel
                   {
                       Code = obj.Code,
                       ItemRefCode = obj.ItemRefCode,
                       ItemBarCode = obj.ItemBarCode,
                       ItemRFIDCode = obj.ItemRFIDCode,
                       ItemFinanceCode = obj.ItemFinanceCode,
                       ItemQrCode = obj.ItemQrCode,
                       ItemNameEn = obj.ItemNameEn,
                       ItemNameAr = obj.ItemNameAr,
                       ItemDescEn = obj.ItemDescEn,
                       ItemDescAr = obj.ItemDescAr,
                       ItemCategoryId = obj.ItemCategoryId,
                       ItemBasePrice = obj.ItemBasePrice,
                       QUnitCode = obj.QUnitCode,
                       MinQty = obj.MinQty,
                       Notes = obj.Notes,
                       ItemImage = obj.ItemImage,
                       D_ItemsCategoryTitleAr = obj.D_ItemsCategoryTitleAr,
                       D_ItemsCategoryTitleEn = obj.D_ItemsCategoryTitleEn,
                       D_QtyUnitTitleAr = obj.D_QtyUnitTitleAr,
                       D_QtyUnitTitleEn = obj.D_QtyUnitTitleEn
                   };

                return result.ToList<ItemViewModel>();
            }
        }

        public List<EntityEmployeeViewModel> getEntityEmployeeList(int Parentid)
        {
            //using (var DC = new AssetsEntitiesNew())
            //{
            //    var result =
            //       from obj in DC.sp_getEntityEmployeeList(Parentid)
            //       select new EntityEmployeeViewModel
            //       {
            //           Code = obj.Code,
            //           EmpCode = obj.EmpCode,
            //           JobTitleId = obj.JobTitleId,
            //           OrgRefId = obj.OrgRefId,
            //           EmpName = obj.EmpName,
            //           CivilId = obj.CivilId,
            //           Phone = obj.Phone,
            //           Mobile = obj.Mobile,
            //           JolbTitleAr = obj.JolbTitleAr,
            //           JolbTitleEn = obj.JolbTitleEn,
            //           EntityNameEn = obj.EntityNameEn,
            //           EntityNameAr = obj.EntityNameAr,
            //       };

            //    return result.ToList<EntityEmployeeViewModel>();
            //}
            return null;
        }

        public List<LocationViewModelEdit> GetLocationList()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_Locations
                     orderby obj.LocationParentId

                     select new LocationViewModelEdit
                     {
                         Code = obj.Code,
                         LocationType = obj.LocationType,
                         LocationNameEn = obj.LocationNameEn,
                         LocationNameAr = obj.LocationNameAr,
                         LocationParentId = obj.LocationParentId.Value,

                         OrgChartRefCode = obj.OrgChartRefCode,
                         LocationRefCode = obj.LocationRefCode,
                         LocationTypeTitleEn = obj.D_LocationType.TitleEn,
                         LocationTypeTitleAr = obj.D_LocationType.TitleAr,


                     });

                return result.ToList<LocationViewModelEdit>();
            }



        }



        public List<LocationViewModel> GetLocationTree()
        {


            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_Locations
                     .Include("D_LocationType")
                     orderby obj.LocationParentId
                     select obj).ToList();

                return GetChildren(result, 0);
            }




        }

        public List<LocationViewModel> GetChildren(List<D_Locations> source, int parentId)
        {
            return source
                    .Where(c => c.LocationParentId == parentId)
                    .Select(c => new LocationViewModel
                    {
                        id = c.Code,
                        title = c.LocationNameAr,
                        ParentId = c.LocationParentId.Value,
                        subs = GetChildren(source, c.Code)
                    })
                    .ToList();
        }

        public LocationViewModelEdit GetLocationDetails(int Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                return
                    (from obj in DC.D_Locations
                     .Include("D_LocationType")
                     where obj.Code == Code
                     select new LocationViewModelEdit
                     {
                         Code = obj.Code,
                         LocationType = obj.LocationType,
                         LocationNameEn = obj.LocationNameEn,
                         LocationNameAr = obj.LocationNameAr,
                         LocationParentId = obj.LocationParentId.Value,

                         OrgChartRefCode = obj.OrgChartRefCode,
                         LocationRefCode = obj.LocationRefCode,
                         LocationTypeTitleEn = obj.D_LocationType.TitleEn,
                         LocationTypeTitleAr = obj.D_LocationType.TitleAr

                     }).FirstOrDefault();

            }
        }

        //public EntityViewModelEdit GetEntityChart(int Code)
        //{
        //    using (var DC = new AssetsEntitiesNew())
        //    {
        //        return
        //            (from obj in DC.D_OrgChart
        //             where obj.Code == Code
        //             select new EntityViewModelEdit
        //             {
        //                 Code = obj.Code,
        //                 EntityType = obj.EntityType,
        //                 EntityNameEn = obj.EntityNameEn,
        //                 EntityNameAr = obj.EntityNameAr,
        //                 ParentId = obj.ParentId.Value


        //             }).FirstOrDefault();

        //    }
        //}

        //public List<EntityViewModelEdit> GetEntityList()
        //{
        //    using (var DC = new AssetsEntitiesNew())
        //    {
        //        var result =
        //            (from obj in DC.D_OrgChart
        //             orderby obj.ParentId
        //             select new EntityViewModelEdit
        //             {
        //                 Code = obj.Code,
        //                 EntityType = obj.EntityType,
        //                 EntityNameEn = obj.EntityNameEn,
        //                 EntityNameAr = obj.EntityNameAr,
        //                 ParentId = obj.ParentId.Value
        //             });

        //        return result.ToList<EntityViewModelEdit>();
        //    }
        //}

        public List<LocationViewModelEdit> getEntityLocationList(int entityNode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                return
                    (from obj in DC.D_Locations
                     .Include("D_LocationType")
                     where obj.OrgChartRefCode == entityNode
                     select new LocationViewModelEdit
                     {
                         Code = obj.Code,
                         LocationType = obj.LocationType,
                         LocationNameEn = obj.LocationNameEn,
                         LocationNameAr = obj.LocationNameAr,
                         LocationParentId = obj.LocationParentId.Value,

                         OrgChartRefCode = obj.OrgChartRefCode,
                         LocationRefCode = obj.LocationRefCode,
                         LocationTypeTitleEn = obj.D_LocationType.TitleEn,
                         LocationTypeTitleAr = obj.D_LocationType.TitleAr

                     }).ToList();


            }
        }


        public List<CustodyListViewModel> getCustodyList(List<int> entityNode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                return
                    (from obj in DC.view_CustodyList
                     .GroupBy(x => new
                     {
                         x.OrgChartRefCode,
                         x.ItemCategoryId,
                         x.ItemsCategoryTitleAr,
                         x.ServicePeriod,
                         x.ScrapPrice,
                         x.ItemCode,
                         x.ItemRefCode,
                         x.ItemNameAr,
                         x.QtyUnitTitleAr
                     })
                     where entityNode.Contains(obj.Key.OrgChartRefCode.Value)
                     select new CustodyListViewModel
                     {
                         ItemCategoryId = obj.Key.ItemCategoryId,
                         ItemsCategoryTitleAr = obj.Key.ItemsCategoryTitleAr,
                         OrgChartRefCode = obj.Key.OrgChartRefCode,
                         ItemCode = obj.Key.ItemCode,
                         ItemRefCode = obj.Key.ItemRefCode,
                         ItemNameAr = obj.Key.ItemNameAr,
                         ServicePeriod = obj.Key.ServicePeriod,
                         ScrapPrice = obj.Key.ScrapPrice,
                         QtyUnitTitleAr = obj.Key.QtyUnitTitleAr,
                         Qty = obj.Sum(x => x.Qty),
                         RequestItemPrice =Math.Round( obj.Average(x => x.RequestItemPrice.Value),3),

                     }).ToList();

            }
        }
        public List<view_AssetsEventTrackingHeader> getCustodyListHeader(List<int> entityNode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                return
                    (from obj in DC.view_AssetsEventTrackingHeader

                     where entityNode.Contains(obj.OraEntityRefCode.Value)
                     select obj).ToList();
                     

            }
        }

    }
    public partial class view_AssetsEventTrackingHeaderDisplay
    {
        public int Code { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string RequestRefCode { get; set; }
        public Nullable<int> RequestActionType { get; set; }
        public Nullable<int> ProcessType { get; set; }
        public Nullable<int> TMonth { get; set; }
        public Nullable<int> TYear { get; set; }
        public string Serial { get; set; }
        public Nullable<int> ToLocationId { get; set; }
        public Nullable<int> EmpRefCode { get; set; }
        public string EmpName { get; set; }
        public string RequestNotes { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedAt { get; set; }
        public string Locationpath { get; set; }
        public Nullable<int> OrgChartRefCode { get; set; }
        public Nullable<int> Ora_EmpRefCode { get; set; }
        public string Ora_EmpName { get; set; }
        public string Ora_EmpCivilId { get; set; }
        public Nullable<int> OraEntityRefCode { get; set; }
        public Nullable<int> Emp_Id { get; set; }
        public Nullable<bool> Emp_Active { get; set; }

        public string EmpStatus { get; set; }
        public string Connected { get; set; }
        public string Type { get; set; }

    }
}
