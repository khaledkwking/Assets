using Infrastructure;

using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetsManament.ViewModels;
namespace BOL.Model
{

    public  class UnitOfReports
    {
        // Added properties:
        //private UnitOfWork u;
        private  AssetsRepository u = IoC.Resolve<AssetsRepository>();
        public UnitOfReports()
        {
             u = new AssetsRepository();
        }
        public static List<EmployeeViewModel> GetAllEmployees()
        {

            var entities = new List<EmployeeViewModel>();
            return entities.ToList();
        }

        public static List<view_LocationTree> GetLocationTree()
        {

            var entities = new List<view_LocationTree>();
            return entities.ToList();
        }
        //public static List<tbl_ItemsStock> GetAllItemStock()
        //{

        //    //var entities = UWork.ItemsStockManager.GetNotDelAll();
        //    var entities = new List<tbl_ItemsStock>();
        //    return entities.ToList();
        //}
        //public static List<vwOutOrderDetails> GetOutOrderItems()
        //{
        //    UnitOfWork UWork = new UnitOfWork();
        //    //var entities = UWork.ItemsStockManager.GetNotDelAll();
        //    var entities = new List<vwOutOrderDetails>();
        //    return entities.ToList();
        //}
        //public static List<vwInOrderDetails> GetInOrderItems()
        //{

        //    //var entities = UWork.ItemsStockManager.GetNotDelAll();
        //    var entities = new List<vwInOrderDetails>();
        //    return entities.ToList();
        //}
        //public static List<vwDestroyOrdersDetails> GetDestoryOrderItems()
        //{

        //    //var entities = UWork.ItemsStockManager.GetNotDelAll();
        //    var entities = new List<vwDestroyOrdersDetails>();
        //    return entities.ToList();
        //}
        //public static List<vwHandOverOrdersDetails> GetHandOverOrderItems()
        //{

        //    //var entities = UWork.ItemsStockManager.GetNotDelAll();
        //    var entities = new List<vwHandOverOrdersDetails>();
        //    return entities.ToList();
        //}
        //public static List<vwTransferOrdersDetails> GetTransferOrderItems()
        //{

        //    //var entities = UWork.ItemsStockManager.GetNotDelAll();
        //    var entities = new List<vwTransferOrdersDetails>();
        //    return entities.ToList();
        //}
        //public static List<vwChangeQuantityOrdersDetails> GetChangeQuantityOrderItems()
        //{

        //    //var entities = UWork.ItemsStockManager.GetNotDelAll();
        //    var entities = new List<vwChangeQuantityOrdersDetails>();
        //    return entities.ToList();
        //}
        //public static List<vwAllOrders> GetAllOrderItems()
        //{

        //    //var entities = UWork.ItemsStockManager.GetNotDelAll();
        //    var entities = new List<vwAllOrders>();
        //    return entities.ToList();
        //}
        //public static List<vwReturnInOrdersDetails> GetReturnInOrderItems()
        //{

        //    //var entities = UWork.ItemsStockManager.GetNotDelAll();
        //    var entities = new List<vwReturnInOrdersDetails>();
        //    return entities.ToList();
        //}
        //public static List<vwReturnOutOrdersDetails> GetReturnOutOrderItems()
        //{

        //    //var entities = UWork.ItemsStockManager.GetNotDelAll();
        //    var entities = new List<vwReturnOutOrdersDetails>();
        //    return entities.ToList();
        //}


    }
    
}
