import React, { ReactNode } from "react";
import styles from "./StyledButton.module.css";

interface Props extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  children: string | ReactNode;
  onClick: () => void;
}

export const StyledButton = ({ children, onClick, ...rest }: Props) => {
  return (
    <button onClick={onClick} className={styles.button} {...rest}>
      {children}
    </button>
  );
};
