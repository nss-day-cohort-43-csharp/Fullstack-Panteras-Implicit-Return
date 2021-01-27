// Additions by: Terra Roush, 

import React, { useState, useContext, Fragment } from "react";
import { Link, useHistory } from "react-router-dom";
import { toast } from "react-toastify";
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
  NavbarText,
} from "reactstrap";
import { Header, Image } from "semantic-ui-react";
import { UserProfileContext } from "../providers/UserProfileProvider";

const AppHeader = () => {
  const image = localStorage.getItem("image");
  const { getCurrentUser, logout, isAdmin } = useContext(UserProfileContext);
  const user = getCurrentUser();
  const history = useHistory();
  const [isOpen, setIsOpen] = useState(false);

  const toggle = () => setIsOpen(!isOpen);

  const logoutAndReturn = () => {
    return logout().then(() => {
      toast.dark("You are now logged out");
      history.push("/login");
    });
  };

  return (
    <div>
      <Navbar color="dark" dark expand="md">
        <NavbarBrand tag={Link} to="/">
          <img
            id="header-logo"
            src="/quill.png"
            width="30"
            height="30"
            className="mr-1"
            alt="Quill Logo"
          />
          Tabloid
        </NavbarBrand>
        <NavbarToggler onClick={toggle} />
        <Collapse isOpen={isOpen} navbar>
          <Nav className="mr-auto" navbar>
            {user ? (
              <>
                <NavItem>
                  <NavLink to="/explore" tag={Link}>
                    Explore
                  </NavLink>
                </NavItem>
                <NavItem>
                  <NavLink to="/post/create" tag={Link}>
                    Create Post
                  </NavLink>
                </NavItem>
                <NavItem>
                  <NavLink to="/my-posts" tag={Link}>
                    My Posts
                  </NavLink>
                </NavItem>
                {isAdmin() && (
                <>    <NavItem>
                    <NavLink to="/categories" tag={Link}>
                      Categories
                    </NavLink>
                  </NavItem>
                
                  <NavItem>
                    <NavLink to="/tags" tag={Link}>
                      Tags
                    </NavLink>
                  </NavItem>
                </>
                )}
                <NavItem>
                  <NavLink onClick={logoutAndReturn} tag={Link}>
                    Logout
                  </NavLink>
                </NavItem>
              </>
            ) : (
              <>
                <NavItem>
                  <NavLink to="/login" tag={Link}>
                    Login
                  </NavLink>
                </NavItem>
                <NavItem>
                  <NavLink to="/register" tag={Link}>
                    Register
                  </NavLink>
                </NavItem>
              </>
            )}
          </Nav>
          {user ? (
            <NavbarText className="d-sm-none d-md-block">
              <Header as='h3'>
              <Image src={image} circular/>Welcome {user.displayName}
              </Header>
            </NavbarText>
          ) : null}
        </Collapse>
      </Navbar>
    </div>
  );
};

export default AppHeader;
