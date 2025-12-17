using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TODOAPP.Models;

namespace TODOAPP.Validators
{
    public class AddItemValidator:AbstractValidator<ItemData>
    {
        public AddItemValidator()
        {
            RuleFor(x=>x.title).NotEmpty().WithMessage("Title is required").MaximumLength(100).WithMessage("Title cannot exceed 100 characters");
            RuleFor(x=>x.description).NotEmpty().WithMessage("Description is required").MaximumLength(500).WithMessage("Description cannot exceed 500 characters");
        }
    }
}