﻿namespace LearningCenter.Domain.Exceptions
{
    public class InvalidChapterException : BaseDomainException
    {
        public InvalidChapterException()
        {
        }

        public InvalidChapterException(string error) => this.Error = error;
    }
}
