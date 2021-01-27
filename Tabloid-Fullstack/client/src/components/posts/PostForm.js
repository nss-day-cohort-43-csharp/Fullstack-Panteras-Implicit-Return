// Authored by Sam Edwards
// PostForm for Create and Edit
import React, { useContext, useEffect, useState } from "react"
import { UserProfileContext } from "../../providers/UserProfileProvider"
import { toast } from "react-toastify";
import { useHistory, useParams } from "react-router-dom";

const PostForm = ({ editablePost }) => {

    const history = useHistory();
    const { postId } = useParams();
    const { getToken } = useContext(UserProfileContext)

    const [categories, setCategories] = useState([]);
    const [post, setPost] = useState("");
    const [loading, setLoading] = useState(true);

    const userId = +localStorage.getItem("userProfileId");
  
    useEffect(() => {
        if (editablePost) {
            // Attempting to get DatePicker to actually show date info
            // It doesn't like it even when I try to reformat it.
            if (editablePost.publishDateTime !== null)
            {
                const pubDate = editablePost.publishDateTime.split("T")[0]
                editablePost.publishDateTime = pubDate
            }
            setPost(editablePost)
        }
        getCategories()
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
            setCategories(categories)
            setLoading(false)
          })
      );
    };

    const addPost = submittedPost => {
        getToken().then(token => 
            fetch(`/api/post`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify(submittedPost)
            })
        )
        .then(res => {
            // Check the response status before converting to JSON, and display toast notifications
            if (res.status === 200) {
                toast.info(`Created ${post.title}!`)
                return res.json();
            } else {
                toast.error(`Error! Unable to submit post!`)
                return
            }
        })
        .then(post => {
            // Depending on if we have a response, push to the new post
            if (!post) {
                return
            } else {
                history.push(`/post/${post.id}`)
            }
        })
    }

    const handleControlledInputChange = e => {
        const newPost = { ...post }
        newPost[e.target.name] = e.target.value
        setPost(newPost)
    }

    const constructNewPost = (e) => {
       if (!post.categoryId) {
           toast.error("Error! Must select a Category!")
           return
       }

        if (editablePost !== undefined) {
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
            addPost({
                userProfileId: userId,
                title: post.title,
                content: post.content,
                categoryId: +post.categoryId,
                imageLocation: post.imageLocation,
                publishDateTime: post.publishDateTime,
                IsApproved: true
            })
        }
    }

    const createPost = (e) => {
        e.preventDefault()
        constructNewPost(e)
    }

    if (!categories) {
        return null
    }

    // MAX LENGTH REQUIRED FOR TITLE AND CONTENT
    return (
        <div className="container mt-5">   
            <h1>{!editablePost ? "Create" : "Edit"} Post</h1>
            <form onSubmit={createPost}>
                <fieldset>
                    <label htmlFor="postTitle">Title: </label>
                    <input
                    onChange={handleControlledInputChange}
                    maxLength="255"
                    id="postTitle"
                    name="title"
                    defaultValue={post.title}
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
                   maxLength="255"
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
                   defaultValue={post.imageLocation}
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
                    defaultalue={post.publishDateTime}
                    placeholder=""></input>
               </fieldset>
                <button type="submit" disabled={loading}>Submit</button>
                {!editablePost ? null : <button onClick={e => history.push(`/post/${postId}`)}>Cancel</button>}
            </form>
        </div>
    )
}

export default PostForm