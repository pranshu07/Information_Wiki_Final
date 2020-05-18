using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Information_Wiki_Final.Models
{
    //The wiki page with a titile and a content 
    public class WikiPage
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime LastModifed { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }



    }
}
