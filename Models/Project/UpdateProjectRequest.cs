﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagerApi.Models
{
	public class UpdateProjectRequest
	{
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Owner { get; set; }
    }
}

