using System.ComponentModel.DataAnnotations;
using API.Models;

namespace API.DTOs
{
    public class DeviceDto
    {
        public int Id { get; set; }
        
        [Required] public string Name { get; set; }
        
        [Required] public string Manufacturer { get; set; }
        
        [Required] public DeviceType Type { get; set; }
        
        [Required] public string OS { get; set; }
        
        [Required] public float OSVersion { get; set; }
        
        [Required] public float Processor { get; set; }
        
        [Required] public int RAM { get; set; }
    }
}