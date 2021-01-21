import React, { useEffect, useState, useContext } from "react";
import { useParams, useHistory } from "react-router-dom";
import { toast } from "react-toastify";
import { Container, Jumbotron } from "reactstrap";
import CommentSummaryCard from "../components/CommentSummaryCard";
import PostReactions from "../components/PostReactions";
import formatDate from "../utils/dateFormatter";
import "./PostDetails.css";
import { UserProfileContext } from '../providers/UserProfileProvider';

const PostDetails = () => {
  const { postId } = useParams();
  const [post, setPost] = useState();
  const [reactionCounts, setReactionCounts] = useState([]);
  const [comment, setComment] = useState();
  const { getToken } = useContext(UserProfileContext);
  const history = useHistory();

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
        </div>
        <div className="text-justify post-details__content">{post.content}</div>
        <div className="my-4">
          <PostReactions postReactions={reactionCounts} />
        </div>
      </div>

      <div>
        <h4>Comments</h4>
        <Container>
          {comment.map((c) => {
            return <div key={c.id}><CommentSummaryCard comment={c} /></div>
          })
          }

        </Container>

      </div>
    </div>

  );
};

export default PostDetails;
