import { ChatListItem } from "./ChatListItem";
import styles from "./ChatsList.module.css";
import { useAppDispatch, useAppSelector } from "Shared/Lib/hooks";
import { selectAllChats } from "Features/Chat/Model/Selector/ChatSelectors";
import { useEffect } from "react";
import { fetchChats } from "Features/Chat/Model/Services/FetchChats";
import { useChatHub } from "Features/Chat/Model/Hooks/UseChatHub";

export const ChatList = () => {
  const dispatch = useAppDispatch();
  const chats = useAppSelector(selectAllChats);

  const hub = useChatHub();

  useEffect(() => {
    dispatch(fetchChats());
  }, [dispatch]);

  useEffect(() => {
    if (!hub) {
      return;
    }

    console.log();

    hub.invoke("JoinHub", localStorage.getItem("userId"));
  }, [hub]);

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
