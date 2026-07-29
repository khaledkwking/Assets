using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace UI.Web.Controllers
{
    public class HeplerController : ApiController
    {

        public SharedBusinessrRepository objRepository = IoC.Resolve<SharedBusinessrRepository>();
        public static AssetsRepository objRepository2 = IoC.Resolve<AssetsRepository>();


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
        public List<ItemViewModel> GetCategoryItemList(int nodeId)
        {
            return objRepository.GetCategoryItemList(nodeId);
        }


        [HttpGet]
        [ActionName("GetLocationTree")]
        public List<dynamic> GetLocationTree()
        {
            var locations = objRepository.GetLocationList();

            var result = locations.Select(loc => new
            {
                Code = loc.Code,
                LocationNameAr = loc.LocationNameAr,
                LocationNameEn = loc.LocationNameEn,
                LocationParentId = loc.LocationParentId,
                LocationType = loc.LocationType ?? 0,  // Ensure LocationType is not null
                LocationRefCode = loc.LocationRefCode,
                City = loc.City
            }).ToList<dynamic>();

            return result;
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
        public List<EntityEmployeeViewModel> getEntityEmployeeList(int nodeId)
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
                //return JsonConvert.DeserializeObject<List<ORGANIZATION_CHART>>(result);

                //var json = Res.Content.ReadAsStringAsync().Result;

                // Convert JSON to objects
               var organizationCharts = JsonConvert.DeserializeObject<List<ORGANIZATION_CHART>>(result);
                // exclude organiztion

               
                var allExcluded = objRepository2.GetExcludedCodes();

              
               // var allExcluded = new HashSet<int>(excludedEntityCodes);

                void AddChildren(int parentCode)
                {
                    var children = organizationCharts
                        .Where(x => x.PARENTCODE == parentCode)
                        .Select(x => x.ENTITYCODE);

                    foreach (var child in children)
                    {
                        if (allExcluded.Add(child))
                            AddChildren(child);
                    }
                }

                foreach (var code in allExcluded.ToList()) // ToList() avoids modifying the collection while iterating
                {
                    AddChildren(code);
                }

                organizationCharts = organizationCharts
                    .Where(x => !allExcluded.Contains(x.ENTITYCODE))
                    .ToList();

                return organizationCharts;


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
        // عرض قائمة الموظفين للجهات
        [HttpGet]
        [ActionName("GetEmployeeHierarhcy")]
        public async Task<List<EmployeeViewModel>> GetEmployeeHierarhcy(int nodeId)
        {
          
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["centeralApi"].ToString());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync(string.Format("OrgChart/EmployeeHierarchy/{0}", nodeId));

                    if (!Res.IsSuccessStatusCode)
                        throw new Exception(Res.ToString());

                    var result = await Res.Content.ReadAsStringAsync();

                    List<EmployeeViewModel> EmployeesList = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(result);
                    
                    if (EmployeesList != null && EmployeesList.Count > 0)
                    {
                        // Get all employee codes
                        var empCodes = EmployeesList
                            .Select(e => ZeroIntergerIFNull(e.EMP_ID))
                            .Where(code => code > 0)
                            .Distinct()  // ← Added: Avoid duplicate queries
                            .ToList();

                        // Only fetch if there are employee codes
                        if (empCodes.Count > 0)  // ← Added: Guard clause
                        {
                            var trackingHeadersList = objRepository2.GetTrackingRequestHeadersByEmpIds(empCodes);
                            // Use HashSet for checking existence instead of Dictionary to handle duplicates
                            var empCodesWithAssets = trackingHeadersList != null 
                                ? new HashSet<int>(trackingHeadersList.Select(h => h.EmpRefCode ?? 0))
                                : new HashSet<int>();

                            // Update employee list with asset status
                            foreach (var item in EmployeesList)
                            {
                                int empCode = ZeroIntergerIFNull(item.EMP_ID);
                                if (empCodesWithAssets.Contains(empCode))
                                {
                                    item.AssetsStatus = DataModel.HasAsset;
                                    item.AssetsStatusFlag = "1";
                                }
                                else
                                {
                                    item.AssetsStatus = DataModel.HasNotAsset;
                                    item.AssetsStatusFlag = "0";
                                }
                            }
                        }
                        else
                        {
                            // No valid employee codes, mark all as no assets
                            foreach (var item in EmployeesList)
                            {
                                item.AssetsStatus = DataModel.HasNotAsset;
                                item.AssetsStatusFlag = "0";
                            }
                        }
                    }
                    return EmployeesList;
                }
                catch (Exception ex)
                {
                    return null;
                }
               

            }

        }

        [HttpGet]
        [ActionName("GetEmployeeData")]
        public async Task<List<EmployeeViewModel>> GetEmployeeData(int nodeId)
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

        [HttpGet]
        [ActionName("GetEmployeeEntityCode")]
        public async Task<string> GetEmployeeEntityCode(int nodeId, string empId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["centeralApi"].ToString());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"OrgChart/EmployeeHierarchy/{nodeId}");

                if (!Res.IsSuccessStatusCode)
                    throw new Exception(Res.ToString());

                var result = await Res.Content.ReadAsStringAsync();

                var employees = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(result);

                string entityCode = employees
                                   .FirstOrDefault(e => e.EMP_ID == empId)?
                                   .ENTITYCODE.ToString();

                return entityCode;
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
        public static async Task<string> GetEmployeePosition(int empId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://mystro-dev/mystroapi/api/OrgChart/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"GetEmployeeDetails/{empId}");
                var json = await response.Content.ReadAsStringAsync();
                var arr = JArray.Parse(json);

                if (arr == null || arr.Count == 0)
                    return "not-found";

                var firstItem = arr[0];
                return firstItem["positioN_NO"]?.ToString() ?? "unknown";
            }
        }


        [HttpGet]
        [ActionName("GetEmployeeStatus")]
        public async Task<IHttpActionResult> GetEmployeeStatus(int empId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://mystro-dev/mystroapi/api/OrgChart/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync($"GetEmployeeDetails/{empId}");
                    if (!response.IsSuccessStatusCode)
                        return Ok(new { status = "not-found" });

                    var json = await response.Content.ReadAsStringAsync();

                    // Deserialize into JArray (because response is array)
                    var arr = JsonConvert.DeserializeObject<JArray>(json);
                    if (arr == null || arr.Count == 0)
                        return Ok(new { status = "not-found" });

                    var firstItem = arr[0];
                    var status = firstItem["emP_STATUS"]?.ToString() ?? "unknown";

                    return Ok(new { status = status });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = "error", message = ex.Message });
            }
        }
        // عرض header  الخاص بعهد الموظفين
        [HttpGet]
        [ActionName("GetOrgChartCustodyHeader")]
        public List<view_AssetsEventTrackingHeader> GetOrgChartCustodyHeader(int nodeId)
        {
            List<EmployeeViewModel> nodeChildren = new List<EmployeeViewModel>();
           
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["centeralApi"].ToString());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = client.GetAsync(string.Format("OrgChart/NodeChildrenTree/{0}", nodeId)).Result;

                if (!Res.IsSuccessStatusCode)
                    throw new Exception(Res.ToString());

                var result = Res.Content.ReadAsStringAsync().Result;

                nodeChildren = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(result);

            }
            
            return objRepository.getCustodyListHeader(nodeChildren.Select(x => x.ENTITYCODE.Value).ToList()); 

        }
        // POST: /api/hepler/DeleteCustody
        [HttpPost]
        [Route("api/hepler/DeleteCustody")]
        public IHttpActionResult DeleteCustody([FromBody] CustodyDeleteRequest request)
        {
            try
            {

                using (var db = new AssetsEntitiesNew())
                {
                    // Find the custody record by the 'Code'
                    var custodyRecord = db.AssetsEventTrackingHeaders.FirstOrDefault(c => c.Code == request.Code);

                    if (custodyRecord == null)
                        return NotFound(); // Return 404 if the custody does not exist

                    custodyRecord.IsDeleted = true;
                    
                    db.SaveChanges();
                }

                return Ok(new { success = true, message = "تم الحذف بنجاح" });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //[HttpGet]
        //[ActionName("HandleEmployeeSelection")]
        //public List<EmployeeSelectionResponse>  HandleEmployeeSelection(string selectedValue)
        //{
        //    List<EmployeeSelectionResponse> responseList = new List<EmployeeSelectionResponse>();

        //    // Get the list of employees
        //    int nodeId = ZeroIntergerIFNull(selectedValue);
        //    List<EmployeeViewModel> employeeList = GetOraEmpList(nodeId);

        //    // Find the selected employee
        //    var selectedEmployee = employeeList.FirstOrDefault(x => x.EMP_ID == selectedValue);

        //    if (selectedEmployee != null)
        //    {
        //        // Assuming you have a way to get the location based on the selected employee

        //        var locationCode = objRepository2.getEmployeeLocations(ZeroIntergerIFNull(selectedEmployee.EMP_ID));

        //        // Call fillRequestItems logic (you might need to adapt this)
        //        var requestItems = FillRequestItems(locationCode.LocationCode, selectedValue);

        //        // Prepare the response object
        //        var response = new EmployeeSelectionResponse
        //        {
        //            EMP_NAME = selectedEmployee.EMP_NAME,
        //            JOB_NAME = selectedEmployee.JOB_NAME,
        //            EMP_ID = selectedEmployee.EMP_ID,
        //            LOCATION_NAME = GetEmp_Location(ZeroIntergerIFNull(selectedEmployee.EMP_ID)),
        //            REQUEST_ITEMS = requestItems // Assuming you want to return some request items
        //        };
        //        responseList.Add(response);
        //        //return response;

        //        //return response; // Return the data to the AJAX call
        //    }

        //    return null; // Or handle the case when no employee is found
        //}
        [HttpGet]
        public IHttpActionResult GetEmployeeCustodyItems(string selectedValue)
        {
            //var custodyItems = FillRequestItems(locationCode.LocationCode, employeeId); ; // Replace with your method to get data

            //return Ok(custodyItems);


            int nodeId = ZeroIntergerIFNull(selectedValue);
            List<EmployeeViewModel> employeeList = GetOraEmpList(nodeId);

            // Find the selected employee
            var selectedEmployee = employeeList.FirstOrDefault(x => x.EMP_ID == selectedValue);

            if (selectedEmployee != null)
            {
                // Assuming you have a way to get the location based on the selected employee
                var locationCode = objRepository2.getEmployeeLocations(ZeroIntergerIFNull(selectedEmployee.EMP_ID));

                // Call fillRequestItems logic (you might need to adapt this)
                var requestItems = FillRequestItems(locationCode.LocationCode, selectedValue);

                return Ok(requestItems);
            }
            return null;
        }


        [HttpGet]
        [ActionName("HandleEmployeeSelection")]
        public List<EmployeeSelectionResponse> HandleEmployeeSelection(string selectedValue)
        {
            // Initialize the response list
            List<EmployeeSelectionResponse> responseList = new List<EmployeeSelectionResponse>();

            // Get the list of employees
            int nodeId = ZeroIntergerIFNull(selectedValue);
            List<EmployeeViewModel> employeeList = GetOraEmpList(nodeId);

            // Find the selected employee
            var selectedEmployee = employeeList.FirstOrDefault(x => x.EMP_ID == selectedValue);

            if (selectedEmployee != null)
            {
                // Assuming you have a way to get the location based on the selected employee
                var locationCode = objRepository2.getEmployeeLocations(ZeroIntergerIFNull(selectedEmployee.EMP_ID));

                // Call fillRequestItems logic (you might need to adapt this)
                var requestItems = FillRequestItems(locationCode.LocationCode, selectedValue);

                // Prepare the response object
                var response = new EmployeeSelectionResponse
                {
                    EMP_NAME = selectedEmployee.EMP_NAME,
                    JOB_NAME = selectedEmployee.JOB_NAME,
                    EMP_ID = selectedEmployee.EMP_ID,
                    LOCATION_NAME = GetEmp_Location(ZeroIntergerIFNull(selectedEmployee.EMP_ID)),
                    REQUEST_ITEMS = requestItems // Assuming you want to return some request items
                };

                // Add the response object to the list
                responseList.Add(response);
            }

            // Return the list of responses (even if it's empty)
            return responseList;
        }
     
        public static  Int32 ZeroIntergerIFNull(string obj)
        {
            if (obj.Equals(""))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        public static string GetEmp_Location(int EmpId)
        {

            using (AssetsEntitiesNew en = new AssetsEntitiesNew())
            {
                string FullLocationPath = "";
                var EmpList = en.Get_Emp_Location(EmpId).ToList();
                if (EmpList.Count > 0)
                    FullLocationPath = EmpList.FirstOrDefault().FullLocationPath;

                return FullLocationPath;
            }
        }
        private static List<view_CustodyList> FillRequestItems(int? locationCode, string employeeId)
        {
            // Logic to fill request items similar to your existing fillRequestItems method
            int refHCode = 0;// ZeroIntergerIFNull(hdnMasterID.Value); // You may need to adjust this
            var objList = objRepository2.getCustodyListByMasterData(refHCode, ZeroIntergerIFNull(locationCode.ToString()), ZeroIntergerIFNull(employeeId));

            // Process objList to fill in the details as per your original logic
            if (objList != null && objList.Count > 0)
            {
                // Here you can return the processed list or any other data you need
                return objList;
            }

            return new List<view_CustodyList>(); // Return an empty list if no items found
        }

        public static List<EmployeeViewModel> GetOraEmpList(int nodeId)
        {
            using (AssetsEntitiesNew en = new AssetsEntitiesNew())
            {
                var EmpList = en.Employee_tbl
                    .Where(o => o.Emp_Active == true)
                    .Join(
                        en.D_JobTitle,
                        emp => emp.Job_Id,
                        job => job.Code,
                        (emp, job) => new EmployeeViewModel
                        {
                            EMP_ID = emp.Emp_Id.ToString(),
                            EMP_NAME = emp.Emp_Name,
                            JOB_NAME = job.TitleAr
                        })
                    .ToList();

                return EmpList;
            }
        }
        public static List<view_CustodyList> getCustodyListByMasterData(int RequestHeaderCode, int ToLocationId, int EmpRefCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_CustodyList
                     where obj.EmpRefCode == EmpRefCode
                     && (ToLocationId != 0 ? obj.ToLocationId == ToLocationId : true)
                     && (RequestHeaderCode != 0 ? obj.RequestHeaderCode == RequestHeaderCode : true)
                     select obj);

                return result.ToList<view_CustodyList>();
            }
        }

    }
    public class EmployeeSelectionResponse
    {
        public string EMP_NAME { get; set; }
        public string JOB_NAME { get; set; }
        public string EMP_ID { get; set; }
        public string LOCATION_NAME { get; set; }
        public List<view_CustodyList> REQUEST_ITEMS { get; set; }
    }
    public class CustodyDeleteRequest
    {
        public int Code { get; set; }
    }
}