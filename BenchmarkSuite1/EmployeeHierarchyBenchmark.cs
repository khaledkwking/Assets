using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenchmarkSuite1
{
    [ShortRunJob]
    public class GetEmployeeHierarchyBenchmark
    {
        private List<MockEmployeeViewModel> _mockEmployees;

        [GlobalSetup]
        public void Setup()
        {
            // Create mock employee data
            _mockEmployees = Enumerable.Range(1, 100)
                .Select(i => new MockEmployeeViewModel
                {
                    EMP_ID = i.ToString(),
                    EMP_NAME = $"Employee {i}",
                    JOB_NAME = "Engineer"
                })
                .ToList();
        }

        [Benchmark(Description = "Current Implementation (with N+1 queries)")]
        public void CurrentImplementation()
        {
            // Simulate the current N+1 behavior
            var employeesList = new List<MockEmployeeViewModel>(_mockEmployees);
            
            foreach (var item in employeesList)
            {
                int empCode = int.TryParse(item.EMP_ID, out var code) ? code : 0;
                // Simulating database query for each employee (N queries)
                var header = SimulateGetTrackingRequestHeader(empCode);
                item.AssetsStatus = header != null ? "HasAsset" : "HasNotAsset";
                item.AssetsStatusFlag = header != null ? "1" : "0";
            }
        }

        [Benchmark(Description = "Optimized Implementation (Batch queries)")]
        public void OptimizedImplementation()
        {
            // Simulate optimized batch query
            var employeesList = new List<MockEmployeeViewModel>(_mockEmployees);
            var empCodes = employeesList
                .Select(e => int.TryParse(e.EMP_ID, out var code) ? code : 0)
                .Where(c => c > 0)
                .ToList();
            
            // Simulating a single batch query instead of N queries
            var headersByEmpCode = SimulateGetTrackingRequestHeadersBatch(empCodes)
                .ToDictionary(h => h.EmpCode, h => h);
            
            foreach (var item in employeesList)
            {
                int empCode = int.TryParse(item.EMP_ID, out var code) ? code : 0;
                var hasAsset = headersByEmpCode.ContainsKey(empCode);
                item.AssetsStatus = hasAsset ? "HasAsset" : "HasNotAsset";
                item.AssetsStatusFlag = hasAsset ? "1" : "0";
            }
        }

        private MockAssetHeader SimulateGetTrackingRequestHeader(int empCode)
        {
            // Simulate database delay (1ms per query)
            System.Threading.Thread.Sleep(1);
            return empCode % 3 == 0 ? new MockAssetHeader { EmpCode = empCode } : null;
        }

        private List<MockAssetHeader> SimulateGetTrackingRequestHeadersBatch(List<int> empCodes)
        {
            // Simulate single database query with minimal overhead (5ms total)
            System.Threading.Thread.Sleep(5);
            return empCodes.Where(code => code % 3 == 0)
                .Select(code => new MockAssetHeader { EmpCode = code })
                .ToList();
        }
    }

    public class MockEmployeeViewModel
    {
        public string EMP_ID { get; set; }
        public string EMP_NAME { get; set; }
        public string JOB_NAME { get; set; }
        public string AssetsStatus { get; set; }
        public string AssetsStatusFlag { get; set; }
    }

    public class MockAssetHeader
    {
        public int EmpCode { get; set; }
    }
}
