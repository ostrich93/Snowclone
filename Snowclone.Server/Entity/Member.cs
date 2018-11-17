﻿using System;
using System.Collections.Generic;

namespace Snowclone.Entities
{
    //Not a model, but an actual entity to be manipulated
    public class Member
    {
        public int MemberId { get; set; }

        public string Handle { get; set; }

        public string Password; //maybe make it protected instead? Also needs to be hashed.

        public string Email { get; set; } //might get rid of when app scales up.

        public int UserId { get; set; } //Associated with the user for when app scales up.

        public List<Message> Messages { get; set; } //list of messages posted by the user

        public Member()
        {

        }
    }
}