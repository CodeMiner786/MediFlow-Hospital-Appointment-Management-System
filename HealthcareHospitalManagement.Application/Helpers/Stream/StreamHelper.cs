using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Helpers.Stream
{
    public static class StreamHelper
    {
        // Stream → List এ convert করা
        public static async Task<List<T>> ToList<T>(IAsyncEnumerable<T> stream, CancellationToken ct)
        {
            var list = new List<T>();
            await foreach (var item in stream.WithCancellation(ct))
            {
                list.Add(item);
            }
            return list;
        }

        // Pagination + Mapping
        public static PagedResultDto<TDto> Paginate<TEntity, TDto>(
            List<TEntity> items, int pageNumber, int pageSize, IMapper mapper)
        {
            var totalCount = items.Count;
            var pagedItems = items
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var mapped = mapper.Map<List<TDto>>(pagedItems);

            return new PagedResultDto<TDto>(mapped, totalCount, pageNumber, pageSize);
        }
    }
}
