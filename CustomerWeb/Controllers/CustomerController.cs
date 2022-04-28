using Microsoft.AspNetCore.Mvc;
using CustomerData.Models;
using CustomerData.Repositories;
using System;
using System.Linq;

namespace CustomerWeb.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository customerRepository;
        
        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public IActionResult Index()
        {
            var customerList = this.customerRepository.FindAll().ToList();
            return View(customerList);
        }

        public IActionResult Details(Guid id)
        {
            var customer = this.customerRepository.FindByPrimaryKey(id);
            ViewData["Customer"] = customer;
            return View();
        }

        public IActionResult Delete(Guid id)
        {
            var customer = this.customerRepository.Delete(id);

            return RedirectToAction("Index");
        }

        public IActionResult New()
        {
            ViewData["Action"] = "New";
            return View("Form", new CustomerEntity());
        }

        public IActionResult Edit(Guid id)
        {
            ViewData["Action"] = "Edit";
            var customer = this.customerRepository.FindByPrimaryKey(id);
            return View("Form", customer);
        }

        public IActionResult Save(string action, CustomerEntity customer)
        {
            ViewData["Action"] = action;
            if (ModelState.IsValid)
            {
                if (action.ToLower().Equals("new"))
                {
                    customerRepository.Insert(customer);
                    return RedirectToAction("Index");
                }
                else if (action.ToLower().Equals("edit"))
                {
                    customerRepository.Update(customer);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View("Form", customer);
            }
        }

    }
}
