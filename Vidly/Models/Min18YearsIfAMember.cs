using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.DTOs;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId==MembershipType.Unknown 
                || customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.Birthdate == null)
                return new ValidationResult("Must enter Date of Birth");

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            return (age >= 18) 
                ? ValidationResult.Success 
                : new ValidationResult("Customer needs to be older than 18 to go on a Membership");
        }
    }
}