using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Device
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Manufacturer { get; set; }
        
        [Required]
        public string Type { get; set; }
        
        [Required]
        public string OS { get; set; }
        
        [Required]
        public float OSVersion { get; set; }
        
        [Required]
        public float Processor { get; set; }
        
        [Required]
        public int RAM { get; set; }
    }
}