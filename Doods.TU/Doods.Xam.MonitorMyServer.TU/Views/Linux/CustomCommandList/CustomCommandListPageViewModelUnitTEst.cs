using System;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Repository.Std.Tables;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.CustomCommandList;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Views.Linux.CustomCommandList
{
    [TestClass]
    public class CustomCommandListPageViewModelUnitTEst : ViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var sshService = new Mock<ISshService>();
            var messageBoxService = new Mock<IMessageBoxService>();
            var obj = new CustomCommandListPageViewModel(sshService.Object, messageBoxService.Object);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.ClearCommand);
            Assert.IsNotNull(obj.ExecuteCommand);
            Assert.IsNotNull(obj.RunCommand);
            Assert.IsNull(obj.ShellBox);
        }

        [TestMethod]
        public void ExecuteCommand_NoString()
        {
            var sshService = new Mock<ISshService>();
            var messageBoxService = new Mock<IMessageBoxService>();
            var obj = new CustomCommandListPageViewModel(sshService.Object, messageBoxService.Object);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.ClearCommand);
            Assert.IsNotNull(obj.ExecuteCommand);
            Assert.IsNotNull(obj.RunCommand);
            Assert.IsNull(obj.ShellBox);

            Assert.IsTrue(obj.ExecuteCommand.CanExecute(null));
            obj.ExecuteCommand.Execute(1F);
        }

        [TestMethod]
        public void ExecuteCommand()
        {
            var sshService = new Mock<ISshService>();
            var messageBoxService = new Mock<IMessageBoxService>();
            var obj = new CustomCommandListPageViewModel(sshService.Object, messageBoxService.Object);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.ClearCommand);
            Assert.IsNotNull(obj.ExecuteCommand);
            Assert.IsNotNull(obj.RunCommand);
            Assert.IsNull(obj.ShellBox);

            Assert.IsTrue(obj.ExecuteCommand.CanExecute(null));

            obj.ExecuteCommand.Execute("ls -la");
        }

        [TestMethod]
        public void RunCommand_Null()
        {
            var sshService = new Mock<ISshService>();
            var messageBoxService = new Mock<IMessageBoxService>();
            var obj = new CustomCommandListPageViewModel(sshService.Object, messageBoxService.Object);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.ClearCommand);
            Assert.IsNotNull(obj.ExecuteCommand);
            Assert.IsNotNull(obj.RunCommand);
            Assert.IsNull(obj.ShellBox);

            Assert.IsTrue(obj.RunCommand.CanExecute(null));

            obj.RunCommand.Execute(null);
        }

        [TestMethod]
        public void RunCommand_string()
        {
            var sshService = new Mock<ISshService>();
            var messageBoxService = new Mock<IMessageBoxService>();
            var obj = new CustomCommandListPageViewModel(sshService.Object, messageBoxService.Object);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.ClearCommand);
            Assert.IsNotNull(obj.ExecuteCommand);
            Assert.IsNotNull(obj.RunCommand);
            Assert.IsNull(obj.ShellBox);

            Assert.IsTrue(obj.RunCommand.CanExecute(null));

            Assert.ThrowsException<NullReferenceException>(() => { obj.RunCommand.Execute("ls -la"); });
        }

        [TestMethod]
        public void RunCommand_CustomCommandSsh()
        {
            var sshService = new Mock<ISshService>();
            var messageBoxService = new Mock<IMessageBoxService>();
            var obj = new CustomCommandListPageViewModel(sshService.Object, messageBoxService.Object);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.ClearCommand);
            Assert.IsNotNull(obj.ExecuteCommand);
            Assert.IsNotNull(obj.RunCommand);
            Assert.IsNull(obj.ShellBox);

            Assert.IsTrue(obj.RunCommand.CanExecute(null));

            var cmd = new CustomCommandSsh
            {
                CommandString = "ls -la"
            };

            Assert.ThrowsException<NullReferenceException>(() => { obj.RunCommand.Execute(cmd); });
        }
    }
}