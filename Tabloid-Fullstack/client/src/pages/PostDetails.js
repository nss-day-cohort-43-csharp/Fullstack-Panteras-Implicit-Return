import React, { useEffect, useState, useContext } from "react";
import {
  Button,
  ButtonGroup,
  Modal,
  ModalBody,
  ModalFooter,
  ModalHeader,
} from "reactstrap";
import { useParams, useHistory } from "react-router-dom";
import { toast } from "react-toastify";
import { Container, Jumbotron } from "reactstrap";
import CommentCard from "../components/CommentCard";
import PostReactions from "../components/posts/PostReactions";
import formatDate from "../utils/dateFormatter";
import { UserProfileContext } from '../providers/UserProfileProvider';
import "./PostDetails.css";
import CommentManager from "./CommentManager";

const PostDetails = () => {
  const { postId } = useParams();

  const [post, setPost] = useState();
  const [reactionCounts, setReactionCounts] = useState([]);
  const [comment, setComment] = useState();
  const [pendingDelete, setPendingDelete] = useState(false);

  const { getToken, isAdmin } = useContext(UserProfileContext);

  const history = useHistory();
  const userId = +localStorage.getItem("userProfileId");

  useEffect(() => {
    return getToken().then((token) =>
      fetch(`/api/post/${postId}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`
        }
      })
        .then((res) => {
          if (res.status === 404) {
            toast.error("This isn't the post you're looking for");
            history.push("/explore")
            return;
          }
          return res.json();
        })
        .then((data) => {
          if (data) {
            setPost(data ? data.post : null);
            setReactionCounts(data.reactionCounts);
          }
        }));
  }, [postId]);

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
      });
  }, [postId]);

  if (!post) return null;
  if (!comment) return null;

  return (
    <>
    <div>
      <Jumbotron
        className="post-details__jumbo"
        style={{ backgroundImage: `url('${post.imageLocation}')` }}
      ></Jumbotron>
      <div className="container">
        <h1>{post.title}</h1>
        <h5 className="text-danger">{post.category.name}</h5>
        <div className="row">
          <div className="col">
            <img
              src={post.userProfile.imageLocation}
              alt={post.userProfile.displayName}
              className="post-details__avatar rounded-circle"
            />
            <p className="d-inline-block">{post.userProfile.displayName}</p>
          </div>
          <div className="col">
            <p>{formatDate(post.publishDateTime)}</p>
          </div>

          {
            // If I'm an Admin or it's my post, show me edit/delete options
            !isAdmin() && post.userProfileId !== userId ? null : 
              <ButtonGroup size="sm">
                <Button className="btn btn-primary" onClick={e => history.push(`/post/edit/${postId}`)}>
                  Edit
                </Button>
                <Button
                  className="btn btn-danger"
                  onClick={e => setPendingDelete(true)}
                >
                  Delete
                </Button>
              </ButtonGroup>
          }

        </div>
        <div className="text-justify post-details__content">{post.content}</div>
        <div className="my-4">
          <PostReactions postReactions={reactionCounts} />
        </div>
      </div>

      {/* ***********Comments******** */}
      <div>
      <Container>
          <CommentManager />
        </Container>
      </div>
      {/* ***********END Comments******** */}
    </div>

    <Modal isOpen={pendingDelete}>
      <ModalHeader>Delete {post.name}?</ModalHeader>
      <ModalBody>
        Are you sure you want to delete this post? This action cannot be undone.
      </ModalBody>
      <ModalFooter>
        <Button onClick={(e) => setPendingDelete(false)}>No, Cancel</Button>
        {/* need onclick event to fire off delete method */}
        <Button onClick={e => {
          getToken().then(token => 
            fetch(`/api/post/${post.id}`, {
              method: "DELETE",
              headers: {
                Authorization: `Bearer ${token}`,
              },
            }).then(res => {
              if (res.status === 204) {
                toast.info("Post deleted!")
                history.push("/explore")
              } else {
                toast.error("Error! Unable to delete post!")
              }
            }))
        }}
        className="btn btn-outline-danger">Yes, Delete</Button>
      </ModalFooter>
    </Modal>
    </>
  );
};

export default PostDetails;
