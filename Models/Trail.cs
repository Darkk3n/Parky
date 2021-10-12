using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parky.Models
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        public DifficultyType Difficulty { get; set; }

        [Required]
        public int NationalParkId { get; set; }

        [ForeignKey("NationalParkId")]
        public NationalPark NationalPark { get; set; }

        public DateTime CreationDate { get; set; }
    }
}