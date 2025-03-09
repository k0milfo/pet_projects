namespace Pingo.Messages.ComponentTests;

[CollectionDefinition(DatabaseCollection.NonParallelTests, DisableParallelization = true)]
public sealed class DataCollection : ICollectionFixture<WebAppFactoryFixture>;
