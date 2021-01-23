import React, { useEffect, useState, useContext } from "react";
import { UserProfileContext } from "../providers/UserProfileProvider"
import PostList from "../components/posts/PostList";

const Explore = () => {

  const [posts, setPosts] = useState([]);
  const { getToken } = useContext(UserProfileContext);

  useEffect(() => {
    return getToken().then(token =>
      fetch("/api/post", {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`
        }
      })
      .then(res => {
        if (res.status === 401) {
          // Maybe return a message if an error occurs?
        }
        return res.json()
      })
      .then(posts => setPosts(posts))
  )}, []);

  return (
    <div className="row">
      <div className="col-lg-2 col-xs-12"></div>
      <div className="col-lg-10 col-xs-12">
        <PostList posts={posts} />
      </div>
    </div>
  );
};

export default Explore;
