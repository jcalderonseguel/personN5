using FluentValidation.Results;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Super.EWalletCore.PersonDataManagement.Application.Common.Exceptions
{
    public class ValidateException : Exception
    {
        public ValidateException()
            : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidateException(List<ValidationFailure> failures)
            : this()
        {

            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Failures { get; }
    }
    //public class ValidateException : RpcException
    //{
    //    // public IDictionary<string, string[]> Failures { get; }

    //    public ValidateException()
    //        : base(new Status(StatusCode.InvalidArgument, "One or more validation failures have occurred."), new Metadata(), "ERORRRRRR")
    //    { }

    //    public ValidateException(IList<ValidationFailure> failures)
    //        : this()
    //    {
    //        IDictionary<string, string[]> Failures = new Dictionary<string, string[]>();
    //        var propertyNames = failures
    //            .Select(e => e.PropertyName)
    //            .Distinct();

    //        foreach (var propertyName in propertyNames)
    //        {
    //            var propertyFailures = failures
    //                .Where(e => e.PropertyName == propertyName)
    //                .Select(e => e.ErrorMessage)
    //                .ToArray();

    //            Failures.Add(propertyName, propertyFailures);
    //        }

    //        foreach (var item in Failures)
    //        {
    //            var failedValues = String.Concat(item.Value);
    //            Trailers.Add(new Metadata.Entry(item.Key.Replace("[", "").Replace("]", ""), failedValues));
    //        }

    //    }
    //}
}
