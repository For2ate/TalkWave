import { selectMessagesByChatId } from "Features/Chat/Model/Selector/MessageSelectors";
import { useAppDispatch, useAppSelector } from "Shared/Lib/hooks";
import { Message } from "./Message";
import { useEffect } from "react";
import { fetchMessages } from "Features/Chat/Model/Services/MessageThunks";
import { selectChatById } from "Features/Chat/Model/Selector/ChatSelectors";

interface Props {
  chatId: string;
}

export const Messages = ({ chatId }: Props) => {
  const messages = useAppSelector(selectMessagesByChatId(chatId));
  const dispatch = useAppDispatch();
  const chat = useAppSelector(selectChatById(chatId));

  useEffect(() => {
    const loadMessages = async () => {
      if (chat?.lastMessage) {
        dispatch(
          fetchMessages({
            chatId: chatId,
            messageId: chat.lastMessage.id,
            take: 50,
          })
        );
      }
    };

    if (chatId) {
      loadMessages();
    }
  }, [chat?.lastMessage?.id]);

  return (
    <div>
      {messages &&
        messages.map((message) => (
          <Message key={message.id} message={message} />
        ))}
    </div>
  );
};
