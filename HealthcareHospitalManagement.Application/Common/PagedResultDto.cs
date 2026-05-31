using System.Collections.Generic;

namespace HealthcareHospitalManagement.Application.Common
{
    public class PagedResultDto<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        // ✅ TotalPages এখন null-safe
        public int TotalPages => PageSize > 0
            ? (int)Math.Ceiling((double)TotalCount / PageSize)
            : 0;

        // ✅ Navigation helpers যোগ করা হলো
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        // ✅ Parameterized constructor
        public PagedResultDto(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items ?? new List<T>();
            TotalCount = totalCount;
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            PageSize = pageSize <= 0 ? 10 : pageSize;
        }

        // ✅ Empty constructor (object initializer এর জন্য)
        public PagedResultDto() { }
    }
}
