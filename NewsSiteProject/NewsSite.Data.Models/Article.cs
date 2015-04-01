namespace NewsSite.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NewsSite.Data.Common.Models;

    public class Article : DeletableEntity
    {
        private ICollection<Photo> photos;
        private ICollection<Comment> comments;

        public Article()
        {
            this.photos = new HashSet<Photo>();
            this.comments = new HashSet<Comment>();
            this.IsHighlighted = false;
        }

        [Key]
        public long Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsImportant { get; set; }

        public int ReadersCount { get; set; }

        /// <summary>
        /// Marker for highlighting a news. It will be shown in the carousel if checked.
        /// </summary>
        public bool IsHighlighted { get; set; }

        //[Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public long PhotoId { get; set; }

        public virtual Photo Photo { get; set; }

        public ICollection<Photo> Photos
        {
            get { return this.photos; }
            set { this.photos = value; }
        }

        public ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
