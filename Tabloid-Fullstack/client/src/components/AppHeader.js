import React, { useState, useContext } from "react";
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
import { UserProfileContext } from "../providers/UserProfileProvider";

const AppHeader = () => {
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
                {isAdmin() && (
                  <NavItem>
                    <NavLink to="/categories" tag={Link}>
                      Categories
                    </NavLink>
                  </NavItem>
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
              Welcome {user.displayName}
            </NavbarText>
          ) : null}
        </Collapse>
      </Navbar>
    </div>
  );
};

export default AppHeader;
