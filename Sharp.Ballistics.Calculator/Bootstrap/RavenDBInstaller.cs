﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;
using Raven.Client.Embedded;

namespace Sharp.Ballistics.Calculator.Bootstrap
{
    public class RavenDBInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDocumentStore>().UsingFactoryMethod(() =>
            {
                var documentStore = new EmbeddableDocumentStore
                {
                    DefaultDatabase = Constants.DatabaseName,
                    DataDirectory = Constants.DataFolder,
                    UseEmbeddedHttpServer = true
                };

                documentStore.Initialize();
                return documentStore;
            }).LifestyleSingleton());            
        }
    }
}
