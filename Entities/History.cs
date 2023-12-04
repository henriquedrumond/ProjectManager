using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagerApi.Entities
{
	public class History
	{
        [Key]
        public Guid Id { get; set; }
		public Guid TaskId { get; set; }
		public DateTime Modify { get; set; }

        [MaxLength(50)]
        public string ModifyBy { get; set; }

        [MaxLength(255)]
        public string ChangeFields { get; set; }

        [MaxLength(250)]
        public string Table { get; set; }
	}
}

