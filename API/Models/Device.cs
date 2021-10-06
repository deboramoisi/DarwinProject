using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Device
    {
        public int Id { get; set; }

        [Required] public string Name { get; set; }
        
        [Required] public string Manufacturer { get; set; }
        
        [Required] public string Type { get; set; }
        
        [Required] public string OS { get; set; }
        
        [Required] public float OSVersion { get; set; }
        
        [Required] public float Processor { get; set; }
        
        [Required] public int RAM { get; set; }
        
        public int? AppUserId { get; set; }

        [ForeignKey("AppUserId")] public AppUser AppUser { get; set; }
    }
}