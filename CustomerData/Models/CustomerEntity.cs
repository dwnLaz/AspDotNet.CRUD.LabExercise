using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CustomerData.Models
{
    public class CustomerEntity : BaseEntity
    {
        [Required]
        [MinLength(1)]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(25)]
        public string LastName { get; set; }

        public string? Address { get; set; }

        [CheckValidPhone]
        public string? Phone { get; set; }
    }

    public class CheckValidPhone : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if(value != null)
            {
                Regex r = new Regex("^\\d{9}$");
                bool validPhoneChecker = r.IsMatch((string)value);
                if (validPhoneChecker || value == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
            
        }
        public CheckValidPhone()
        {
            ErrorMessage = "Phone Number pattern doesn't match. Phone Number should have 9 numbers";
        }
    }
}
