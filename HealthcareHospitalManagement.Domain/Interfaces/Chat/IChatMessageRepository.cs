using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Chat;
using HealthcareHospitalManagement.Domain.Enums.Chat;

namespace HealthcareHospitalManagement.Domain.Interfaces.Chat
{
    public interface IChatMessageRepository : IGenericRepository<ChatMessage>
    {
        // নির্দিষ্ট একটি কনভারসেশনের সব মেসেজ স্ট্রীম আকারে পাওয়া
        IAsyncEnumerable<ChatMessage> GetMessagesByConversationIdStream(Guid conversationId);

        // নির্দিষ্ট কোনো ইউজারের পাঠানো সব মেসেজ খুঁজে বের করা
        IAsyncEnumerable<ChatMessage> GetMessagesBySenderIdStream(Guid senderId);

        // আনরিড (Unread) মেসেজগুলো খুঁজে বের করার মেথড
        IAsyncEnumerable<ChatMessage> GetUnreadMessagesByConversationStream(Guid conversationId);

        // মেসেজের স্ট্যাটাস (Sent, Delivered, Read) আপডেট করার মেথড
        Task UpdateMessageStatusAsync(Guid messageId, MessageStatus status, CancellationToken ct = default);
    }
}
