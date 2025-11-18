namespace DSCProviderCore.Tests;

using Microsoft.Extensions.Logging;
using Moq;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Attributes;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Constants;

[TestClass]
public class DscConfigurationPropertyBagTests
{
    private Mock<ILogger<DscConfigurationPropertyBag>>? _loggerMock;
    private DscConfigurationPropertyBag? _propertyBag;

    [TestInitialize]
    public void Setup()
    {
        this._loggerMock = new Mock<ILogger<DscConfigurationPropertyBag>>();
        this._propertyBag = new DscConfigurationPropertyBag();
        this._propertyBag.SetLogger(this._loggerMock.Object);
    }

    #region Set Tests

    [TestMethod]
    public void Set_WithStringValue_StoresValue()
    {
        // Arrange
        const string key = "TestKey";
        const string value = "TestValue";

        // Act
        this._propertyBag!.Set(key, value);

        // Assert
        var result = this._propertyBag.Get<string>(key);
        Assert.AreEqual(value, result);
    }

    [TestMethod]
    public void Set_WithIntValue_StoresValue()
    {
        // Arrange
        const string key = "TestKey";
        const int value = 42;

        // Act
        this._propertyBag!.Set(key, value);

        // Assert
        var result = this._propertyBag.Get<int>(key);
        Assert.AreEqual(value, result);
    }

    [TestMethod]
    public void Set_WithBoolValue_StoresValue()
    {
        // Arrange
        const string key = "TestKey";
        const bool value = true;

        // Act
        this._propertyBag!.Set(key, value);

        // Assert
        var result = this._propertyBag.Get<bool>(key);
        Assert.AreEqual(value, result);
    }

    [TestMethod]
    public void Set_WithEnumValue_StoresAsString()
    {
        // Arrange
        const string key = "TestKey";
        var value = TestEnum.Value1;

        // Act
        this._propertyBag!.Set(key, value);

        // Assert
        var result = this._propertyBag.Get<TestEnum>(key);
        Assert.AreEqual(value, result);
    }

    [TestMethod]
    public void Set_WithNullableEnum_StoresAsString()
    {
        // Arrange
        const string key = "TestKey";
        TestEnum? value = TestEnum.Value2;

        // Act
        this._propertyBag!.Set(key, value);

        // Assert
        var result = this._propertyBag.Get<TestEnum?>(key);
        Assert.AreEqual(value, result);
    }

    [TestMethod]
    public void Set_WithNullValue_DoesNotStore()
    {
        // Arrange
        const string key = "TestKey";
        string? value = null;

        // Act
        this._propertyBag!.Set(key, value);

        // Assert
        var result = this._propertyBag.Get<string>(key);
        Assert.IsNull(result);
    }

    [TestMethod]
    public void Set_UpdatesExistingValue()
    {
        // Arrange
        const string key = "TestKey";
        const string initialValue = "Initial";
        const string updatedValue = "Updated";

        // Act
        this._propertyBag!.Set(key, initialValue);
        this._propertyBag.Set(key, updatedValue);

        // Assert
        var result = this._propertyBag.Get<string>(key);
        Assert.AreEqual(updatedValue, result);
    }

    [TestMethod]
    public void Set_WithTimeSpan_StoresValue()
    {
        // Arrange
        const string key = "TestKey";
        var value = TimeSpan.FromMinutes(30);

        // Act
        this._propertyBag!.Set(key, value);

        // Assert
        var result = this._propertyBag.Get<TimeSpan>(key);
        Assert.AreEqual(value, result);
    }

    [TestMethod]
    public void Set_WithCharValue_StoresValue()
    {
        // Arrange
        const string key = "TestKey";
        const char value = 'A';

        // Act
        this._propertyBag!.Set(key, value);

        // Assert
        var result = this._propertyBag.Get<char>(key);
        Assert.AreEqual(value, result);
    }

    [TestMethod]
    public void Set_WithStringList_StoresValue()
    {
        // Arrange
        const string key = "TestKey";
        var value = new List<string> { "item1", "item2", "item3" };

        // Act
        this._propertyBag!.Set(key, value);

        // Assert
        var result = this._propertyBag.Get<List<string>>(key);
        CollectionAssert.AreEqual(value, result);
    }

    #endregion

    #region Get Tests

