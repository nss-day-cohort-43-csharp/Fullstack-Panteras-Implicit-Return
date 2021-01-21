import React, { useContext } from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import { UserProfileContext } from "../providers/UserProfileProvider";
import Explore from "../pages/Explore";
import Login from "../pages/Login";
import Register from "../pages/Register";
import PostDetails from "../pages/PostDetails";
import CategoryManager from "../pages/CategoryManager";
import NotFound from "../pages/NotFound";
import CommentList from "./CommentList";

const ApplicationViews = () => {
  const { isLoggedIn } = useContext(UserProfileContext);

  return (
    <Switch>
      <Route path="/" exact>
        {isLoggedIn ? <p>Home</p> : <Redirect to="/login" />}
      </Route>
      <Route path="/explore">
        {isLoggedIn ? <Explore /> : <Redirect to="/login" />}
      </Route>
      <Route path="/post/:postId">
        {isLoggedIn ? <PostDetails /> : <Redirect to="/login" />}
      </Route>

      <Route path="/comment/:postId">
        {isLoggedIn ? <CommentList /> : <Redirect to="/login" />}
      </Route>

      <Route path="/categories">
        {isLoggedIn && JSON.parse(localStorage.getItem("userProfile")).userTypeId === 1 ? <CategoryManager /> : <Redirect to="/notfound" />}
      </Route>
      <Route path="/login">
        <Login />
      </Route>
      <Route path="/register">
        <Register />
      </Route>
      <Route component={NotFound} />
    </Switch>
  );
};

export default ApplicationViews;
