import { IconButton, SearchIcon, StyledInput } from "Shared/Ui";

import styles from "./Search.module.css";
import { useState } from "react";

export const Search = () => {
  const [searchValue, setSearchValue] = useState("");

  return (
    <div className={styles.layout}>
      <StyledInput
        label=""
        placeholder="search user"
        onChange={(e) => setSearchValue(e.target.value)}
        value={searchValue}
      />
      <IconButton size={35}>
        <SearchIcon color={"white"} />
      </IconButton>
    </div>
  );
};
