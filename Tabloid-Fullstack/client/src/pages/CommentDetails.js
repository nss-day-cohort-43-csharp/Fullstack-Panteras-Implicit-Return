// page not currently in use

import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { toast } from "react-toastify";
// import formatDate from "../utils/dateFormatter";
import "./PostDetails.css";

const CommentDetails = () => {
    const { postId } = useParams();
    const [comment, setComment] = useState();

    useEffect(() => {
        fetch(`/api/comment/${postId}`)
            .then((res) => {
                if (res.status === 404) {
                    toast.error("Comment unavailable");
                    return;
                }
                return res.json();
            })
            .then((data) => {
                setComment(data);
                console.log(data);
            });
    }, [postId]);
    // console.log(comment);
    if (!comment) return null;

    const getComments = () => {
        getToken().then((token) =>
            fetch(`/api/comment`, {
                method: "GET",
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            })
                .then((res) => res.json())
                .then((comments) => {
                    setComments(comments);
                })
        );
    }
};

const saveNewComment = () => {
    const commentToAdd = { name: newComment };
    getToken().then((token) =>
        fetch("/api/comment", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify(commentToAdd),
        }).then(() => {
            setNewCategory("");
            getCategories();
        })
    );

    return (
        <div>
            <div className="container">

                <h1>{comment[0].subject}</h1>
                {/* <h5 className="text-danger">{comment.content}</h5> */}
                <div className="row">

                    {/* <p className="d-inline-block">{post.userProfile.displayName}</p> */}

                    <div className="col">
                        {/* <p>{formatDate(comment.CreateDateTime)}</p> */}
                    </div>
                </div>

                <div className="my-4">
                    <InputGroup>
                        <Input
                            onChange={(e) => setNewComment(e.target.value)}
                            value={newComment}
                            placeholder="Add a new comment"
                        />
                        <Button onClick={saveNewComment}>Save</Button>
                    </InputGroup>
                </div>
            </div>
        </div>
    );
};

export default CommentDetails;