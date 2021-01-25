// Authored by: Terra Roush

import React from "react";
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
