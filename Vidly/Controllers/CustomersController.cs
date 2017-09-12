using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult details(int id)
        {

            var customer = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return new HttpNotFoundResult();
            return View(customer);
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer=new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
 
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer=customer,
                    MembershipTypes=_context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            else
            {
                //this action will be used to both create new customer and update an existing one
                //here we have to know what to do:
                if(customer.Id==0)
                    _context.Customers.Add(customer);
                else
                {
                    var customerInDb = _context.Customers.Single(c=>c.Id==customer.Id);
    
                    customerInDb.Name = customer.Name;
                    customerInDb.MembershipTypeId = customer.MembershipTypeId;
                    customerInDb.IsSuscbribedToNewsletter = customer.IsSuscbribedToNewsletter;
                    customerInDb.Birthdate = customer.Birthdate;
    
    
                }
    
                _context.SaveChanges();

                
                return RedirectToAction("Index","Customers");
            }

        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm",viewModel);
        }

    }
}