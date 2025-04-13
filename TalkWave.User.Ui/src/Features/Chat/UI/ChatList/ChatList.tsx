import { useAppDispatch, useAppSelector } from "Shared/Lib";
import { ChatListItem } from "./ChatListItem";
import styles from "./ChatsList.module.css";
import { useEffect } from "react";
import { fetchChats, selectAllChats } from "Features/Chat/Model";

export const ChatList = () => {
  const dispatch = useAppDispatch();
  const chats = useAppSelector(selectAllChats);

  useEffect(() => {
    dispatch(fetchChats());
  }, [dispatch]);

  return (
    <div className={styles.layout}>
      <main>
        {chats?.map((chat) => (
          <ChatListItem key={chat.id} chat={chat} />
        ))}
      </main>
    </div>
  );
};
