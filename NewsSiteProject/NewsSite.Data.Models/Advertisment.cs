namespace NewsSite.Data.Models
{
    using NewsSite.Data.Common.Models;

    public class Advertisment : DeletableEntity
    {
        public long Id { get; set; }

        public string Link { get; set; }

        public string Firm { get; set; }

        public AdvertismentType Type { get; set; }

        public long PhotoId { get; set; }

        public virtual Photo Photo { get; set; }

        public long Clicks { get; set; }

        public bool IsActive { get; set; }
    }
}
