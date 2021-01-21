import React from "react";
import { Link } from "react-router-dom";
import { Card } from "reactstrap";
import formatDate from "../utils/dateFormatter";
import "./PostSummaryCard.css";

const CommentSummaryCard = ({ comment }) => {
    return (
        <Card className="post-summary__card">
            <div className="row">
                <div className="col-lg-5 col-sm-12 py-3">
                    <div>
                        {/* <Link to={`/comment/${comment.Id}`}> */}
                        <h2>{comment.subject}</h2>
                        {/* </Link> */}
                        <strong className="text-danger">{comment.content}</strong>
                    </div>
                </div>
                <div className="col-lg-4 col-sm-12 mt-2 py-3 text-left">
                    <p className="ml-5">
                        Published on: {formatDate(comment.createDateTime)}
                    </p>
                    <p className="ml-5">
                        User Profile: {comment.userProfile.displayName}
                    </p>
                </div>
            </div>
        </Card>
    );
};

export default CommentSummaryCard;
