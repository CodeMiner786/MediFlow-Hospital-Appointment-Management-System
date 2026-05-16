using HealthcareHospitalManagement.Domain.Entities.Chat;
using HealthcareHospitalManagement.Domain.Enums.Chat;
using HealthcareHospitalManagement.Domain.Interfaces.Chat;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Chat
{
    public class ChatMessageRepository(ApplicationDbContext context)
        : GenericRepository<ChatMessage>(context), IChatMessageRepository
    {
        private readonly DbSet<ChatMessage> _dbSet = context.Set<ChatMessage>();

        public IAsyncEnumerable<ChatMessage> GetMessagesByConversationIdStream(Guid conversationId)
        {
            return _dbSet
                .Where(m => m.ConversationId == conversationId && !m.IsDeleted)
                .OrderBy(m => m.CreatedAt)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<ChatMessage> GetMessagesBySenderIdStream(Guid senderId)
        {
            return _dbSet
                .Where(m => m.SenderId == senderId && !m.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<ChatMessage> GetUnreadMessagesByConversationStream(Guid conversationId)
        {
            return _dbSet
                .Where(m => m.ConversationId == conversationId
                         && m.Status != MessageStatus.Read
                         && !m.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        public async Task UpdateMessageStatusAsync(Guid messageId, MessageStatus status, CancellationToken ct = default)
        {
            var message = await GetByIdAsync(messageId, ct);
            if (message != null)
            {
                message.Status = status;

                if (status == MessageStatus.Delivered) message.DeliveredAt = DateTime.UtcNow;
                if (status == MessageStatus.Read) message.ReadAt = DateTime.UtcNow;

                // 🔹 এখন CancellationToken pass করা হচ্ছে
                await UpdateAsync(message, ct);
                await context.SaveChangesAsync(ct);
            }
        }
    }
}
