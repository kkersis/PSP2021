using LibraryImplementation.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryImplementation.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;
        private readonly string _filePath;

        public UserRepository(IConfiguration config)
        {
            _config = config;
            _filePath = _config.GetValue<string>("FilePathSettings:Default");
        }

        public UserRepository(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<User> GetAll()
        {
            return ReadUsersFromFile();
        }

        public User GetById(int id)
        {
            var allUsers = ReadUsersFromFile().ToList();
            var user = allUsers.FirstOrDefault(i => i.Id == id);
            return user;
        }

        public User Save(User user)
        {
            var allUsers = ReadUsersFromFile().ToList();
            if(user.Id == 0) // if creating new
            {
                int lastId = 0;
                if (allUsers.Count != 0)
                {
                    lastId = allUsers.Last().Id;
                }
                user.Id = lastId + 1;
                allUsers.Add(user);
            }
            else // if updating existing
            {
                var index = allUsers.FindIndex(i => i.Id == user.Id);
                allUsers[index] = user;
            }
            return WriteUsersToFile(allUsers) ? user : null;
        }

        public User DeleteById(int id)
        {
            var allUsers = ReadUsersFromFile().ToList();
            var userToDelete = allUsers.FirstOrDefault(i => i.Id == id);
            bool deleted = allUsers.Remove(userToDelete);
            WriteUsersToFile(allUsers);
            return deleted ? userToDelete : null;
        }

        private bool WriteUsersToFile(IEnumerable<User> users)
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(users);
                writer = new StreamWriter(_filePath, false);
                writer.Write(contentsToWriteToFile);
            }
            catch (IOException)
            {
                return false;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
            return true;
        }

        private IEnumerable<User> ReadUsersFromFile()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(_filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<IEnumerable<User>>(fileContents);
            }
            catch (IOException)
            {
                return Enumerable.Empty<User>();
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
