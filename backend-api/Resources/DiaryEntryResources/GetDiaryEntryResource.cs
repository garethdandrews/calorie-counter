using System;
using System.ComponentModel.DataAnnotations;

namespace backend_api.Resources.DiaryEntryResources
{
    public class GetDiaryEntryResource
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(8)]
        public string StringDate { get; set; }
    }
}