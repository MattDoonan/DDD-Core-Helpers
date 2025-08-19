using ValueObjectTests.Helpers;

namespace ValueObjectTests;

// TODO write these properly... I am lazy
public class ValueObjectBaseIntTests() : ValueObjectBaseHelper<int>(5, 10, 2);
public class ValueObjectBaseLongTests() : ValueObjectBaseHelper<long>(5, 10, 2);
public class ValueObjectBaseByteTests() : ValueObjectBaseHelper<byte>(5, 10, 2);
public class ValueObjectBaseFloatTests() : ValueObjectBaseHelper<float>(5, 10, 2);
public class ValueObjectBaseStringTests() : ValueObjectBaseHelper<string>("banana", "cherry", "apple");
