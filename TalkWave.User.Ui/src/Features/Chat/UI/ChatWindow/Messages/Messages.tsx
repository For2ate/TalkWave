import { useAppDispatch, useAppSelector, useChatHub } from "Shared/Lib";
import styles from "./Messages.module.css";
import {
  addNewMessage,
  loadMessages,
  selectCurrentChat,
  selectMessagesByChatId,
  setSelectedChat,
  updateLastMessage,
} from "Features/Chat/Model";
import { Message } from "./Message";
import { useEffect, useRef } from "react";
import { MessageModel } from "Entities/Messages/MessageTypes";
import { selectHasMoreByChatId } from "Features/Chat/Model";

interface Props {
  chatId: string;
}

export const Messages = ({ chatId }: Props) => {
  const dispatch = useAppDispatch();
  const messages = useAppSelector(selectMessagesByChatId(chatId));
  const chat = useAppSelector(selectCurrentChat);
  const hasMore = useAppSelector(selectHasMoreByChatId(chatId));
  const currentUserId = localStorage["userId"];

  // Scroll to the last message
  // When?
  // 1) You send message
  // 2) First load
  const endMessagesRef = useRef<HTMLDivElement>(null);
  const scrollToBottom = () => {
    endMessagesRef.current?.scrollIntoView({ behavior: "auto" });
  };

  // Initial fetch messages when open chat
  useEffect(() => {
    const initMessages = async () => {
      await dispatch(
        loadMessages({
          chatId: chatId,
          take: 20,
          direction: "after",
          messageId: chat?.lastMessage?.id,
        })
      );
      scrollToBottom();
    };

    const initSelectedChat = async () => {
      await dispatch(setSelectedChat(chatId));
    };

    // If it chat not selected
    if (chatId && !chat) {
      initSelectedChat();
    }

    if (chat?.lastMessage && !messages) {
      initMessages();
    }
  }, [chat, chatId]);

  // When scroll add messages for top
  const messagesContainerRef = useRef<HTMLDivElement>(null);
  const handleScroll = async () => {
    if (!messagesContainerRef.current || !hasMore) return;

    const container = messagesContainerRef.current;
    const scrollTop = container.scrollTop;
    const scrollHeight = container.scrollHeight;
    const clientHeight = container.clientHeight;

    if (scrollTop < 100 && scrollHeight > clientHeight) {
      await dispatch(
        loadMessages({
          chatId: chatId,
          messageId: messages[0].id,
          take: 20,
          direction: "before",
        })
      );
    }
  };

  const hub = useChatHub();

  useEffect(() => {
    const RecieveMessage = async (message: MessageModel) => {
      await dispatch(addNewMessage(message));
      await dispatch(updateLastMessage({ chatId: chatId, message: message }));
      if (message.senderId === currentUserId) {
        scrollToBottom();
      }
    };

    hub?.on("ReceiveMessage", RecieveMessage);
  }, [hub]);

  return (
    <div
      className={styles.layout}
      ref={messagesContainerRef}
      onScroll={handleScroll}
    >
      {messages?.map((message) => (
        <Message
          key={message.id}
          message={message}
          isCurrentUser={message.senderId === currentUserId}
        />
      ))}
      <div ref={endMessagesRef} />
    </div>
  );
};
