using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KOODLE.Models
{
        [Table("Post")]
        public class Post
        {
            [Key]
            public int Post_Id { get; set; }

            public string Post_Title { get; set; }
            public string Post_Txt { get; set; }
        public string Image { get; set; } 
        public DateTime Post_Date { get; set; } = DateTime.Now;
            public int Post_Like { get; set; }



        }
}