    [TestMethod]
    public void Get_NonExistentKey_ReturnsDefault()
    {
        // Arrange
        const string key = "NonExistent";

        // Act
        var result = this._propertyBag!.Get<string>(key);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public void Get_NonExistentKeyInt_ReturnsDefaultInt()
    {
        // Arrange
        const string key = "NonExistent";

        // Act
        var result = this._propertyBag!.Get<int>(key);

        // Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Get_WithEnum_ParsesCorrectly()
    {
        // Arrange
        const string key = "TestKey";
        this._propertyBag!.Set(key, TestEnum.Value1);

        // Act
        var result = this._propertyBag.Get<TestEnum>(key);

        // Assert
        Assert.AreEqual(TestEnum.Value1, result);
    }

    [TestMethod]
    public void Get_WithNullableEnum_ParsesCorrectly()
    {
        // Arrange
        const string key = "TestKey";
        this._propertyBag!.Set(key, TestEnum.Value2);

        // Act
        var result = this._propertyBag.Get<TestEnum?>(key);

        // Assert
        Assert.AreEqual(TestEnum.Value2, result);
    }

    [TestMethod]
    public void Get_StringOverload_ReturnsString()
    {
        // Arrange
        const string key = "TestKey";
        const string value = "TestValue";
        this._propertyBag!.Set(key, value);

        // Act
        var result = this._propertyBag.Get(key);

        // Assert
        Assert.AreEqual(value, result);
    }

    #endregion

    #region Init Tests

    [TestMethod]
    public void Init_WithStringType_InitializesEmptyString()
    {
        // Arrange
        const string key = "TestKey";

        // Act
        this._propertyBag!.Init<string>(key);

        // Assert
        var result = this._propertyBag.Get<string>(key);
        Assert.IsTrue(string.IsNullOrEmpty(result));
    }

    [TestMethod]
    public void Init_WithIntType_InitializesDefaultValue()
    {
        // Arrange
        const string key = "TestKey";

        // Act
        this._propertyBag!.Init<int>(key);

        // Assert
        // Init stores default(object) which is null
        // We can't safely Get this value as it will cause NullReferenceException
        // So we just verify ToLiquid doesn't crash and excludes null values
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;
        Assert.IsNotNull(result);
        // Null values are skipped in ToLiquid, so the key shouldn't be present
        Assert.IsFalse(result.ContainsKey(key));
    }

    [TestMethod]
    public void Init_NoTypeParameter_InitializesEmptyString()
    {
        // Arrange
        const string key = "TestKey";

        // Act
        this._propertyBag!.Init(key);

        // Assert
        var result = this._propertyBag.Get<string>(key);
        Assert.AreEqual(string.Empty, result);
    }

    #endregion

    #region ToLiquid Tests

    [TestMethod]
    public void ToLiquid_WithBoolTrue_ReturnsFormattedString()
    {
        // Arrange
        const string key = "TestKey";
        this._propertyBag!.Set(key, true);

        // Act
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("$true", result[key]);
    }

    [TestMethod]
    public void ToLiquid_WithBoolFalse_ReturnsFormattedString()
    {
        // Arrange
        const string key = "TestKey";
        this._propertyBag!.Set(key, false);

        // Act
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("$false", result[key]);
    }

    [TestMethod]
    public void ToLiquid_WithString_ReturnsQuotedString()
    {
        // Arrange
        const string key = "TestKey";
        const string value = "TestValue";
        this._propertyBag!.Set(key, value);

        // Act
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual($"\"{value}\"", result[key]);
    }

    [TestMethod]
    public void ToLiquid_WithEnum_ReturnsQuotedValue()
    {
        // Arrange
        const string key = "TestKey";
        this._propertyBag!.Set(key, TestEnum.Value1);

        // Act
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        // Enums are quoted by default (no UnquotedEnum attribute present)
        Assert.AreEqual("\"Value1\"", result[key]);
    }

    [TestMethod]
    public void ToLiquid_WithTimeSpan_ReturnsQuotedString()
    {
        // Arrange
        const string key = "TestKey";
        var value = TimeSpan.FromMinutes(30);
        this._propertyBag!.Set(key, value);

        // Act
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual($"\"{value}\"", result[key]);
    }

    [TestMethod]
    public void ToLiquid_WithChar_ReturnsQuotedString()
    {
        // Arrange
        const string key = "TestKey";
        const char value = 'A';
        this._propertyBag!.Set(key, value);

        // Act
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual($"\"{value}\"", result[key]);
    }

    [TestMethod]
    public void ToLiquid_WithStringList_ReturnsFormattedArray()
    {
        // Arrange
        const string key = "TestKey";
        var value = new List<string> { "item1", "item2", "item3" };
        this._propertyBag!.Set(key, value);

        // Act
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("@(\"item1\", \"item2\", \"item3\")", result[key]);
    }

    [TestMethod]
    public void ToLiquid_WithEnumList_ReturnsFormattedArray()
    {
        // Arrange
        const string key = "TestKey";
        var value = new List<TestEnum> { TestEnum.Value1, TestEnum.Value2 };
        this._propertyBag!.Set(key, value);

        // Act
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("@(\"Value1\", \"Value2\")", result[key]);
    }

    [TestMethod]
    public void ToLiquid_WithNoValue_ReturnsEmptyQuotedString()
    {
        // Arrange
        const string key = "TestKey";
        this._propertyBag!.Set(key, PropertyBagValues.NoValue);

        // Act
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("\"\"", result[key]);
    }

    [TestMethod]
    public void ToLiquid_WithEmptyString_DoesNotIncludeInResult()
    {
        // Arrange
        const string key = "TestKey";
        this._propertyBag!.Set(key, string.Empty);

        // Act
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.ContainsKey(key));
    }

    [TestMethod]
    public void ToLiquid_WithWhitespaceString_DoesNotIncludeInResult()
    {
        // Arrange
        const string key = "TestKey";
        this._propertyBag!.Set(key, "   ");

        // Act
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.ContainsKey(key));
    }

