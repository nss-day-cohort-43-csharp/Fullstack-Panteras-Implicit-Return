// Authored by Sam Edwards
// Post Edit display post form for edit
import React, { useEffect, useState } from "react"
import { useParams } from "react-router-dom";
import PostForm from "../../components/posts/PostForm"


const PostEdit = () => {

    const [ post, setPost ] = useState()



    useEffect(() => {
        // Get Current Post
    }, [])

    return (
    <div className="container mt-5">
        <PostForm editablePost={post} />
    </div>
    )
}


export default PostEdit