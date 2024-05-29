using System;
using System.Collections.Generic;
using System.Linq;
using NewsPortal.BusinessLogic.DbModel;
using NewsPortal.Domain.Entities.Comment;
using NewsPortal.Domain.Entities.Post;
using NewsPortal.Domain.Entities.User;

namespace NewsPortal.BusinessLogic.Core
{
    public class CommentApi
    {
        public ServiceResponse AddComment(NewCommentData data)
        {
            var newCommentary = new CDbTable()
            {
                Content = data.Content,
                DateAdded = data.DateAdded,
                AuthorId = data.AuthorId,
                PostId = data.PostId,
                Author = data.Author
            };

            var response = new ServiceResponse();

            try
            {
                using (var db = new CommentContext())
                {
                    db.Comments.Add(newCommentary);
                    db.SaveChanges();
                }

                response.StatusMessage = "Comment added successfully!";
                response.Status = true;
                response.PostId = newCommentary.PostId;
            }
            catch
            {
                response.StatusMessage = "An error occured while adding comment";
                response.Status = false;
                response.PostId = 0;
            }

            return response;
        }

        public IEnumerable<CommentMinimal> ReturnCommentsByPost(int postId)
        {
            List<CommentMinimal> list = new List<CommentMinimal>();

            using (var commentDb = new CommentContext())
            {
                var results = commentDb.Comments.Where(a => a.PostId == postId);
                UDbTable author;
                string authorUsername;
                int authorId;

                foreach (var item in results)
                {
                    using (var userDb = new UserContext())
                    {
                        author = userDb.Users.FirstOrDefault(u => u.Id == item.AuthorId);
                    }

                    if (author == null)
                    {
                        authorUsername = "Deleted user";
                        authorId = 0;
                    }
                    else
                    {
                        authorUsername = author.Username;
                        authorId = author.Id;
                    }

                    var commentMinimal = new CommentMinimal()
                    {
                        Id = item.Id,
                        Content = item.Content,
                        Author = authorUsername,
                        AuthorId = authorId,
                        PostId = item.AuthorId,
                        DateAdded = item.DateAdded,
                    };

                    list.Add(commentMinimal);
                }
            }

            return list.ToList();
        }

        public IEnumerable<CommentMinimal> ReturnAllComments()
        {
            List<CommentMinimal> list = new List<CommentMinimal>();

            using (var commentaryDb = new CommentContext())
            {
                var results = commentaryDb.Comments;
                UDbTable author;
                string authorUsername;
                int authorId;

                foreach (var item in results)
                {
                    using (var userDb = new UserContext())
                    {
                        author = userDb.Users.FirstOrDefault(u => u.Id == item.AuthorId);
                    }

                    if (author == null)
                    {
                        authorUsername = "Deleted user";
                        authorId = 0;
                    }
                    else
                    {
                        authorUsername = author.Username;
                        authorId = author.Id;
                    }

                    var commentaryMinimal = new CommentMinimal()
                    {
                        Id = item.Id,
                        Content = item.Content,
                        Author = authorUsername,
                        AuthorId = authorId,
                        PostId = item.AuthorId,
                        DateAdded = item.DateAdded,
                    };

                    list.Add(commentaryMinimal);
                }
            }

            return list.ToList();
        }


        public ServiceResponse ReturnEditedComment(CEditData existingComment)
        {
            var response = new ServiceResponse();
            using (var db = new CommentContext())
            {
                try
                {
                    var commentaryToEdit = db.Comments.Find(existingComment.Id);
                    if (commentaryToEdit != null)
                    {
                        commentaryToEdit.Id = existingComment.Id;
                        commentaryToEdit.Content = existingComment.Content;
                        commentaryToEdit.Author = existingComment.Author;
                        commentaryToEdit.AuthorId = existingComment.AuthorId;
                        commentaryToEdit.PostId = existingComment.PostId;
                        commentaryToEdit.DateAdded = existingComment.DateAdded;

                        db.SaveChanges();
                        response.Status = true;
                        response.PostId = commentaryToEdit.Id;
                        response.StatusMessage = "Your comment was edited successfully!";
                    }
                    else
                    {
                        response.PostId = 0;
                        response.Status = false;
                        response.StatusMessage = "Comment not found!";
                    }
                }
                catch (Exception ex)
                {
                    response.PostId = 0;
                    response.Status = false;
                    response.StatusMessage = "An error occurred!";
                }
            }

            return response;
        }
    }
}