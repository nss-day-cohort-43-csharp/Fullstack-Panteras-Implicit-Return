import React, { useState } from "react";
import formatDate from "../utils/dateFormatter";
import {
    Button,
    ButtonGroup,
    Form,
    Input,
    InputGroup,
    Modal,
    ModalBody,
    ModalFooter,
    ModalHeader,
} from "reactstrap";
// import CommentCard from "./CommentCard";

const Comment = ({ comment }) => {
    const [isEditing, setIsEditing] = useState(false);
    const [pendingDelete, setPendingDelete] = useState(false);
    const [commentEdits, setCommentEdits] = useState("");

    const showEditForm = () => {
        setIsEditing(true);
        setCommentEdits(comment.subject);
    };

    const hideEditForm = () => {
        setIsEditing(false);
        setCommentEdits("");
    };

    if (!comment) return null;

    return (
        <div >
            {isEditing ? (
                <Form className="w-100">
                    <InputGroup>
                        <Input
                            size="sm"
                            onChange={(e) => setCommentEdits(e.target.value)}
                            value={commentEdits}
                        />
                        <ButtonGroup size="sm">
                            <Button onClick={showEditForm}>Save</Button>
                            <Button outline color="danger" onClick={hideEditForm}>
                                Cancel
                            </Button>
                        </ButtonGroup>
                    </InputGroup>
                </Form>
            ) : (
                    <>
                        {/* <CommentCard /> */}
                        <div className="row">
                            <div className="col-lg-10 text-center">
                                <div>
                                    <h4 className="text-center"> {comment.subject}</h4>
                                    <h6 className="text-left">{comment.content}</h6>
                                    <div className="ml-5">
                                        <h6>Published on: {formatDate(comment.createDateTime)}</h6>
                                    </div>
                                </div>
                            </div>
                            <div className="col-lg-4 col-sm-12 mt-2 py-3 text-center">
                                <div className="ml-5">
                                    <h6>Author: {comment.userProfile.displayName}</h6>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div className="text-right">
                            <ButtonGroup size="sm">
                                <Button className="btn btn-primary" onClick={showEditForm}>
                                    Edit
                                </Button>
                                <Button
                                    className="btn btn-danger"
                                    onClick={(e) => setPendingDelete(true)}
                                >
                                    Delete
                                </Button>
                            </ButtonGroup>
                        </div>
                    </>
                )}
            {/* DELETE CONFIRM MODAL */}
            <Modal isOpen={pendingDelete}>
                <ModalHeader>Delete {comment.subject}?</ModalHeader>
                <ModalBody>
                    Are you sure you want to delete this comment? This action cannot be
                    undone.
            </ModalBody>
                <ModalFooter>
                    <Button onClick={(e) => setPendingDelete(false)}>No, Cancel</Button>
                    <Button className="btn btn-outline-danger">Yes, Delete</Button>
                </ModalFooter>
            </Modal>
        </div>
    );
};

export default Comment;
