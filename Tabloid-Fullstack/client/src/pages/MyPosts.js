// Authored by Sam Edwards
// MyPosts displays all posts by the current UserId
import React, { useState, useEffect, useContext } from "react"
import { toast } from "react-toastify"
import PostList from "../components/posts/PostList"
import { UserProfileContext } from "../providers/UserProfileProvider"

const MyPosts = () => {

    const { getToken } = useContext(UserProfileContext)

    const userId = localStorage.getItem("userProfileId");
    const [myPosts, setMyPosts] = useState([]);

    useEffect(() => {
        if (userId !== null) {
            getToken().then((token) => 
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
            
            )
        }
    }, [])

    if (!myPosts) {
        return null
    }

    return ( 
        <>
        <h1>My Posts</h1>
        { myPosts.length > 0 ? <PostList posts={myPosts} /> : <p>You don't have any posts yet!</p> }
        </>
    )
}

export default MyPosts