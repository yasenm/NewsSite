namespace NewsSite.Data.Models
{
    using NewsSite.Data.Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Photo : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public byte[] Content { get; set; }

        public string Extension { get; set; }

        public long? ArticleId { get; set; }

        public long? AdvertismentId { get; set; }

        public virtual Article Article { get; set; }
    }
}
