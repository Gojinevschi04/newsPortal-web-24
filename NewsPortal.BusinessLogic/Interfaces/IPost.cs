﻿using System.Collections.Generic;
using NewsPortal.Domain.Entities.Post;
using NewsPortal.Domain.Entities.User;

namespace NewsPortal.BusinessLogic.Interfaces
{
     public interface IPost
     {
          ServiceResponse AddPostAction(NewPostData data);
          PDbTable GetById(int postId);
          IEnumerable<PDbTable> GetAll();
     }
}