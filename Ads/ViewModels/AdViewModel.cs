namespace Ads.ViewModels
{
    using System;
    using Models;

    public class AdViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}