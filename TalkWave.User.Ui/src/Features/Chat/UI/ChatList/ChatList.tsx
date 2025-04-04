import { ChatListItem } from "./ChatListItem";
import styles from "./ChatsList.module.css";
import { useAppDispatch, useAppSelector } from "Shared/Lib/hooks";
import { selectAllChats } from "Features/Chat/Model/Selector/ChatSelectors";
import { useEffect } from "react";
import { fetchChats } from "Features/Chat/Model/Services/FetchChats";

export const ChatList = () => {
  const dispatch = useAppDispatch();
  const chats = useAppSelector(selectAllChats);

  useEffect(() => {
    dispatch(fetchChats());
  }, [dispatch]);

  return (
    <div className={styles.layout}>
      <main>
        {chats.map((chat) => (
          <ChatListItem key={chat.id} chat={chat} />
        ))}
      </main>
    </div>
  );
};
