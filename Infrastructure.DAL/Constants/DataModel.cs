using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL
{
    public class DataModel
    {
        public static int pageSize = 15;
        public static int DayRange = 3;
        public static string EditMode = "Edit";
        public static string AddMode = "Add";
        public static int MainKortasaId = 4;
        public const int RequestLevel1 = 1;// انتظار اعتماد امين المخزن
        //public const int RequestLevel2 = 2;// انتظار اعتماد رئيس المخازن
        public const int RequestLevel3 = 3;// انتظار اعتماد مدير إدارة الشؤون المالية 
        public const int RequestLevel4 = 4;// انتظار صرف امين المخزن
        public const int RequestLevel5 = 5;// انتظار تاكيد الاستلام

        public const int RejectStatus = 6;// طلب مرفوض
        public const int ReturnStatus = 7;// تم الاسترجاع

        public const int WaitConfrimStatus = 5;// تم الاسترجاع

        public const int ConfrimStatus = 8; // تم التاكيد

        public const int SuperAdminRole = 1;
        public const int SecretaryRole = 4;
        public const int Inventory_ManagerRole = 5;
        public const int SupervisorRole = 6;
        public const int StorekeeperRole = 7;
        public const int AdminRoleId = 1;
        public static string access_token ="";

        public const int SReject =0; // reject status
        public const int SPending = 1; //reject Pending
        public const int SAccept = 2; //reject Accept

        public static string ItemNotFound = "-الصنف غير موجود بالمخزن";
        public static string BarcodedifferenceMessage = "يجب ان يكون الباركود اصناف العهد مختلفين فى الباركود";
        public static string QtyinsufficientMessage = "الكمية غير كافية للاصناف العهده (غير نثرية)";
        public static string DefineAssetItemsMessage = "يجب اولا  يتم تحديد الاصناف العهد";
        public static string LessQtyInStock = "الكمية الموجود بالمخزن غير كافية من الصنف";
        public static string ErrorIdInStock = "خطا فى رقم الصف الموجود بالسند";
        public static string CurrentItemQuantity = "الكمية الحالية للصنف";
        public const string StorekeeperTitleEng  = "Store Keeper";
        public const string SupervisorTitleEng = "head of department";
        public const string StorekeeperTitle = "امين مخزن";
        public const string SupervisorTitle = "مدير إدارة الشئون المالية";
        public static readonly int[] Locationtypes = { 1, 2,4 }; // Add allowed table names here
        public static readonly string  HasAsset="العهده مسجله";
        public static readonly string HasNotAsset = "العهده غير مسجله";
        //private static readonly string[] AllowedTables = { "Categories", "D_Locations", "Assets" }; // Add allowed table names here
    }
    
}
