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
                        <h4> {comment.subject}</h4>
                        {/* </Link> */}
                        <h6>{comment.content}</h6>
                    </div>
                </div>
                <div className="col-lg-4 col-sm-12 mt-2 py-3 text-left">
                    <div className="ml-5">
                        <h6>Published on: {formatDate(comment.createDateTime)}</h6>
                    </div>
                    <div className="ml-5">
                        <h6>Author: {comment.userProfile.displayName}</h6>
                    </div>
                </div>
            </div>
        </Card>
    );
};

export default CommentSummaryCard;
