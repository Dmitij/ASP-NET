using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Intefaces;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<EmployeeView> _employees;

        public EmployeeService()
        {
            _employees = new List<EmployeeView>
            {
                new EmployeeView
                {
                    Id=1,
                    FirstName = "Иван",
                    LastName = "Пузиков",
                    Patronymic =  "Александрович",
                    Age = 22
                },
                new EmployeeView
                {
                    Id = 2,
                    FirstName = "Владислав",
                    LastName = "Петров",
                    Patronymic = "Иванович",
                    Age = 35
                }
            };
        }

        public IEnumerable<EmployeeView> GetAll()
        {
            return _employees;
        }

        public EmployeeView GetById(int id)
        {
            return _employees.FirstOrDefault(x => x.Id == id);
        }

        public void Commit()
        {
            //throw new NotImplementedException();
        }

        public void AddNew(EmployeeView model)
        {
            model.Id = _employees.Max(e => e.Id) + 1;
            _employees.Add(model);
        }

        public void Delete(int id)
        {
            var emp = GetById(id);
            if (emp == null)
                return;

            _employees.Remove(emp);
        }
    }
}
