namespace HealthcareHospitalManagement.Domain.Common.PagedResponse
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalRecords { get; private set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        private PagedResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecords)
        {
            Data = data;
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            PageSize = pageSize <= 0 ? 10 : pageSize;
            TotalRecords = totalRecords;
        }

        public static PagedResponse<T> Create(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecords)
            => new(data, pageNumber, pageSize, totalRecords);
    }
}
