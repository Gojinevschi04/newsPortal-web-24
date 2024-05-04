using System;
using System.Collections.Generic;
using System.Linq;
using NewsPortal.BusinessLogic.DbModel;
using NewsPortal.Domain.Entities.Comment;
using NewsPortal.Domain.Entities.Post;
using NewsPortal.Domain.Entities.User;

namespace NewsPortal.BusinessLogic.Core
{
    public class CommentaryApi
    {
        public ServiceResponse AddCommentary(NewCommentaryData data)
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
                using (var db = new CommentaryContext())
                {
                    db.Comments.Add(newCommentary);
                    db.SaveChanges();
                }

                response.StatusMessage = "Commentary added successfully!";
                response.Status = true;
                response.PostId = newCommentary.PostId;
            }
            catch
            {
                response.StatusMessage = "An error occured while adding commentary";
                response.Status = false;
                response.PostId = 0;
            }

            return response;
        }

        public IEnumerable<CommentaryMinimal> ReturnCommentariesByPost(int postId)
        {
            List<CommentaryMinimal> list = new List<CommentaryMinimal>();

            using (var commentaryDb = new CommentaryContext())
            {
                var results = commentaryDb.Comments.Where(a => a.PostId == postId);
                UDbTable author;

                foreach (var item in results)
                {
                    using (var userDb = new UserContext())
                    {
                        author = userDb.Users.FirstOrDefault(u => u.Id == item.AuthorId);
                    }

                    var commentaryMinimal = new CommentaryMinimal()
                    {
                        Id = item.Id,
                        Content = item.Content,
                        Author = author.Username,
                        AuthorId = item.AuthorId,
                        PostId = item.AuthorId,
                        DateAdded = item.DateAdded,
                    };

                    list.Add(commentaryMinimal);
                }
            }

            return list.ToList();
        }

        public ServiceResponse ReturnEditedCommentary(CEditData existingCommentary)
        {
            var response = new ServiceResponse();
            using (var db = new CommentaryContext())
            {
                try
                {
                    var commentaryToEdit = db.Comments.Find(existingCommentary.Id);
                    if (commentaryToEdit != null)
                    {
                        commentaryToEdit.Id = existingCommentary.Id;
                        commentaryToEdit.Content = existingCommentary.Content;
                        commentaryToEdit.Author = existingCommentary.Author;
                        commentaryToEdit.AuthorId = existingCommentary.AuthorId;
                        commentaryToEdit.PostId = existingCommentary.PostId;
                        commentaryToEdit.DateAdded = existingCommentary.DateAdded;

                        db.SaveChanges();
                        response.Status = true;
                        response.PostId = commentaryToEdit.Id;
                        response.StatusMessage = "Your commentary was edited successfully!";
                    }
                    else
                    {
                        response.PostId = 0;
                        response.Status = false;
                        response.StatusMessage = "Commentary not found!";
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