using System;
using System.Collections.Generic;
using System.Linq;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data.Repositories
{
    /// <summary>
    /// The class to work with database User table.
    /// </summary>
    public class UserRepo : IUserRepo
    {
        private readonly RedSwanStoreDBContent dbContent;

        public UserRepo(RedSwanStoreDBContent dbContent)
        {
            this.dbContent = dbContent;
        }

        private void LoadDataFor(User user)
        {
            dbContent.Entry(user).Reference(u => u.Library).Load();
            dbContent.Entry(user.Library).Collection(ul => ul.UserLibraryGames).Load();
        }

        private bool TryUpdate(User user)
        {
            try
            {
                dbContent.Users.Update(user);
                dbContent.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
        
        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> result = dbContent.Users.ToList();

            foreach (User user in result)
                LoadDataFor(user);

            return result;
        }

        public User GetUserByUrl(string url)
        {
            User result = dbContent.Users.FirstOrDefault(
                u => u.UserUrl == url
            );
            
            LoadDataFor(result);

            return result;
        }

        public User GetUserByEmail(string email)
        {
            User result = dbContent.Users.FirstOrDefault(
                u => u.Email == email
            );
            
            LoadDataFor(result);

            return result;
        }

        public bool UpdateUserName(User user, string name)
        {
            if (name.Length < 2 || name.Length > 50)
                return false;

            foreach (var sym in name)
            {
                if (!char.IsLetter(sym))
                    return false;
            }
            
            user.Name = name;

            return TryUpdate(user);
        }

        public bool UpdateUserSurname(User user, string surname)
        {
            if (surname.Length < 2 || surname.Length > 50)
                return false;
            
            foreach (var sym in surname)
            {
                if (!char.IsLetter(sym))
                    return false;
            }
            
            user.Surname = surname;

            return TryUpdate(user);
        }

        public bool UpdateUserPhoto(User user, string photoUrl)
        {
            user.Photo = photoUrl;

            return TryUpdate(user);
        }

        public bool UpdateUserLogin(User user, string login)
        {
            if (login.Length < 2 || login.Length > 20)
                return false;
            
            user.Login = login;

            return TryUpdate(user);
        }

        public bool UpdateUserPassword(User user, string password)
        {
            if (password.Length < 8)
                return false;

            foreach (var sym in password)
            {
                if (!char.IsLetterOrDigit(sym) && sym != ' ')
                    return false;
            }

            user.Password = password;

            return TryUpdate(user);
        }

        public bool UpdateUserBalance(User user, decimal balanceChange)
        {
            balanceChange = decimal.Round(balanceChange, 2);

            if (user.Balance + balanceChange < 0)
                return false;

            user.Balance += balanceChange;

            return TryUpdate(user);
        }

        public bool UpdateUserNewsGetting(User user, bool getNewsOnEmail)
        {
            user.GetNewsOnEmail = getNewsOnEmail;

            return TryUpdate(user);
        }

        public bool UpdateUserUrl(User user, string url)
        {
            if (url.Length < 1 || url.Length > 50)
                return false;

            foreach (var sym in url)
            {
                if (!char.IsLetterOrDigit(sym) && sym != '_' && sym != '-')
                    return false;
            }

            bool isUnique = dbContent.Users.FirstOrDefault(u => u.UserUrl == url) is null;

            return isUnique && TryUpdate(user);
        }

        public bool AddGameToLibrary(User user, Game game)
        {
            var libraryGame = new UserLibraryGame {
                Game = game,
                HoursPlayed = 0,
                IsFavourite = false,
                LastPlayed = DateTime.MinValue,
            };
            
            user.Library ??= new UserLibrary();
            
            user.Library.UserLibraryGames.Add(libraryGame);

            return TryUpdate(user);
        }

        private bool IsValidLength(string str, int minLength = 0, int maxLength = int.MaxValue)
        {
            return str.Length >= minLength && str.Length <= maxLength;
        }
        
        public bool AddUser(string name, string surname, string login, string email, string password, bool getNewsOnEmail)
        {
            if (!IsValidLength(name, 2, 50))
                return false;
            
            foreach (var sym in name)
            {
                if (!char.IsLetter(sym))
                    return false;
            }

            if (!IsValidLength(surname, 2, 50))
                return false;
            
            foreach (var sym in surname)
            {
                if (!char.IsLetter(sym))
                    return false;
            }

            if (!IsValidLength(login, 2, 20))
                return false;

            if (!IsValidLength(email, 5))
                return false;

            if (!IsValidLength(password, 8))
                return false;
            
            foreach (var sym in password)
            {
                if (!char.IsLetterOrDigit(sym) && sym != ' ')
                    return false;
            }

            var lastUserId = dbContent.Users.Max(u => u.Id);

            var url = $"id#{(lastUserId + 1).ToString()}";
            var picture = "https://i.ibb.co/YyHyyKh/Rew-Swan-Pic.png";
            

            var user = new User {
                Name = name,
                Surname = surname,
                Password = password,
                Login = login,
                Email = email,
                GetNewsOnEmail = getNewsOnEmail,
                Photo = picture,
                UserUrl = url
            };

            try
            {
                dbContent.Users.Add(user);
                dbContent.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}