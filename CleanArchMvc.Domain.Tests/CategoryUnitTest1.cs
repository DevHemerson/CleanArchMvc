using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class CategoryUnitTest1
{
    [Fact(DisplayName ="Create Category With Valid State")]
    public void CreateCategory_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Category(1, "Category Name");
        action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create Category With Invalid Id Value")]
    public void CreateCategory_WithInvalidIdValue_ResultObjectInvalidState()
    {
        Action action = () => new Category(-1, "Category Name");
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Id value");
    }

    [Fact(DisplayName = "Create Category With Short Name Value")]
    public void CreateCategory_WithShortNameValue_ResultObjectInvalidState()
    {
        Action action = () => new Category(1, "Ca");
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Too short, minimum 3 characters");
    }

    [Fact(DisplayName = "Create Category With Null Name Value")]
    public void CreateCategory_WithNullNameValue_ResultObjectInvalidState()
    {
        Action action = () => new Category(1, null);
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is required");
    }

    [Fact(DisplayName = "Create Category Missing Name Value")]
    public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
    {
        Action action = () => new Category(1, "");
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is required");
    }
}