import React from "react";
import PostSummaryCard from "./PostSummaryCard";

const PostList = ({ posts }) => {
  return (
    <div>
      {posts.map((post) => (
        <div className="m-4" key={post.id}>
          <PostSummaryCard post={post} />
        </div>
      ))}
    </div>
  );
};

export default PostList;
