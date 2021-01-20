import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { toast } from "react-toastify";
import formatDate from "../utils/dateFormatter";
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
                setComment(data.comment);
            });
    }, [postId]);

    if (!comment) return null;

    return (
        <div>
            <div className="container">
                <h1>{comment.subject}</h1>
                <h5 className="text-danger">{comment.content}</h5>
                <div className="row">

                    {/* <p className="d-inline-block">{post.userProfile.displayName}</p> */}

                    <div className="col">
                        <p>{formatDate(comment.CreatedDateTime)}</p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default CommentDetails;