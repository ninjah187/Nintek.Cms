using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms.Users.Models
{
    public class Session : Model
    {
        public Session(int userId, string key, DateTime createdAt)
        {
            UserId = userId;
            Key = key;
            CreatedAt = createdAt;
        }

        public int UserId { get; }
        public string Key { get; }
        public DateTime CreatedAt { get; }
    }
}
