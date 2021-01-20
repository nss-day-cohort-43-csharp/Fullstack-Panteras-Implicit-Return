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
                        <Link to={`/comment${comment.id}`}>
                            <h2>{comment.subject}</h2>
                        </Link>
                        <strong className="text-danger">{comment.content}</strong>
                    </div>
                    <p className="text-justify mx-5">{comment.previewText}</p>
                </div>
                <div className="col-lg-4 col-sm-12 mt-2 py-3 text-left">
                    <p className="ml-5">
                        Published on {formatDate(comment.CreatedDateTime)}
                    </p>
                </div>
            </div>
        </Card>
    );
};

export default CommentSummaryCard;
