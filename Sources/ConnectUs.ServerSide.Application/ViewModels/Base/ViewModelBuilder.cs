using System;
using System.Linq;
using System.Reflection;
using GalaSoft.MvvmLight.Ioc;

namespace ConnectUs.ServerSide.Application.ViewModels.Base
{
    public class ViewModelBuilder : IViewModelBuilder
    {
        public ClientCommandViewModel BuildNewClientCommandViewModel()
        {
            return BuildNew<ClientCommandViewModel>();
        }

        private static T BuildNew<T>()
        {
            var type = typeof (T);
            var typeInfo = type.GetTypeInfo();
            foreach (var ctor in typeInfo.DeclaredConstructors) {
                var parameters = ctor
                    .GetParameters()
                    .Select(x => SimpleIoc.Default.GetInstance(x.ParameterType))
                    .ToArray();
                return (T) ctor.Invoke(parameters);
            }
            throw new Exception("Unable to build new viewmodel");
        }
    }
}