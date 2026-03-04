using FluentValidation;
using Services.DTOs.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Validators.Event
{
    public class EventUpdateDtoValidator : AbstractValidator<EventUpdateDto>
    {
        public EventUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty();

            RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.City)
                .NotEmpty();

            RuleFor(x => x.Venue)
                .NotEmpty();

            RuleFor(x => x.EventDate)
                .GreaterThan(DateTime.UtcNow);

            RuleFor(x => x.Capacity)
                .GreaterThan(0);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);
        }
    }
}
