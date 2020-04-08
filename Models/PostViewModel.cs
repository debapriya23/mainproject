using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOODLE.Models
{
    public class PostViewModel
    {
        public int Post_Id { get; set; }

        public string Post_Title { get; set; }
        public string Post_Txt { get; set; }
        public IFormFile Image { get; set; } = null;
        public DateTime Post_Date { get; set; } = DateTime.Now;
        public int Post_Like { get; set; }
    }
}

