using Framework.Patterns.Cqrs.Implementation;

namespace Identity.Handlers.Commands
{
    public sealed class RegisterUserCommand : Command
    {
        public RegisterUserCommand(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public string Name { get; }

        public string Password { get; }
    }
}
