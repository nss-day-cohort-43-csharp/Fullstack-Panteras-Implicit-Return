import React, { useEffect, useState, useContext } from "react";
import { toast } from "react-toastify";
import { Dropdown, DropdownToggle, DropdownMenu, DropdownItem} from "reactstrap";
import { UserProfileContext } from '../providers/UserProfileProvider';

const TagSelect = ({tag}) => {
  const [tags, setTags] = useState([]);
  const [postTag, setPostTag] = useState();
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const { getToken } = useContext(UserProfileContext);

  const handleControlledInputChange = (event) => {
    const newPostTag = { ...postTag };
    newPostTag[event.target.name] = event.target.value;
    setPostTag(newPostTag);
  };
  const toggle = () => setDropdownOpen(prevState => !prevState);


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
            }
        })
    )
  }, []);

  if (!tag) return null;

  return (
    <div>
       <Dropdown isOpen={dropdownOpen} toggle={toggle}>
           <DropdownToggle caret>
            Tags
           </DropdownToggle>
                <DropdownMenu
                    value={tag.id}
                    name="tagId"
                    required
                    id="tagId"
                    className="form-control"
                    autoFocus
                    onChange={handleControlledInputChange}>
                    <DropdownItem value="0">
                        select tag
                    </DropdownItem>
                    {tags.map((tag) => {
                        return (
                            <DropdownItem key={tag.id} value={tag.id}>
                            {tag.name}
                            </DropdownItem>
                        )})}
                </DropdownMenu>
        </Dropdown>
    </div>             
  );
};

export default TagSelect;
