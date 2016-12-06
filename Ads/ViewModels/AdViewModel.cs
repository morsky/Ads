namespace Ads.ViewModels
{
    using System;
    using Models;

    public class AdViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string FileName { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}