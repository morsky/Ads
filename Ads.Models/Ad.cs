namespace Ads.Models
{
    using System;

    public class Ad
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string FileName { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}