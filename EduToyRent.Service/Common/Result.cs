using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EduToyRent.Service.Common
{
    public class Result
    {
        private Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        private Result(bool isSuccess, List<Error> errors)
        {
            if (isSuccess && (errors == null || errors.Count > 0) ||
                !isSuccess && (errors != null && errors.Count == 0))
            {
                throw new ArgumentException("Invalid errors", nameof(errors));
            }

            IsSuccess = isSuccess;
            Errors = errors;
        }

        private Result(bool isSuccess, object obj, List<object> objects)
        {
            if (isSuccess && (obj == null && (objects == null || objects.Count > 0)) ||
                !isSuccess && (obj != null || (objects != null && objects.Count == 0)))
            {
                throw new ArgumentException("Invalid parameters for object and list of objects", nameof(objects));
            }

            IsSuccess = isSuccess;
            Object = obj;
            Objects = objects;
        }

        public bool IsSuccess { get; }

        [JsonIgnore]
        public bool IsFailure => !IsSuccess;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Error Error { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<Error> Errors { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public object Object { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<object> Objects { get; }

        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);
        public static Result Failures(List<Error> errors) => new(false, errors);
        public static Result SuccessWithObject(object obj) => new(true, obj, null);
        public static Result SuccessWithObjects(List<object> objects) => new(true, null, objects);
        public static Result FailureWithObjects(List<object> objects) => new(false, null, objects);

        public static Error CreateError(string code, string errorMessage)
        {
            return new Error(code, errorMessage);
        }


    }
}
