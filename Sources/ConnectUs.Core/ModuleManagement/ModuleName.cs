namespace ConnectUs.Core.ModuleManagement
{
    public class ModuleName
    {
        private readonly string _moduleName;

        public ModuleName(string moduleName)
        {
            _moduleName = moduleName;
        }

        public override bool Equals(object obj)
        {
            var moduleName = obj as ModuleName;
            if (moduleName == null) {
                return base.Equals(obj);
            }
            return string.Equals(_moduleName, moduleName._moduleName);
        }
        public override int GetHashCode()
        {
            return (_moduleName != null ? _moduleName.GetHashCode() : 0);
        }
        public override string ToString()
        {
            return _moduleName;
        }
    }
}