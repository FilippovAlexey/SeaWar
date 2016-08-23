using System;
using System.Collections.Generic;
using System.Reflection;

namespace SeaWarServer
{
    public class Messages
    {
        public const string WrongRequest = "Wrong request data";
        public const string UserNotFound = "User not found";
        public const string Success = "Success";
        public const string NotFound = "Not Found";
        public const string BadData = "Wrong data";
        public const string NotEmpty = "Values must not be empty";
        public const string NotPos = "Not possible";
        public const string RoleMissing = "This role is missing";
        public const string SessionNotFound = "Session not found";
        public const string AlreadyTurns = "Turn already ";
        public const string AlreadyCreated = "Game already created";
        public const string AlreadyStarted = "Game already started";

        public static Dictionary<string, string> GetStrings()
        {
            Type t = typeof(Messages);
            var fields = t.GetFields();
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var a in fields)
            {
                result.Add(a.Name, (string)a.GetValue(a));
            }
            return result;
        }
    }
}