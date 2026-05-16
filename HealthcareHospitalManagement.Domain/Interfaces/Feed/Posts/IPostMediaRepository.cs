using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using HealthcareHospitalManagement.Domain.Enums.Feed.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts
{
    public interface IPostMediaRepository : IGenericRepository<PostMedia>
    {
        // নির্দিষ্ট একটি পোস্টের সব মিডিয়া ফাইল সঠিক সিরিয়ালে (SortOrder) আনা
        IAsyncEnumerable<PostMedia> GetMediaByPostIdStream(Guid postId);

        // মিডিয়া টাইপ অনুযায়ী ফিল্টার করা (যেমন: শুধু ভিডিওগুলো দেখা)
        IAsyncEnumerable<PostMedia> GetMediaByTypeStream(PostMediaType type);

        // পোস্টের মিডিয়াগুলোর সিরিয়াল বা অর্ডার আপডেট করা
        Task UpdateMediaSortOrderAsync(Guid mediaId, int newOrder);

        // একটি পোস্টের সব মিডিয়া একসাথে ডিলিট করা (যদি প্রয়োজন হয়)
        Task DeleteAllMediaByPostIdAsync(Guid postId);
    }
}
