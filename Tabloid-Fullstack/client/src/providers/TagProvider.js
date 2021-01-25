import React, { useState, createContext } from "react";

export const TagContext = createContext();

export const TagProvider = (props) => {
    const [tags, setTags] = useState([]);

    const getAllTags = () => {
        return fetch("/api/tag")
        .then((res) => res.json())
        .then(setTags);
    }

    const getTags = (id) => {
        return fetch(`/api/tag/${id}`).then(res => res.json());
    }

    const addPost = post => {
        const token = localStorage.getItem("token");

        return fetch("/api/comment", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify(post),
        });
    };
    
      return (
        <PostContext.Provider
          value={{
            posts,
            getAllPosts,
            addPost,
            getPost,
            searchPosts,
          }}
        >
          {props.children}
        </PostContext.Provider>
      );


}