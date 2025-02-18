namespace LearningCenter.Domain.Exceptions
{
    public abstract class BaseDomainException : Exception
    {
        public string Error { get; private set; }

        protected BaseDomainException(string message = "")
            : base(message)
        {
            Error = message;
        }

        public override string ToString() => Error;
    }
}
