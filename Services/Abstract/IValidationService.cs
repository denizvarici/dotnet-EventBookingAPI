using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstract
{
    public interface IValidationService
    {
        Task ValidateAsync<T>(T model);
    }
}
