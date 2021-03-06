// Authored by: Terra Roush

import React, { useState } from "react";
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

const Tag = ({ tag, saveUpdatedTag, deleteTag }) => {
  const [isEditing, setIsEditing] = useState(false);
  const [pendingDelete, setPendingDelete] = useState(false);
  const [tagEdits, setTagEdits] = useState("");

  const showEditForm = () => {
    setIsEditing(true);
    setTagEdits(tag.name);
  };
  const saveEditForm = () => {
    saveUpdatedTag(tagEdits, tag.id);
    setIsEditing(false);
  };

  const hideEditForm = () => {
    setIsEditing(false);
    setTagEdits("");
  };

  const handleDelete = () => {
    deleteTag(tag.id);
    setPendingDelete(false);
  };


  return (
    <div className="justify-content-between row">
      {isEditing ? (
        <Form className="w-100">
          <InputGroup>
            <Input
              size="sm"
              onChange={(e) => setTagEdits(e.target.value)}
              value={tagEdits}
            />
            <ButtonGroup size="sm">
              <Button onClick={saveEditForm}>Save</Button>
              <Button outline color="danger" onClick={hideEditForm}>
                Cancel
              </Button>
            </ButtonGroup>
          </InputGroup>
        </Form>
      ) : (
        <>
          <div className="p-1">{tag.name}</div>
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
        </>
      )}
      {/* DELETE CONFIRM MODAL */}
      <Modal isOpen={pendingDelete}>
        <ModalHeader>Delete {tag.name}?</ModalHeader>
        <ModalBody>
          Are you sure you want to delete this tag? This action cannot be
          undone.
        </ModalBody>
        <ModalFooter>
          <Button onClick={(e) => setPendingDelete(false)}>No, Cancel</Button>
          {/* need onclick event to fire off delete method */}
          <Button onClick={handleDelete} className="btn btn-outline-danger">Yes, Delete</Button>
        </ModalFooter>
      </Modal>
    </div>
  );
};

export default Tag;
