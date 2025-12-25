using Core.Tests.ValueObjects.TestFixtures;

namespace Core.Tests.ValueObjects;

// TODO write these properly... I am lazy
public class ValueObjectBaseIntTests() : ValueObjectTestFixture<int>(5, 10, 2);
public class ValueObjectBaseLongTests() : ValueObjectTestFixture<long>(5, 10, 2);
public class ValueObjectBaseByteTests() : ValueObjectTestFixture<byte>(5, 10, 2);
public class ValueObjectBaseFloatTests() : ValueObjectTestFixture<float>(5, 10, 2);
public class ValueObjectBaseStringTests() : ValueObjectTestFixture<string>("banana", "cherry", "apple");
