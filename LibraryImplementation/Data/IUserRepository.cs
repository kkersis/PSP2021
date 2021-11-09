using LibraryImplementation.Models;
using System.Collections.Generic;

namespace LibraryImplementation.Data
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User DeleteById(int id);
        User Save(User user);
    }
}