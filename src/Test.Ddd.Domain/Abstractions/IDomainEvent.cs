namespace Test.Ddd.Domain.Abstractions;

public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}
