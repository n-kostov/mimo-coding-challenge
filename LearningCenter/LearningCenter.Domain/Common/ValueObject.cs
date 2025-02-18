namespace LearningCenter.Domain.Common
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(17, (current, obj) => current * 59 + (obj?.GetHashCode() ?? 0));
        }

        public static bool operator ==(ValueObject first, ValueObject second) => first.Equals(second);

        public static bool operator !=(ValueObject first, ValueObject second) => !(first == second);
    }
}
