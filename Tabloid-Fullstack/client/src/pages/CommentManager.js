import React, { useEffect, useState, useContext } from "react";
import { useHistory, useParams } from "react-router-dom";
import {
    ListGroup,
    ListGroupItem,
    Input,
    InputGroup,
    Button,
} from "reactstrap";
import Comment from "../components/Comment";
import { UserProfileContext } from "../providers/UserProfileProvider";


const CommentManager = () => {
    const { getToken } = useContext(UserProfileContext);
    const [comments, setComments] = useState([]);
    const [newComment, setNewComment] = useState("");

    const [subject, setSubject] = useState([]);
    const [newSubject, setNewSubject] = useState("");

    const [content, setContent] = useState([]);
    const [newContent, setNewContent] = useState("");

    const { postId } = useParams();

    useEffect(() => {
        getComments();

    }, []);

    const getComments = () => {
        getToken().then((token) =>
            fetch(`/api/comment/${postId}`, {
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
    };

    const saveNewComment = () => {
        console.log(postId);
        const commentToAdd = {
            subject: newSubject,
            content: newContent,
            postId: postId


        };
        getToken().then((token) =>
            fetch("/api/comment", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify(commentToAdd),
            }).then(() => {
                setNewComment("");
                getComments();
            })
        );
    };

    return (
        <div className="container mt-5">
            <h1>Comments</h1>
            <div className="row justify-content-center">
                <div >

                    <div className="my-4">
                        <InputGroup>
                            <div className="col-lg-10 text-center">
                                <Input
                                    onChange={(e) => setNewSubject(e.target.value)}
                                    value={newSubject}
                                    placeholder="Subject"
                                />
                            </div>
                            <br />
                            <div className="col-lg-10 text-center">
                                <Input
                                    onChange={(e) => setNewContent(e.target.value)}
                                    value={newContent}
                                    placeholder="Content"
                                />
                            </div>
                            <div className="text-center">
                                <Button onClick={saveNewComment}>Save Comment</Button>
                            </div>
                        </InputGroup>
                    </div>
                    <div>
                        <ListGroup>
                            {comments.map((comment) => (
                                <ListGroupItem key={comment.id}>
                                    <Comment comment={comment} />
                                </ListGroupItem>
                            ))}
                        </ListGroup>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default CommentManager;
