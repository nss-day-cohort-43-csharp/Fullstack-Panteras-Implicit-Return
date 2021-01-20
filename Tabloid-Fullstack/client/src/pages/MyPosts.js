// Authored by Sam Edwards
// MyPosts displays all posts by the current UserId
import React, { useState, useEffect } from "react"
import { toast } from "react-toastify"


const MyPosts = () => {

    const userId = localStorage.getItem("userProfileId");
    const [myPosts, setMyPosts] = useState([]);

    useEffect(() => {
        debugger
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
    }, [])


    return (
        null   
    )
}

export default MyPosts