﻿using TalkWave.Common.Message;

namespace TalkWave.Chat.Models.Messages.Request {

    public class UpdateMessageRequestModel {

        public Guid Id { get; set; }

        public string Content { get; set; }

        public MessageStatus Status { get; set; }

    }

}
