namespace TichTich.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TichTich.Data.Common.Models;

    public class Result : BaseDeletableModel<int>
    {
        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public string Race { get; set; }

        public string RaceId { get; set; }

        [Required]
        public TimeSpan FinishTime { get; set; }
    }
}