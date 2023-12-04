using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagerApi.Models
{
	public class CreateProjectRequest
	{
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Owner { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
    }
}

