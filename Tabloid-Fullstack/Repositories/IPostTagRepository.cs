﻿using System.Collections.Generic;
using Tabloid_Fullstack.Models;

namespace Tabloid_Fullstack.Repositories
{
    public interface IPostTagRepository
    {
        void Add(PostTag postTag);
        void Delete(int id);
        List<PostTag> Get();
        PostTag GetById(int id);
    }
}