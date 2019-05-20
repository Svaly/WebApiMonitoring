namespace Identity.Handlers.Contracts.Command
{
    public sealed class RegisterUserCommand : Framework.Patterns.Cqrs.Implementation.Command
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
