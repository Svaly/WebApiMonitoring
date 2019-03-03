using Framework.Service.Cqrs;

namespace Identity.Handlers.Command
{
    public sealed class RegisterUserCommand : ICommand
    {
        public RegisterUserCommand(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public string Name { get; }

        public string Password{ get; }
    }
}
