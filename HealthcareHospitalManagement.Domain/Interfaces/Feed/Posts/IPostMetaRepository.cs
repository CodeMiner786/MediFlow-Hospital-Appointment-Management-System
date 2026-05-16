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
    public interface IPostMetaRepository : IGenericRepository<PostMeta>
    {
        // পোস্ট আইডি দিয়ে মেটা ডেটা খুঁজে বের করা
        Task<PostMeta?> GetByPostIdAsync(Guid postId);

        // নির্দিষ্ট স্ট্যাটাস অনুযায়ী (যেমন: Draft, Published, Scheduled) পোস্টগুলো ফিল্টার করা
        IAsyncEnumerable<PostMeta> GetByStatusStream(PostStatus status);

        // পিন করা (Pinned) পোস্টগুলো খুঁজে বের করা (যাতে প্রোফাইলের উপরে দেখানো যায়)
        IAsyncEnumerable<PostMeta> GetPinnedPostsStream();

        // নির্দিষ্ট ডেট রেঞ্জের মধ্যে পাবলিশ হওয়া পোস্টগুলো দেখা
        IAsyncEnumerable<PostMeta> GetPublishedInDateRangeStream(DateTime start, DateTime end);

        // পোস্টের স্ট্যাটাস পরিবর্তন করা (যেমন: Draft থেকে Published করা)
        Task UpdateStatusAsync(Guid postId, PostStatus newStatus);
    }
}
