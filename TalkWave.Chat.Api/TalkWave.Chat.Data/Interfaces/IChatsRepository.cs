﻿using TalkWave.Chat.Data.Entities;

namespace TalkWave.Chat.Data.Interfaces {

    public interface IChatsRepository : IBaseRepository<ChatEntity> {

        Task<ChatEntity?> GetChatWithMembersAsync(Guid chatId);


    }

}
