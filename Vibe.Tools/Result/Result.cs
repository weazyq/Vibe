﻿using System.Text.Json.Serialization;

namespace Vibe.Tools.Result
{
    public partial class Result : IResult
    {
        private Error[] _errors => new Error[] { };

        public List<Error> Errors => _errors.ToList();

        public Boolean IsSuccess => Errors.IsEmpty();

        public Boolean IsFail => Errors.IsNotEmpty();

        public Result(Error? error = null)
        {
            if (error != null) AddError(error);
        }

        [JsonConstructor]
        public Result(List<Error>? errors, Boolean isSuccess, Boolean isFail)
        {
            if (errors is not null) 
                AddErrors(errors);
        }

        public void AddError(Error error)
        {
            Errors.Add(error);
        }

        public void AddErrors(List<Error> errors)
        {
            errors.ForEach(AddError);
        }

        public static Result Success = new Result() { };

        public static Result Fail(String message) 
        {
            Result result = new Result();
            result.AddError(new Error(message));

            return result;
        }

        public static Result Fail(List<Error> errors)
        {
            Result result = new Result();
            result.AddErrors(errors);
            return result;
        }
    }
}
