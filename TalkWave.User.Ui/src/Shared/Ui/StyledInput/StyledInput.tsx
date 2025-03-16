import React from "react";
import styles from "./StyledInput.module.css";

interface Props extends React.InputHTMLAttributes<HTMLInputElement> {
  label: string;
  value: string;
  onChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

export const StyledInput = (props: Props) => {
  const { label, value, onChange, ...rest } = props;

  return (
    <div>
      <label className={styles.label}>{label}</label>
      <input
        type={rest.type || "text"}
        value={value}
        onChange={onChange}
        className={styles.input}
        {...rest}
      />
    </div>
  );
};
