import React from "react";
import { Link } from "react-router-dom";
import { Card } from "reactstrap";
import "./PostSummaryCard.css";

const TagCard = ({ tag }) => {
  return (
    <Card className="post-summary__card">
      <div className="row">
        <div className="col-lg-3 col-sm-12">
          
        </div>
        <div className="col-lg-5 col-sm-12 py-3">
          <div>
            <strong className="text-danger">{tag.name}</strong>
          </div>
          </div>
      </div>
    </Card>
  );
};

export default TagCard;
