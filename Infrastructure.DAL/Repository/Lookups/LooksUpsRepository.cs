using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.DAL.Model.DB;

namespace Infrastructure.DAL
{
    public class LooksUpsRepository
    {
        public static LooksUpsRepository ins = new LooksUpsRepository();

        #region "Shared"

        public List<D_AttachmentType> FillAttachmentTypes()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_AttachmentType
                     orderby obj.TitleEn ascending
                     select obj);

                return result.ToList<D_AttachmentType>();
            }
        }

        public List<D_ItemsCategory> FillItemCategory()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_ItemsCategory
                     orderby obj.TitleEn ascending
                     select obj);

                return result.ToList<D_ItemsCategory>();
            }
        }

        public List<D_ItemCard> fillCategoryItems(int CategoryId)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_ItemCard
                     where obj.ItemCategoryId== CategoryId
                     orderby obj.ItemNameAr ascending
                     select obj);

                return result.ToList<D_ItemCard>();
            }
        }

        public List<D_Locations> FillLocations()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_Locations
                     orderby obj.LocationNameAr ascending
                     select obj);

                return result.ToList<D_Locations>();
            }
        }
        public List<D_LocationType> FillLocationsTypes()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_LocationType
                     orderby obj.TitleAr ascending
                     select obj);

                return result.ToList<D_LocationType>();
            }
        }

        public List<D_Country> FillCountries()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_Country
                     orderby obj.TitleEn ascending
                     select obj);

                return result.ToList<D_Country>();
            }
        }

        //public List<D_OrgChart> FillEntityList()
        //{
        //    using (var DC = new AssetsEntitiesNew())
        //    {
        //        var result =
        //            (from obj in DC.D_OrgChart
        //             orderby obj.EntityNameAr ascending
        //             select obj);

        //        return result.ToList<D_OrgChart>();
        //    }
        //}
          public List<D_JobTitle> FIllJobTtile()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_JobTitle
                     orderby obj.TitleAr ascending
                     select obj);

                return result.ToList<D_JobTitle>();
            }
        }

        public List<D_QtyUnit> FillQUnit()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_QtyUnit
                     orderby obj.TitleEn ascending
                     select obj);

                return result.ToList<D_QtyUnit>();
            }
        }


        #endregion

        #region "Inbound lookups"
        public List<D_InboundType> FillInboundTypes()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_InboundType
                     orderby obj.TitleEn ascending
                     select obj);

                return result.ToList<D_InboundType>();
            }
        }

        public List<D_OutboundType> FillOutboundTypes()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_OutboundType
                     orderby obj.TitleEn ascending
                     select obj);

                return result.ToList<D_OutboundType>();
            }
        }


        public List<D_VendorData> Fillvendor()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_VendorData
                     select obj);

                return result.ToList<D_VendorData>();
            }
        }

        public List<view_LocationTree> FillStoreLocations()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_LocationTree
                     where obj.LocationType == 4 // Stores
                     select obj);

                return result.ToList<view_LocationTree>();
            }
        }


        public List<D_Locations> FillInboundStoreLocations()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_Locations
                     where obj.LocationType == 4 && obj.AllowInbound==true // Stores
                     select obj);

                return result.ToList<D_Locations>();
            }
        }



        public List<D_InboundDepositeStatusType> FillDepositeStatusType()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_InboundDepositeStatusType
                     orderby obj.TitleEn ascending
                     select obj);

                return result.ToList<D_InboundDepositeStatusType>();
            }
        }


        public List<D_QtyUnit> FillQuantityCode()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_QtyUnit
                     orderby obj.TitleEn ascending
                     select obj);

                return result.ToList<D_QtyUnit>();
            }
        }
        public List<D_ItemUsedStatus> fillUsedStatus()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_ItemUsedStatus
                     select obj);

                return result.ToList<D_ItemUsedStatus>();
            }
        }
        public List<D_Locations> FillLocation()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_Locations
                     orderby obj.Code
                     select obj);

                return result.ToList<D_Locations>();
            }
        }
        public List<D_EmployeeList> FillEmployee()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_EmployeeList
                     orderby obj.Code
                     select obj);

                return result.ToList<D_EmployeeList>();
            }
        }
        public sp_ItemAutoComp_Result[] FillItemsAuto(string prefixText)
        {
            ArrayList name = new ArrayList();
            ArrayList value = new ArrayList();

            name.Add("@pre");
            value.Add(prefixText.ToLower());
            using (var DC = new AssetsEntitiesNew())
            {

                var result =
                    (from obj in DC.sp_ItemAutoComp(prefixText.ToLower())
                      select obj).ToArray(); ;

                return result;
            }
        }

        #endregion

        #region "Tracking"

        public List<AssetsAvailabilityStatu> FillTrackingStatus()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.AssetsAvailabilityStatus
                     orderby obj.TitleEn ascending
                     select obj);

                return result.ToList<AssetsAvailabilityStatu>();
            }
        }


        public List<AssetsTrackingAction> FillAssetsTrackingActions()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.AssetsTrackingActions
                     orderby obj.TitleEn ascending
                     select obj);

                return result.ToList<AssetsTrackingAction>();
            }
        }



        #endregion

        #region "FillOutbound Lookups"

        //public List<D_OutboundType> FillOutboundTypes()
        //{
        //    using (var DC = new AssetsEntitiesNew())
        //    {
        //        var result =
        //            (from obj in DC.D_OutboundType
        //             orderby obj.TitleEn ascending
        //             select obj);

        //        return result.ToList<D_OutboundType>();
        //    }
        //}


        //public List<D_OutboundwithdrawStatus> FilllWithdrawOrderStatusCode()
        //{
        //    using (var DC = new AssetsEntitiesNew())
        //    {
        //        var result =
        //            (from obj in DC.D_OutboundwithdrawStatus
        //             orderby obj.TitleEn ascending
        //             select obj);

        //        return result.ToList<D_OutboundwithdrawStatus>();
        //    }
        //}



        #endregion
    }
}
