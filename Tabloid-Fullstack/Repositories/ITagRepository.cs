using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabloid_Fullstack.Models;

namespace Tabloid_Fullstack.Repositories
{
    public interface ITagRepository
    {
        void Add(Tag tag);
        List<Tag> GetAll();
        Tag GetById(int id);
    }
}
