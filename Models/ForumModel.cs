using System;
using System.ComponentModel.DataAnnotations;

namespace w_list.Models
{
    public class ForumModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Locked { get; set; }
        public bool IsPublic {get; set; }
        public string AllowedWord1 { get; set; }
        public string AllowedWord2 { get; set; }
        public string AllowedWord3 { get; set; }
        public string AllowedWord4 { get; set; }
        public string AllowedWord5 { get; set; }
        public string AllowedWord6 { get; set; }
        public string AllowedWord7 { get; set; }
        public string AllowedWord8 { get; set; }
        public string AllowedWord9 { get; set; }
        public string AllowedWord10 { get; set; }
    }
}