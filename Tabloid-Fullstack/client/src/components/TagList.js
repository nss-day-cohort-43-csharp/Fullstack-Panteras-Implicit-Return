// Authored by: Terra Roush

import React from "react";
import { TagCard } from "./TagCard";

const TagList = ({ tags }) => {
  return (
    <div>
      {tags.map((tag) => (
        <div className="m-4" key={tag.id}>
          <TagCard tag={tag} />
        </div>
      ))}
    </div>
  );
};

export default TagList;
