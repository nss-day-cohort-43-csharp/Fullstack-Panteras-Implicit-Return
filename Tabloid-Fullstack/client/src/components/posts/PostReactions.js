import React from "react";
import { Badge } from "reactstrap";
import "./PostReaction.css";

const PostReactions = ({ postReactions }) => {
  return (
    <div className="float-left">
      {postReactions.map((postReaction) => (
        <div key={postReaction.reaction.id} className="d-inline-block mx-2">
          <Badge
            pill
            className="p-2 border border-dark post-reaction__pill"
            title={postReaction.reaction.name}
          >
            {postReaction.reaction.emoji} {postReaction.count}
          </Badge>
        </div>
      ))}
    </div>
  );
};

export default PostReactions;
