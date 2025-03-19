import React from "react";
import styles from "./IconButton.module.css";

interface Props {
  children?: React.ReactNode;
  onClick?: () => void;
  size?: number;
}

export const IconButton = ({ onClick, size, children }: Props) => {
  return (
    <button
      onClick={onClick}
      className={styles.iconButton}
      style={{ width: size, height: size }}
    >
      {children}
    </button>
  );
};
