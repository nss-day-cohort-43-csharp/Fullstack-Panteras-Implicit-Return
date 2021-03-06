// Additions by: Terra Roush, 
import React, { useContext } from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import { UserProfileContext } from "../providers/UserProfileProvider";
import Explore from "../pages/Explore";
import Login from "../pages/Login";
import Register from "../pages/Register";
import PostCreate from "../pages/post/PostCreate"
import PostEdit from "../pages/post/PostEdit";
import PostDetails from "../pages/PostDetails";
import CategoryManager from "../pages/CategoryManager";
import MyPosts from "../pages/MyPosts";
import TagManager from "../pages/TagManager";
import NotFound from "../pages/NotFound";

const ApplicationViews = () => {
  const { isLoggedIn, isAdmin } = useContext(UserProfileContext);

  return (
    <Switch>
      <Route path="/" exact>
        {isLoggedIn ? <p>Home</p> : <Redirect to="/login" />}
      </Route>
      <Route path="/explore">
        {isLoggedIn ? <Explore /> : <Redirect to="/login" />}
      </Route>
      <Route path="/post/create">
        {isLoggedIn ? <PostCreate /> : <Redirect to="/login" />}
      </Route>
      <Route path="/post/edit/:postId">
        {isLoggedIn ? <PostEdit /> : <Redirect to="/login" />}
      </Route>
      <Route path="/my-posts">
        {isLoggedIn ? <MyPosts /> : <Redirect to="/login" />}
      </Route>
      <Route path="/post/:postId">
        {isLoggedIn ? <PostDetails /> : <Redirect to="/login" />}
      </Route>
      <Route path="/tags">
        {isLoggedIn && isAdmin() ? <TagManager /> : !isLoggedIn ? <Redirect to="/login" /> : <Redirect to="/notfound" />}
      </Route>

      <Route path="/categories">
        {isLoggedIn && isAdmin() ? <CategoryManager /> : <Redirect to="/notfound" />}
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
