// Authored by Sam Edwards
// MyPosts displays all posts by the current UserId
import React, { useState, useEffect } from "react"


const MyPosts = () => {

    const userId = localStorage.getItem("userProfile").id;
    const [myPosts, setMyPosts] = useState([]);

    useEffect(() => {
        fetch(`/api/post`)
    })


    return (
        null   
    )
}

export default MyPosts