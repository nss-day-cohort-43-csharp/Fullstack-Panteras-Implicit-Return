import React, { useEffect, useState, useContext } from "react";
import { useParams } from "react-router-dom";
import { toast } from "react-toastify";
import { UserProfileContext } from '../providers/UserProfileProvider';

const TagSelect = () => {
  const { postId } = useParams();
  const [tags, setTags] = useState([]);
  const [postTag, setPostTag] = useState();
  const { getToken } = useContext(UserProfileContext);

  const addPostTag = submittedPostTag => {
    getToken().then(token => 
        fetch(`/api/posttag`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify(submittedPostTag)
        })
    )
    .then(res => {
        // Check the response status before converting to JSON, and display toast notifications
        if (res.status === 200) {
            toast.info("You've successfully added a tag to your post!")
            return res.json();
        } else {
            toast.error(`Error! Unable to add tag!`)
            return
        }
    })
}

  useEffect(() => {
    return getToken().then((token) =>
      fetch(`/api/tag`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`
        }
      })
        .then((res) => {
          if (res.status === 404) {
            toast.error("This isn't the tag you're looking for");
            return;
          }
          return res.json();
        })
        .then((data) => {
            if (data) {
                setTags(data);
                console.log(data)
            }
        })
    )
  }, []);

  if (!tags) return null;

  return (
    <div>
            <select
                value=""
                name="tagId"
                id="tagId"
                className="form-control"
                onChange={e => {
                    addPostTag ({
                        postId,
                        tagId: e.target.value
                    })
                }}>
                <option value="0">
                    add a tag
                </option>
                {tags.map((tag) => {
                    return (
                        <option key={tag.id} value={tag.id}>
                        {tag.name}
                        </option>
                    )})}
            </select>
        
    </div>             
  );
};

export default TagSelect;
