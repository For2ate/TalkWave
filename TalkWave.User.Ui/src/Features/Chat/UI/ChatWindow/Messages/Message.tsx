import { MessageModel } from "Features/Chat/Model/Types/Message";

interface Props {
  message: MessageModel;
}

export const Message = ({ message }: Props) => {
  return <div>{message.content}</div>;
};
