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
    public interface IPostAudienceRepository : IGenericRepository<PostAudience>
    {
        // নির্দিষ্ট পোস্টের অডিয়েন্স সেটিংস খুঁজে বের করা
        Task<PostAudience?> GetByPostIdAsync(Guid postId);

        // নির্দিষ্ট ভিজিবিলিটি অনুযায়ী পোস্ট অডিয়েন্স লিস্ট ফিল্টার করা (যেমন: শুধু Public পোস্ট)
        IAsyncEnumerable<PostAudience> GetByVisibilityStream(PostVisibility visibility);

        // নির্দিষ্ট শহর বা লোকেশন টার্গেট করা অডিয়েন্স সেটিংসগুলো দেখা
        IAsyncEnumerable<PostAudience> GetByTargetCityStream(string city);

        // অ্যানোনিমাস (Anonymous) পোস্টগুলোর অডিয়েন্স সেটিংস খুঁজে বের করা
        IAsyncEnumerable<PostAudience> GetAnonymousPostAudiencesStream();
    }
}
