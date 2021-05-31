using System;
using System.ComponentModel.DataAnnotations;
using contractor.Models;

namespace contractor.Models
{
    public class JobContractor
    {
        public int Id { get; set; }
        [Required]
        public int JobId { get; set; }
        [Required]
        public int ContractorId { get; set; }
        [Required]
        public string CreatorId { get; set; }
    }
    public class JobContractorViewModel : Job
    {
        public int JobId { get; set; }
        public int ContractorId { get; set; }
        public int JobContractorId { get; set; }

    }
}