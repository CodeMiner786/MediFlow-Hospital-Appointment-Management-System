using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Chat
{
    public interface IConversationRepository : IGenericRepository<Conversation>
    {
        // কোনো ইউজারের আইডি দিয়ে তার সব একটিভ কনভারসেশন খুঁজে বের করা
        IAsyncEnumerable<Conversation> GetUserConversationsStream(Guid userId);

        // দুজন নির্দিষ্ট ইউজারের মধ্যে কোনো এক্সিস্টিং কনভারসেশন আছে কিনা তা দেখা
        Task<Conversation?> GetConversationBetweenUsersAsync(Guid userIdA, Guid userIdB);

        // অ্যাপয়েন্টমেন্ট বা বুকিং রেফারেন্স আইডি দিয়ে কনভারসেশন খুঁজে বের করা
        Task<Conversation?> GetByReferenceIdAsync(Guid referenceId);

        // সর্বশেষ মেসেজ আসার সময় অনুযায়ী কনভারসেশনগুলো সাজিয়ে স্ট্রীম করা
        IAsyncEnumerable<Conversation> GetRecentConversationsStream(Guid userId);
    }
}
