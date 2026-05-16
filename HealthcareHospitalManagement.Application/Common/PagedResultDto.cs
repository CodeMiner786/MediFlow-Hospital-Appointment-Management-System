using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Application.Common
{
    public class PagedResultDto<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        // ✅ Parameterized constructor যোগ করা হলো
        public PagedResultDto(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        // ✅ Empty constructor (object initializer এর জন্য)
        public PagedResultDto() { }
    }
}
