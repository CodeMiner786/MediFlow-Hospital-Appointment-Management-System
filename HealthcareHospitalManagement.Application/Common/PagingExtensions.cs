using HealthcareHospitalManagement.Domain.Common.PagedResponse;

namespace HealthcareHospitalManagement.Application.Common
{
    public static class PagingExtensions
    {
        public static PagedResultDto<TDestination> ToMappedPagedResult<TSource, TDestination>(
            this PagedResponse<TSource> pagedResponse,
            AutoMapper.IMapper mapper)
        {
            return new PagedResultDto<TDestination>
            {
                Items = mapper.Map<List<TDestination>>(pagedResponse.Data),
                TotalCount = pagedResponse.TotalRecords,
                PageNumber = pagedResponse.PageNumber,
                PageSize = pagedResponse.PageSize
            };
        }
    }
}

