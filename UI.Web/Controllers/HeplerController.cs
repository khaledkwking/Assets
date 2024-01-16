using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace UI.Web.Controllers
{
    public class HeplerController : ApiController
    {

        public SharedBusinessrRepository objRepository = IoC.Resolve<SharedBusinessrRepository>();


        [HttpGet]
        [ActionName("GetItemsCategoryTree")]
        public List<view_ItemCategoryTree> GetItemsCategoryTree()
        {
            return objRepository.GetItemCategoryList();

        }

        [HttpGet]
        [ActionName("GetItemCategoryDetails")]
        public ItemCategoryViewModel GetItemCategoryDetails(int nodeid)
        {
            return objRepository.GetCategoryDetails(nodeid);

        }

        [HttpGet]
        [ActionName("GetCategoryItemList")]
        public async Task<List<ItemViewModel>> GetCategoryItemList(int nodeId)
        {

            return objRepository.GetCategoryItemList(nodeId);

        }


        [HttpGet]
        [ActionName("GetLocationTree")]
        public List<LocationViewModelEdit> GetLocationTree()
        {
            return objRepository.GetLocationList();

        }
        [HttpGet]
        [ActionName("GetEntityLocationTree")]
        public List<LocationViewModelEdit> GetEntityLocationTree(int entityId)
        {
            var LocationList= objRepository.getEntityLocationList(entityId);
            // Reset Parent Id For First Level
            foreach (var item in LocationList)
            {
                if (!LocationList.Select(x => x.Code).ToArray().Contains(item.LocationParentId)  )
                {
                    item.LocationParentId = 0;
                }

            }
            return LocationList;
        }

        [HttpGet]
        [ActionName("GetLocationHera")]
        public List<LocationViewModel> GetLocationHera()
        {
            return objRepository.GetLocationTree();

        }

        [HttpGet]
        [ActionName("GetLocationDetails")]
        public LocationViewModelEdit GetLocationDetails(int nodeid)
        {
            return objRepository.GetLocationDetails(nodeid);

        }


        //[HttpGet]
        //[ActionName("GetEntityChart")]
        //public EntityViewModelEdit GetEntityChart(int nodeid)
        //{
        //    return objRepository.GetEntityChart(nodeid);

        //}

        //[HttpGet]
        //[ActionName("GetEntityTree")]
        //public List<EntityViewModelEdit> GetEntityTree()
        //{
        //    return objRepository.GetEntityList();

        //}
        [HttpGet]
        [ActionName("EntityEmployeeList")]
        public async Task<List<EntityEmployeeViewModel>> getEntityEmployeeList(int nodeId)
        {

            return objRepository.getEntityEmployeeList(nodeId);

        }



        [HttpGet]
        [ActionName("EntityLocationList")]
        public List<LocationViewModelEdit> getEntityLocationList(int nodeid)
        {
            return objRepository.getEntityLocationList(nodeid);

        }
        [HttpGet]
        [ActionName("orgChart")]
        public async Task<List<ORGANIZATION_CHART>> orgChart(int nodeid)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["centeralApi"].ToString());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(string.Format("orgchart/GetChart"));

                if (!Res.IsSuccessStatusCode)
                    throw new Exception(Res.ToString());

                var result = Res.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<ORGANIZATION_CHART>>(result);
            }
        }

        [HttpGet]
        [ActionName("orgChartList")]
        public List<ORGANIZATION_CHART> orgChartList(int nodeid)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["centeralApi"].ToString());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = client.GetAsync(string.Format("orgchart/GetChart")).Result;

                if (!Res.IsSuccessStatusCode)
                    throw new Exception(Res.ToString());

                var result = Res.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<ORGANIZATION_CHART>>(result);
            }
        }

        [HttpGet]
        [ActionName("GetEmployeeHierarhcy")]
        public async Task<List<EmployeeViewModel>> GetEmployeeHierarhcy(int nodeId)
        {

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["centeralApi"].ToString());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(string.Format("OrgChart/EmployeeHierarchy/{0}", nodeId));

                if (!Res.IsSuccessStatusCode)
                    throw new Exception(Res.ToString());

                var result = Res.Content.ReadAsStringAsync().Result;

                // Get Employee Location 
                //var assignedLocations =   objRepository.GetLocationList();

                return JsonConvert.DeserializeObject<List<EmployeeViewModel>>(result);


            }

        }



        //////Custody 
        ///

        [HttpGet]
        [ActionName("GetOrgChartCustody")]
        public  List<CustodyListViewModel> GetOrgChartCustody(int nodeId)
        {
            List<EmployeeViewModel> nodeChildren = new List<EmployeeViewModel>();
            List<CustodyListViewModel> CustodyList = new List<CustodyListViewModel>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["centeralApi"].ToString());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = client.GetAsync(string.Format("OrgChart/NodeChildrenTree/{0}", nodeId)).Result;

                if (!Res.IsSuccessStatusCode)
                    throw new Exception(Res.ToString());

                var result = Res.Content.ReadAsStringAsync().Result;

                // Get Employee Location 
                //var assignedLocations =   objRepository.GetLocationList();

                nodeChildren = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(result);
               
            }


            if (nodeChildren != null && nodeChildren.Count != 0)
            {//TODO
             // Get Node List Anbd 
                List<int> NodeList = new List<int>();
                foreach (var item in nodeChildren)
                {
                    NodeList.Add(item.ENTITYCODE.Value);
                }
                  CustodyList = objRepository.getCustodyList(NodeList);

                foreach (var item in CustodyList)
                {
                    item.OrgChartRefName = nodeChildren.Where(x => x.ENTITYCODE == item.OrgChartRefCode).FirstOrDefault().ENTITYNAME;

                }
               

            }
            return CustodyList;

        }
    }
}