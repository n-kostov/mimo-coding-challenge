﻿using LearningCenter.Domain.Exceptions;
using System.Diagnostics;

namespace LearningCenter.Domain.Common
{
    public static class Guard
    {
        public static void AgainstEmptyString<TException>(string value, string name = "Value")
            where TException : BaseDomainException, new()
        {
            if (!string.IsNullOrEmpty(value))
            {
                return;
            }

            ThrowException<TException>($"{name} cannot be null ot empty.");
        }

        public static void ForStringLength<TException>(string value, int minLength, int maxLength, string name = "Value")
            where TException : BaseDomainException, new()
        {
            AgainstEmptyString<TException>(value, name);

            if (minLength <= value.Length && value.Length <= maxLength)
            {
                return;
            }

            ThrowException<TException>($"{name} must have between {minLength} and {maxLength} symbols.");
        }

        public static void AgainstOutOfRange<TException>(int number, int min, int max, string name = "Value")
            where TException : BaseDomainException, new()
        {
            if (min <= number && number <= max)
            {
                return;
            }

            ThrowException<TException>($"{name} must be between {min} and {max}.");
        }

        public static void AgainstOutOfRange<TException>(decimal number, decimal min, decimal max, string name = "Value")
            where TException : BaseDomainException, new()
        {
            if (min <= number && number <= max)
            {
                return;
            }

            ThrowException<TException>($"{name} must be between {min} and {max}.");
        }

        public static void Against<TException>(object actualValue, object unexpectedValue, string name = "Value")
            where TException : BaseDomainException, new()
        {
            if (!actualValue.Equals(unexpectedValue))
            {
                return;
            }

            ThrowException<TException>($"{name} must not be {unexpectedValue}.");
        }

        public static void AgainstGreaterThanOrEqual<TException>(int number, int target, string name = "Value")
            where TException : BaseDomainException, new()
        {
            if (number >= target)
            {
                return;
            }

            ThrowException<TException>($"{name} must be greater than {target}.");
        }

        public static void DateAfter<TException>(DateTime date, DateTime target, string name = "Value")
            where TException : BaseDomainException, new()
        {
            if (date > target)
            {
                return;
            }

            ThrowException<TException>($"{name} must be after {target}.");
        }

        private static void ThrowException<TException>(string message)
            where TException : BaseDomainException, new()
        {
            var exception = (TException)Activator.CreateInstance(typeof(TException), message)!;
            throw exception;
        }
    }
}
