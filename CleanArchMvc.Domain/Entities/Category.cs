using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; }
        public ICollection<Product> Products { get; set; }

        public Category(string name)
        {
           ValidateDomain(name);
        }

        public Category(int id, string name)
        {
            DomainExceptionvalidation.When(id < 0, "Invalid Id value");
            Id = id;
            ValidateDomain(name);
        }

        public void Update(string name)
        {
            ValidateDomain(name);
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionvalidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
            DomainExceptionvalidation.When(name.Length < 3, "Invalid name. Too short, minimum 3 characters");
            Name = name;
        }
    }
}
