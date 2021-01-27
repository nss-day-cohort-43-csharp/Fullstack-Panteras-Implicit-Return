import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import firebase from "firebase/app";
import "firebase/storage";

const firebaseConfig = {
  apiKey: process.env.REACT_APP_API_KEY,
  authDomain: "tabloid-34976.firebaseapp.com",
  projectId: "tabloid-34976",
  storageBucket: "tabloid-34976.appspot.com",
  messagingSenderId: "1064718686299",
  appId: "1:1064718686299:web:c58d9b0041c102ed50877e"
};
firebase.initializeApp(firebaseConfig);

const storage = firebase.storage();

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById("root")
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();

export { storage, firebase as default}
