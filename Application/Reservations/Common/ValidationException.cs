using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Application.Reservations.Common
{
    public class ValidationException : Exception
    {
        public ValidationException()
        {
        }

        public ValidationException(string v)
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .Select((failureGroup, index) => new { Index = index.ToString(), Errors = failureGroup.ToArray() })
                .ToDictionary(group => group.Index, group => group.Errors);
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
