using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace TODOAPP.Validators
{
    public class GetItemByIdValidator:AbstractValidator<int>
    {
        public GetItemByIdValidator()
        {
            RuleFor(x=>x).ExclusiveBetween(0, int.MaxValue);
        }   
    }
}