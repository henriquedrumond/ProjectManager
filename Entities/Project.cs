using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagerApi.Entities
{
	public class Project
	{
        [Key]
        public Guid Id { get; set; }
		public string Name { get; set; }
		public string Owner { get; set; }
		public DateTime CreateDate { get; set; }
	}
}

