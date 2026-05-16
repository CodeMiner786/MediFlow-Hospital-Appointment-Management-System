using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts
{
    public interface IPostShareRepository : IGenericRepository<PostShare>
    {
        // নির্দিষ্ট একটি পোস্ট কতবার শেয়ার হয়েছে তার সংখ্যা
        Task<int> GetShareCountByInteractionIdAsync(Guid interactionId);

        // একজন ইউজার কতগুলো পোস্ট শেয়ার করেছেন তার লিস্ট (Async Stream)
        IAsyncEnumerable<PostShare> GetSharesByUserIdStream(Guid userId);

        // নির্দিষ্ট একটি পোস্টের সব শেয়ার এবং শেয়ারকারী ইউজারদের তথ্য দেখা
        IAsyncEnumerable<PostShare> GetSharesByInteractionIdStream(Guid interactionId);

        // শেয়ার করা পোস্টে ইউজারের দেওয়া নোট (ShareNote) সার্চ করা
        IAsyncEnumerable<PostShare> SearchSharesByNoteStream(string keyword);
    }
}
