using System;
using Topshelf;

using Starter.Bootstrapper;
using Starter.Data.Consumers;

namespace Starter.MessageBroker.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Setup.Bootstrap();

            var consumer = IocWrapper.Instance.GetService<IMessageBrokerConsumer>();

            var rc = HostFactory.Run(x =>
            {
                x.Service<IMessageBrokerConsumer>(s =>
                {
                    s.ConstructUsing(name => consumer);
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("MessageBroker Consumer");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
