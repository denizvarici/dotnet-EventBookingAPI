using FluentValidation;
using Services.DTOs.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Validators.Event
{
    public class EventCreateDtoValidator : AbstractValidator<EventCreateDto>
    {
        public EventCreateDtoValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty.")
            .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty.");

            RuleFor(x => x.City)
                .NotEmpty();

            RuleFor(x => x.Venue)
                .NotEmpty();

            RuleFor(x => x.EventDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Event date must be in the future.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0)
                .WithMessage("Capacity must be greater than 0.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price cannot be negative.");
        }
    }
}
