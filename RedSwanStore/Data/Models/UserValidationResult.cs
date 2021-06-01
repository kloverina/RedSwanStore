using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RedSwanStore.Data.Models
{
    public enum ValidationType
    {
        HasInvalidCharacters,
        HasInvalidLength
    }
    
    [NotMapped]
    [Serializable]
    public class UserValidationResult
    {
        [NotMapped]
        public Dictionary<ValidationType, bool> Name { get; } = new Dictionary<ValidationType, bool> {
            {ValidationType.HasInvalidCharacters, false},
            {ValidationType.HasInvalidLength, false}
        };
        
        [NotMapped]
        public Dictionary<ValidationType, bool> Surname { get; } = new Dictionary<ValidationType, bool> {
            {ValidationType.HasInvalidCharacters, false},
            {ValidationType.HasInvalidLength, false}
        };
        
        [NotMapped]
        public Dictionary<ValidationType, bool> Login { get; } = new Dictionary<ValidationType, bool> {
            {ValidationType.HasInvalidLength, false},
            {ValidationType.HasInvalidCharacters, false}
        };
        
        [NotMapped]
        public Dictionary<ValidationType, bool> Email { get; } = new Dictionary<ValidationType, bool> {
            {ValidationType.HasInvalidCharacters, false},
            {ValidationType.HasInvalidLength, false}
        };
        
        [NotMapped]
        public Dictionary<ValidationType, bool> Password { get; } = new Dictionary<ValidationType, bool> {
            {ValidationType.HasInvalidCharacters, false},
            {ValidationType.HasInvalidLength, false}
        };

        
        private bool IsValidLength(string str, int minLength = 0, int maxLength = int.MaxValue)
        {
            return str.Length >= minLength && str.Length <= maxLength;
        }
        

        public void ValidateUser(string name, string surname, string login, string email, string password)
        {
            if (!IsValidLength(name, 2, 50))
                Name[ValidationType.HasInvalidLength] = true;
            
            foreach (var sym in name)
            {
                if (!char.IsLetter(sym))
                {
                    Name[ValidationType.HasInvalidCharacters] = true;
                    break;
                }
            }
            
            if (!IsValidLength(surname, 2, 50))
                Surname[ValidationType.HasInvalidLength] = true;
            
            foreach (var sym in surname)
            {
                if (!char.IsLetter(sym))
                {
                    Surname[ValidationType.HasInvalidCharacters] = true;
                    break;
                }
            }

            if (!IsValidLength(login, 2, 20))
                Login[ValidationType.HasInvalidLength] = true;

            if (!IsValidLength(email, 5))
                Email[ValidationType.HasInvalidLength] = true;

            if (!IsValidLength(password, 8))
                Password[ValidationType.HasInvalidLength] = true;
            
            foreach (var sym in password)
            {
                if (!char.IsLetterOrDigit(sym) && sym != ' ' && sym != '_' && sym != '-')
                {
                    Password[ValidationType.HasInvalidCharacters] = true;
                    break;
                }
            }
        }

        public bool IsValid()
        {
            return Name.All(kvp => kvp.Value == false) &&
                   Surname.All(kvp => kvp.Value == false) &&
                   Login.All(kvp => kvp.Value == false) &&
                   Email.All(kvp => kvp.Value == false) &&
                   Password.All(kvp => kvp.Value == false);
        }

        public string AsJson()
        {
            var json = "";

            json += "{";
            json += $"\"{nameof(Name)}\":{{";
            json += $"\"HasInvalidCharacters\":{Name[ValidationType.HasInvalidCharacters].ToString().ToLower()},";
            json += $"\"HasInvalidLength\":{Name[ValidationType.HasInvalidLength].ToString().ToLower()}";
            json += "},";
            json += $"\"{nameof(Surname)}\":{{";
            json += $"\"HasInvalidCharacters\":{Surname[ValidationType.HasInvalidCharacters].ToString().ToLower()},";
            json += $"\"HasInvalidLength\":{Surname[ValidationType.HasInvalidLength].ToString().ToLower()}";
            json += "},";
            json += $"\"{nameof(Login)}\":{{";
            json += $"\"HasInvalidCharacters\":{Login[ValidationType.HasInvalidCharacters].ToString().ToLower()},";
            json += $"\"HasInvalidLength\":{Login[ValidationType.HasInvalidLength].ToString().ToLower()}";
            json += "},";
            json += $"\"{nameof(Email)}\":{{";
            json += $"\"HasInvalidCharacters\":{Email[ValidationType.HasInvalidCharacters].ToString().ToLower()},";
            json += $"\"HasInvalidLength\":{Email[ValidationType.HasInvalidLength].ToString().ToLower()}";
            json += "},";
            json += $"\"{nameof(Password)}\":{{";
            json += $"\"HasInvalidCharacters\":{Password[ValidationType.HasInvalidCharacters].ToString().ToLower()},";
            json += $"\"HasInvalidLength\":{Password[ValidationType.HasInvalidLength].ToString().ToLower()}";
            json += "}}";

            return json;
        }
    }
}