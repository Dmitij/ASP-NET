using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Entities.Base.Interfaces
{
    public interface IOrderedEntity
    {
        /// <summary>
        /// порядок
        /// </summary>
        int Order { get; set; } 
    }
}
