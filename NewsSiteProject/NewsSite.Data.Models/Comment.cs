namespace NewsSite.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NewsSite.Data.Common.Models;

    public class Comment : DeletableEntity
    {
        public Comment()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Името трябва да е между 1 и 40 символа")]
        public string AuthorName { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 2, ErrorMessage = "Коментара трябва да е между 2 и 1000 символа")]
        public string Content { get; set; }

        public long ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
