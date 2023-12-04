using System;
using ProjectManagerApi.Entities;

namespace ProjectManagerApi.Models
{
	public class CreateHistoryRequest
	{
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public DateTime Modify { get; set; }
        public string ModifyBy { get; set; }
        public string ChangeFields { get; set; }
        public string Table { get; set; }
    }
}

