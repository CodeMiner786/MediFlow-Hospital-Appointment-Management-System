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
    public interface IPostContentRepository : IGenericRepository<PostContent>
    {
        // পোস্ট আইডি দিয়ে সরাসরি কন্টেন্ট খুঁজে বের করা
        Task<PostContent?> GetByPostIdAsync(Guid postId);

        // নির্দিষ্ট টাইপ অনুযায়ী (যেমন: Article, Poll, Video) পোস্ট কন্টেন্ট ফিল্টার করা
        IAsyncEnumerable<PostContent> GetByPostTypeStream(PostType type);

        // নির্দিষ্ট ল্যাঙ্গুয়েজ অনুযায়ী কন্টেন্ট সার্চ করা (যেমন: শুধু 'Bengali' পোস্ট)
        IAsyncEnumerable<PostContent> GetByLanguageStream(string language);

        // টেক্সট বডির মধ্যে নির্দিষ্ট কিওয়ার্ড দিয়ে কন্টেন্ট সার্চ করা
        IAsyncEnumerable<PostContent> SearchByKeywordStream(string keyword);
    }
}
