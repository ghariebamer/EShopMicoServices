using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommandHandler<in Tcommand> : IRequestHandler<Tcommand, Unit>
    where Tcommand : ICommand<Unit>
{
}

public interface ICommandHandler<in Tcommand, TResponse> : IRequestHandler<Tcommand, TResponse>
    where Tcommand : ICommand<TResponse>
   where TResponse : notnull
{
}
