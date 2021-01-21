// import React from "react";
// import CommentSummaryCard from "./CommentSummaryCard";

// const CommentList = ({ comments }) => {
//     // useEffect(() => {
//     //     fetch(`/api/comment/${postId}`)
//     //         .then((res) => {
//     //             if (res.status === 404) {
//     //                 toast.error("Comment unavailable");
//     //                 return;
//     //             }
//     //             return res.json();
//     //         })
//     //         .then((data) => {
//     //             setComment(data);
//     //             console.log(data);
//     //         });
//     // }, [postId]);

//     if (!comments) return null
//     return (
//         <div>
//             {comments.map((comment) => (
//                 <div className="m-4" key={comment.id}>
//                     <CommentSummaryCard comment={comment} />
//                 </div>
//             ))}
//         </div>
//     );
// };

// export default CommentList;