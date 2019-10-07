using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.ViewModels
{
    public class CategoryViewModel : INamedEntity, IOrderedEntity
    {
        public CategoryViewModel()
        {
            ChildCategories = new List<CategoryViewModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<CategoryViewModel> ChildCategories{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CategoryViewModel ParentCategory { get; set; }
    }
}
