import React from "react";
import CommentSummaryCard from "./CommentSummaryCard";

const CommentList = ({ comments }) => {
    return (
        <div>
            {comments.map((comment) => (
                <div className="m-4" key={comment.id}>
                    <CommentSummaryCard comment={comment} />
                </div>
            ))}
        </div>
    );
};

export default CommentList;