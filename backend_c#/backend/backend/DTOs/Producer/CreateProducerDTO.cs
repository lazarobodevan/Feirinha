﻿using backend.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Producer {
    public class CreateProducerDTO {

        [Required]
        public String Name { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        public String Password { get; set; }

        [Required]
        public String OriginCity { get; set; }

        [Required]
        public String Telephone { get; set; }

        [Column("Picture")]
        public byte[]? Picture { get; set; }

        [Required]
        public String CPF { get; set; }

        [Required]
        public String Attended_Cities { get; set; }

        [Required]
        public String WhereToFind { get; set; }

        public List<Models.Product>? Products { get; set; }
        public List<Order>? Orders { get; set; }
        public List<ConsumerFavProducer> FavdByConsumers { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
