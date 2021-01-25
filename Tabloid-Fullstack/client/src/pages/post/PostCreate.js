// Authored by Sam Edwards
// PostCreate renders Post Form
import React, { useContext, useEffect, useState } from "react"
import { UserProfileContext } from "../../providers/UserProfileProvider"

const PostCreate = () => {
    const { getToken } = useContext(UserProfileContext)
    const [categories, setCategories] = useState([]);
    const [newPost, setNewPost] = useState("");
  
    useEffect(() => {
      getCategories();
    }, []);
  
    const getCategories = () => {
      getToken().then((token) =>
        fetch(`/api/category`, {
          method: "GET",
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
          .then((res) => res.json())
          .then((categories) => {
            setCategories(categories);
          })
      );
    };

    return (
        <>
        <div>POST FORM</div>
        <ul>
            <li>Title</li>
            <li>Content</li>
            <li>CatgegoryId</li>
            <li>Header Image URL OPTIONAL</li>
            <li>Publication Date OPTIONAL</li>
        </ul>
        </>
    )
}

export default PostCreate