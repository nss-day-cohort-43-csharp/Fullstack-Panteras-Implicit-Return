// Authored by Sam Edwards
// Post Edit display post form for edit
// Gets Current Post object and passes it into form
import React, { useEffect, useState, useContext } from "react"
import { useParams } from "react-router-dom";
import PostForm from "../../components/posts/PostForm"
import { UserProfileContext } from '../../providers/UserProfileProvider';


const PostEdit = () => {

    const { postId } = useParams();
    const [ post, setPost ] = useState()

    const { getToken } = useContext(UserProfileContext);

    useEffect(() => {
        return getToken().then(token =>
        fetch(`/api/post/${postId}`, {
          method: "GET",
          headers: {
            Authorization: `Bearer ${token}`
          }
        })
        .then(res => res.json())
        .then(setPost))
    }, [])

    if (!post) {
        return null
    }

    return (
    <div className="container mt-5">
        <PostForm editablePost={post.post} />
    </div>
    )
}


export default PostEdit