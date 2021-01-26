import React, { useEffect, useState, useContext } from "react";
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
    const [categories, setCategories] = useState([]);
    const [newComment, setNewComment] = useState("");

    useEffect(() => {
        getCategories();

    }, []);

    const getCategories = () => {
        getToken().then((token) =>
            fetch(`/api/comment`, {
                method: "GET",
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            })
                .then((res) => res.json())
                .then((categories) => {
                    setCategories(categories);
                })
        );
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
                setNewComment("");
                getCategories();
            })
        );
    };

    return (
        <div className="container mt-5">
            <img
                height="100"
                src="/quill.png"
                alt="Quill Logo"
                className="bg-danger rounded-circle"
            />
            <h1>Comment Management</h1>
            <div className="row justify-content-center">
                <div className="col-xs-12 col-sm-8 col-md-6">
                    <ListGroup>
                        {categories.map((comment) => (
                            <ListGroupItem key={comment.id}>
                                <Comment comment={comment} />
                            </ListGroupItem>
                        ))}
                    </ListGroup>
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
        </div>
    );
};

export default CommentManager;
