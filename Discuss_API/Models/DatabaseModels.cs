using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Discuss_API.Models
{

    public class Discussions
    {
        public Discussions()
        {
            Articles = new HashSet<Articles>();
        }

        public int Id { get; set; }
        [ForeignKey("Category")]
        public int Category_Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Articles> Articles { get; set; }
        [Required]
        [JsonIgnore]
        public virtual Categories Category { get; set; }
    }

    public class Categories
    {
        public Categories()
        {
            Discussions = new HashSet<Discussions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Discussions> Discussions { get; set; }
    }

    public class Articles
    {
        public int Id { get; set; }
        [ForeignKey("Discussion")]
        public int Discussion_Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        [Required]
        [JsonIgnore]
        public virtual Discussions Discussion { get; set; }
    }

}