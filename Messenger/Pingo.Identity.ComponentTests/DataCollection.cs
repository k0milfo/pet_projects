namespace Pingo.Identity.ComponentTests;

[CollectionDefinition(DatabaseCollection.NonParallelTests, DisableParallelization = true)]
public sealed class DataCollection : ICollectionFixture<WebAppFactoryFixture>;
