namespace CleanArchMvc.Domain.Validation
{
    public class DomainExceptionvalidation : Exception
    {
        public DomainExceptionvalidation(string error) : base(error) { }

        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new DomainExceptionvalidation(error);
        }
    }
}
