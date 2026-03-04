using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Concrete
{
    public class ValidationService : IValidationService
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ValidateAsync<T>(T model)
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();
            if (validator is null) return;

            var result = await validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

        }
    }
}
