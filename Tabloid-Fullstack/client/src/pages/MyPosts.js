// Authored by Sam Edwards
// MyPosts displays all posts by the current UserId
import React, { useState, useEffect } from "react"
import { toast } from "react-toastify"
import PostList from "../components/posts/PostList"


const MyPosts = () => {

    const userId = localStorage.getItem("userProfileId");
    const [myPosts, setMyPosts] = useState([]);

    useEffect(() => {
        if (userId !== null) {
            fetch(`/api/post/getbyuserid/${userId}`)
                .then(res => {
                    if (res.status === 404) {
                        toast.error("Couldn't get your posts")
                        return
                    }
                    return res.json();
                })
                .then(data => {
                    setMyPosts(data)
                })
        }
    }, [])

    if (!myPosts) {
        return null
    }

    return ( 
        <>
        <h1>My Posts</h1>
        <PostList posts={myPosts} />
        </>
    )
}

export default MyPosts