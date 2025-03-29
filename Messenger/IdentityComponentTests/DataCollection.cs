using Pingo.Messages.ComponentTests;

namespace IdentityComponentTests;

[CollectionDefinition(DatabaseCollection.NonParallelTests, DisableParallelization = true)]
public sealed class DataCollection : ICollectionFixture<WebAppFactoryFixture>;
