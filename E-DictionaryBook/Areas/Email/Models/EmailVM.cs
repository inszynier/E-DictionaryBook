using E_DictionaryBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_DictionaryBook.Areas.Email.Models
{
    public class EmailVM
    {
        public int Id { get; set; }
        public string To { get; set; }
        public EDictionaryUser[] CClist { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string From { get; set; }
        public DateTime Date { get; set; }
    }
}