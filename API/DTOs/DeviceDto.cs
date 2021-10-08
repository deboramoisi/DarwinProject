using System.ComponentModel.DataAnnotations;
using API.Models;

namespace API.DTOs
{
    public class DeviceDto
    {
        public int Id { get; set; }
        
        [Required] public string Name { get; set; }
        
        [Required] public string Manufacturer { get; set; }
        
        [Required] public string Type { get; set; }
        
        [Required] public string OS { get; set; }
        
        [Required] public float OSVersion { get; set; }
        
        [Required] public string Processor { get; set; }
        
        [Required] public int RAM { get; set; }
        public int? AppUserId { get; set; }
        public string UserName { get; set; }
    }
}