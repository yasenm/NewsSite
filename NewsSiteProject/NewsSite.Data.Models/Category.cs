namespace NewsSite.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NewsSite.Data.Common.Models;

    public class Category : DeletableEntity
    {
        public Category()
        {
            this.CreatedOn = DateTime.Now.Date;
            this.IsDeleted = false;
            this.Articles = new HashSet<Article>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
