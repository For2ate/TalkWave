import { selectMessagesByChatId } from "Features/Chat/Model/Selector/MessageSelectors";
import { useAppDispatch, useAppSelector } from "Shared/Lib/hooks";
import { Message } from "./Message";
import { useCallback, useEffect, useRef, useState } from "react";
import { fetchMessages } from "Features/Chat/Model/Services/MessageThunks";
import { selectChatById } from "Features/Chat/Model/Selector/ChatSelectors";
import { useChatHub } from "Features/Chat/Model/Hooks/UseChatHub";
import { MessageModel } from "Features/Chat/Model/Types/Message";
import { addNewMessage } from "Features/Chat/Model/Slice/MessagesSlice";

import styles from "./Messages.module.css";

interface Props {
  chatId: string;
}

export const Messages = ({ chatId }: Props) => {
  const [itFirstRender, setItFirstRender] = useState(true);
  const messages = useAppSelector(selectMessagesByChatId(chatId));
  const dispatch = useAppDispatch();
  const chat = useAppSelector(selectChatById(chatId));
  const currentUserId = localStorage["userId"];
  const messagesEndRef = useRef<HTMLDivElement>(null);
  const hub = useChatHub();
  const [hasMore, setHasMore] = useState(true);
  const [isLoading, setIsLoading] = useState(false);
  const messagesContainerRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (itFirstRender && messages && messages.length) {
      scrollToBottom();
      setItFirstRender(false);
    }
  }, [messages]);

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "auto" });
  };

  const loadMessages = async (messageId: string, excludeLast: boolean) => {
    const result = await dispatch(
      fetchMessages({
        chatId: chatId,
        messageId: messageId,
        take: 20,
        excludeLast: excludeLast,
      })
    ).unwrap();
    return result;
  };

  useEffect(() => {
    if (chatId && chat?.lastMessage) {
      loadMessages(chat.lastMessage.id, false);
    }
  }, [chat?.lastMessage?.id]);

  useEffect(() => {
    const RecieveMessage = async (message: MessageModel) => {
      await dispatch(addNewMessage(message));
      if (message.senderId === currentUserId) {
        scrollToBottom();
      }
    };

    hub?.on("ReceiveMessage", RecieveMessage);
  }, [hub]);

  const loadMoreMessages = useCallback(async () => {
    if (isLoading || !hasMore || !messages || messages.length === 0) return;

    const firstMessageId = messages[0].id;
    setIsLoading(true);

    try {
      const result = await loadMessages(firstMessageId, true);

      if (!result || result.messages.length < 5) {
        setHasMore(false);
      }
    } finally {
      setIsLoading(false);
    }
  }, [messages, isLoading, hasMore, chatId, dispatch]);

  const handleScroll = useCallback(() => {
    if (!messagesContainerRef.current || isLoading || !hasMore) return;

    const container = messagesContainerRef.current;
    const scrollTop = container.scrollTop;
    const scrollHeight = container.scrollHeight;
    const clientHeight = container.clientHeight;

    // Если пользователь прокрутил близко к верху
    if (scrollTop < 100 && scrollHeight > clientHeight) {
      loadMoreMessages();
    }
  }, [loadMoreMessages, isLoading, hasMore]);

  return (
    <div
      className={styles.layout}
      ref={messagesContainerRef}
      onScroll={handleScroll}
    >
      {messages &&
        messages.map((message) => (
          <Message
            key={message.id}
            message={message}
            isCurrentUser={message.senderId === currentUserId}
          />
        ))}
      <div ref={messagesEndRef} /> {/* Невидимый элемент для прокрутки */}
    </div>
  );
};
