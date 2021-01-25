// Authored by Sam Edwards
// PostCreate renders Post Form
import React, { useContext, useEffect, useState } from "react"
import { UserProfileContext } from "../../providers/UserProfileProvider"

const PostCreate = ({ editablePost }) => {
    const { getToken } = useContext(UserProfileContext)
    const [categories, setCategories] = useState([]);
    const [post, setPost] = useState("");

    const userId = +localStorage.getItem("userProfileId");
  
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

    const handleControlledInputChange = e => {
        const newPost = { ...post }
        newPost[e.target.name] = e.target.value
        setPost(newPost)
    }

    const constructNewPost = (e) => {
        // Check to ensure category Id is not 0, if it is 0, show error toast message
        const newPost = {
            userId,
            title: post.title,
            content: post.content,
            categoryId: +post.categoryId,
            imageLocation: post.imageLocation,
            publishDateTime: post.publishDateTime
        }

        console.log("YOUR NEW POST: ", newPost)

        if (!editablePost) {
            // updatePost({
            //     id: editablePost.id,
            //     userId,
            //     title: post.title,
            //     content: post.content,
            //     categoryId: post.categoryId,
            //     imageLocation: post.imageLocation,
            //     publishDateTime: post.publishDateTime
            // })
        } else {
            // createPost({
            //     userId,
            //     title: post.title,
            //     content: post.content,
            //     categoryId: post.categoryId,
            //     imageLocation: post.imageLocation,
            //     publishDateTime: post.publishDateTime
            // })
        }
    }

    const createPost = (e) => {
        e.preventDefault()
        constructNewPost(e)
    }

    if (!categories) {
        return null
    }

    return (
        <div className="container mt-5">
            <h1>Create Post</h1>
            <form onSubmit={createPost}>
                <fieldset>
                    <label htmlFor="postTitle">Title: </label>
                    <input
                    onChange={handleControlledInputChange}
                    id="postTitle"
                    name="title"
                    value={post.name}
                    placeholder="Add Post Title"
                    required />
                </fieldset>
               <fieldset>
                   <label htmlFor="postContent">Content: </label>
                   <textarea
                   onChange={handleControlledInputChange}
                   id="postContent"
                   name="content"
                   value={post.content}
                   placeholder="Add Post Content"
                   rows={3}
                   cols={40}
                   required />
               </fieldset>
               <fieldset>
                    <label htmlFor="postCategories">Categories: </label>
                    <select
                    onChange={handleControlledInputChange}
                    id="postCategories"
                    name="categoryId"
                    value={post.categoryId}
                    required >
                        <option value="0">Choose a category</option>
                        {categories.map(c => (
                            <option value={c.id} key={c.id}>{c.name}</option>
                        ))}
                    </select>
               </fieldset>
               <fieldset>
                   <label html="postHeader">(Optional) Header Image URL: </label>
                   <input
                   onChange={handleControlledInputChange}
                   id="postHeader"
                   name="imageLocation"
                   value={post.imageLocation}
                   placeholder="Add image URL"
                   />
               </fieldset>
               <fieldset>
                   <label htmlFor="PublishDateTime">(Optional) Publication Date</label>
                    <input
                    onChange={handleControlledInputChange}
                    type="date"
                    id="postDate"
                    name="publishDateTime"
                    placeholder=""></input>
               </fieldset>
                <button type="submit">Submit</button>
            </form>
        </div>
    )
}

export default PostCreate