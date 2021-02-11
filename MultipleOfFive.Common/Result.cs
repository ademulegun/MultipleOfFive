using System;

namespace MultipleOfFive.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string Error { get; }
        public string Timestamp { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, string error)
        {
            if (isSuccess && !string.IsNullOrEmpty(error))
                throw new InvalidOperationException();
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        }

        public static Result<T> Fail<T>(T value, string message)
        {
            return new Result<T>(value, false, message);
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        public static Result Combine(params Result[] results)
        {
            foreach (var result in results)
            {
                if (result.IsFailure)
                    return result;
            }
            return Ok();
        }

        public static Result Combine<T>(params Result[] results)
        {
            foreach (var result in results)
            {
                if (result.IsFailure)
                    return Fail<T>(result.Error);
            }
            return Result.Ok();
        }
    }

    public class Result<T> : Result
    {
        private readonly T _value;
        public T Value
        {
            get
            {
                return _value;
            }
        }

        protected internal Result(T value, bool isSuccess, string error) : base(isSuccess, error)
        {
            _value = value;
        }
    }
}