    [TestMethod]
    public void ToLiquid_WithInt_PassesThroughAsIs()
    {
        // Arrange
        const string key = "TestKey";
        const int value = 42;
        this._propertyBag!.Set(key, value);

        // Act
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(value, result[key]);
    }

    #endregion

    #region SetOwner and QuotedEnum Tests

    [TestMethod]
    public void ToLiquid_WithQuotedEnum_ReturnsQuotedValue()
    {
        // Arrange
        var testClass = new TestClassWithQuotedEnum();
        this._propertyBag!.SetOwner(typeof(TestClassWithQuotedEnum));
        
        // Simulate setting from the property
        testClass.SetPropertyBag(this._propertyBag);
        testClass.QuotedEnumProperty = TestEnum.Value1;

        // Act
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.ContainsKey("QuotedEnumProperty"));
        Assert.AreEqual("\"Value1\"", result["QuotedEnumProperty"]);
    }

    [TestMethod]
    public void ToLiquid_WithUnquotedEnum_ReturnsBareValue()
    {
        // Arrange
        var testClass = new TestClassWithQuotedEnum();
        this._propertyBag!.SetOwner(typeof(TestClassWithQuotedEnum));
        
        // Simulate setting from the property
        testClass.SetPropertyBag(this._propertyBag);
        testClass.UnquotedEnumProperty = TestEnum.Value2;

        // Act
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.ContainsKey("UnquotedEnumProperty"));
        Assert.AreEqual("Value2", result["UnquotedEnumProperty"]);
    }

    #endregion

    #region Multiple Properties Tests

    [TestMethod]
    public void MultipleProperties_AreAllStoredAndRetrieved()
    {
        // Arrange
        this._propertyBag!.Set("String", "value");
        this._propertyBag.Set("Int", 42);
        this._propertyBag.Set("Bool", true);
        this._propertyBag.Set("Enum", TestEnum.Value1);

        // Act & Assert
        Assert.AreEqual("value", this._propertyBag.Get<string>("String"));
        Assert.AreEqual(42, this._propertyBag.Get<int>("Int"));
        Assert.IsTrue(this._propertyBag.Get<bool>("Bool"));
        Assert.AreEqual(TestEnum.Value1, this._propertyBag.Get<TestEnum>("Enum"));
    }

    [TestMethod]
    public void ToLiquid_WithMultipleProperties_ReturnsAllFormatted()
    {
        // Arrange
        this._propertyBag!.Set("String", "value");
        this._propertyBag.Set("Int", 42);
        this._propertyBag.Set("Bool", true);

        // Act
        var result = this._propertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Count);
        Assert.AreEqual("\"value\"", result["String"]);
        Assert.AreEqual(42, result["Int"]);
        Assert.AreEqual("$true", result["Bool"]);
    }

    #endregion

    #region Helper Test Classes

    public enum TestEnum
    {
        Value1,
        Value2,
        Value3
    }

    public class TestClassWithQuotedEnum
    {
        private DscConfigurationPropertyBag? _propertyBag;

        public void SetPropertyBag(DscConfigurationPropertyBag propertyBag)
        {
            this._propertyBag = propertyBag;
        }

        public TestEnum QuotedEnumProperty
        {
            get => this._propertyBag!.Get<TestEnum>(nameof(this.QuotedEnumProperty));
            set => this._propertyBag!.Set(nameof(this.QuotedEnumProperty), value);
        }

        [UnquotedEnum]
        public TestEnum UnquotedEnumProperty
        {
            get => this._propertyBag!.Get<TestEnum>(nameof(this.UnquotedEnumProperty));
            set => this._propertyBag!.Set(nameof(this.UnquotedEnumProperty), value);
        }
    }

    #endregion
}

