namespace ConnectUs.ServerSide.Command
{
    public class CommandArgument
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public CommandArgument(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override bool Equals(object obj)
        {
            var argument = obj as CommandArgument;
            if (argument == null) {
                return base.Equals(obj);
            }
            return argument.Name == Name && argument.Value == Value;
        }
    }
}