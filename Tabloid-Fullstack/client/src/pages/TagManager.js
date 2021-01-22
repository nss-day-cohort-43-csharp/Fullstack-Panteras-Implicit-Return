// Authored by: Terra Roush

import React, { useEffect, useState, useContext } from "react";
import {
  ListGroup,
  ListGroupItem,
  Input,
  InputGroup,
  Button,
} from "reactstrap";
import Tag from "../components/Tag";
import { UserProfileContext } from "../providers/UserProfileProvider";

const TagManager = () => {
  const { getToken } = useContext(UserProfileContext);
  const [tags, setTags] = useState([]);
  const [newTag, setNewTag] = useState("");

  useEffect(() => {
    getTags();
  }, []);

  const getTags = () => {
    getToken().then((token) =>
      fetch(`/api/tag`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
        .then((res) => res.json())
        .then((tags) => {
          setTags(tags);
        })
    );
  };

  const saveNewTag = () => {
    const tagToAdd = { name: newTag };
    getToken().then((token) =>
      fetch("/api/tag", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(tagToAdd),
      }).then(() => {
        setNewTag("");
        getTags();
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
      <h1>Tag Management</h1>
      <div className="row justify-content-center">
        <div className="col-xs-12 col-sm-8 col-md-6">
          <ListGroup>
            {tags.map((tag) => (
              <ListGroupItem key={tag.id}>
                <Tag tag={tag} />
              </ListGroupItem>
            ))}
          </ListGroup>
          <div className="my-4">
            <InputGroup>
              <Input
                onChange={(e) => setNewTag(e.target.value)}
                value={newTag}
                placeholder="Add a new tag"
              />
              <Button onClick={saveNewTag}>Save</Button>
            </InputGroup>
          </div>
        </div>
      </div>
    </div>
  );
};

export default TagManager;
