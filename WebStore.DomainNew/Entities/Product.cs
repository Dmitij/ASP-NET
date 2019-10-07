using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public int SectionId { get; set; }
        public int? BrandId { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        //public string Manufacturer { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")] // не обязательный
        public virtual Category Category { get; set; }

        [ForeignKey("BrandId")] // не обязательный
        public virtual Brand Brand { get; set; }
    }
}
