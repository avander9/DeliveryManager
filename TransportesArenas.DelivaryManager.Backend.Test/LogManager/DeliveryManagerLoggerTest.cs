using System;
using System.Collections;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TransportesArenas.DeliveryManager.Backend.Implementations;
using TransportesArenas.DeliveryManager.Backend.Interfaces;
using Xunit;

namespace TransportesArenas.DelivaryManager.Backend.Test.LogManager
{
    [TestClass]
    public class DeliveryManagerLoggerTest
    {
        private ILog log4Net;
        private IDeliveryManagerLogger logger;

        public DeliveryManagerLoggerTest()
        {
            this.log4Net = Substitute.For<ILog>();
            this.logger = new DeliveryManagerLogger(this.log4Net);
        }

        ~DeliveryManagerLoggerTest()
        {
            this.logger = null;
            this.log4Net = null;
        }

        [TestMethod]
        public void LogDebugMessage()
        {
            this.logger.LogMessage("TestMessage");
            
            //Debug is disable
            this.log4Net.Received(0).Debug(Arg.Any<string>());
        }

        [TestMethod]
        public void LogErrorMessage()
        {
            this.logger.LogMessage("TestMessage", LogType.Error);
            
            this.log4Net.Received(1).Error(Arg.Any<string>());
        }

        [TestMethod]
        public void LogFatalMessage()
        {
            this.logger.LogMessage("TestMessage", LogType.Fatal);
            
            this.log4Net.Received(1).Fatal(Arg.Any<string>());
        }

        [TestMethod]
        public void LogInfoMessage()
        {
            this.logger.LogMessage("TestMessage", LogType.Info);
            
            this.log4Net.Received(1).Info(Arg.Any<string>());
        }

        [TestMethod]
        public void LogWarningMessage()
        {
            this.logger.LogMessage("TestMessage", LogType.Warning);
            
            this.log4Net.Received(1).Warn(Arg.Any<string>());
        }

        [TestMethod]
        public void LogException()
        {
            this.logger.LogException("TestMessage", new Exception());
            
            this.log4Net.Received(1).FatalFormat(Arg.Any<string>(), Arg.Any<Exception>());
        }

    }
}