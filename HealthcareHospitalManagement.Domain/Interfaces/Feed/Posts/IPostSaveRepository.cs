using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts
{
    public interface IPostSaveRepository : IGenericRepository<PostSave>
    {
        // ইউজার এই পোস্টটি অলরেডি সেভ করেছে কি না চেক করা
        Task<bool> IsPostAlreadySavedAsync(Guid interactionId, Guid userId, CancellationToken cancellationToken);

        // একজন ইউজারের সব সেভ করা পোস্টগুলো একসাথে দেখা (Async Stream)
        IAsyncEnumerable<PostSave> GetSavedPostsByUserIdStream(Guid userId);

        // নির্দিষ্ট একটি পোস্ট কতজন ইউজার সেভ করেছে তার সংখ্যা
        Task<int> GetSaveCountByInteractionIdAsync(Guid interactionId, CancellationToken cancellationToken);

        // সেভ করা পোস্ট রিমুভ করা (Unsave)
        Task UnsavePostAsync(Guid interactionId, Guid userId, CancellationToken cancellationToken);

        // 🔹 নতুন method: নির্দিষ্ট interaction + user এর সেভ রেকর্ড বের করা
        Task<PostSave?> GetByInteractionAndUserAsync(Guid interactionId, Guid userId, CancellationToken cancellationToken);
    }

}
