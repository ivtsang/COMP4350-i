using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ConnectR.Models;

namespace ConnectR.DataInit
{
    //copy of this in tests as well, will remove once iter2
    public class TestDbInit : DropCreateDatabaseAlways<ConnectRContext>
    {
        protected override void Seed(ConnectRContext context)
        {
            //users
            context.Users.Add(new User() { user_id = 1 });
            //context.Users.Add(new User() { user_id = 2 });
            //context.Users.Add(new User() { user_id = 3 });

            //profiles
            context.Profiles.Add(new Profile() { ProfileId = 1, UserId = "1", FirstName = "bob" });

            //conferences
            context.Conferences.Add(new Conference() { ConferenceId = 1, ProfileId = 1, Title = "ye" });

            //conversations
            HashSet<User> users = new HashSet<User>();
            User user1 = new User() { user_id = 2 };
            User user2 = new User() { user_id = 3 };
            users.Add(user1);
            users.Add(user2);
            Conversation conv = new Conversation() { conv_id = 11231, Users = users };
            context.Conversations.Add(conv);

            //messages
            context.Messages.Add(new Message() { msg_id = 1, conv_id = 11231, user_id = 2, msg_date = DateTime.Now, msg_txt = "hello world", Conversation = conv });
            context.Messages.Add(new Message() { msg_id = 2, conv_id = 11231, user_id = 2, msg_date = DateTime.Now, msg_txt = "greetings", Conversation = conv });
            context.Messages.Add(new Message() { msg_id = 3, conv_id = 11231, user_id = 2, msg_date = DateTime.Now, msg_txt = "well met", Conversation = conv });
            context.Messages.Add(new Message() { msg_id = 4, conv_id = 11231, user_id = 2, msg_date = DateTime.Now, msg_txt = "i am talking to myself", Conversation = conv });
        }
    }
}