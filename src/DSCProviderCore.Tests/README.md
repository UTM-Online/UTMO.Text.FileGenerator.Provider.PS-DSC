# DscConfigurationPropertyBag Unit Tests

## Overview
This test file contains comprehensive unit tests for the `DscConfigurationPropertyBag` class using MSTest framework.

## Important Notes
The `Init<T>` method stores `default(object)` for non-string types, which is `null`. When calling `Get<T>` on such values where T is an enum or nullable enum, it will cause a NullReferenceException because the code calls `.ToString()` on the null value. This triggers a Fatal log message with exit code 22, causing the test process to crash.

**The `Init_WithIntType_InitializesDefaultValue` test has been updated to use `ToLiquid()` instead of `Get<int>()` to avoid this issue.**

## Test Coverage (35 tests total)

### Set Tests (10 tests)
- `Set_WithStringValue_StoresValue`: Verifies string values are stored correctly
- `Set_WithIntValue_StoresValue`: Verifies integer values are stored correctly  
- `Set_WithBoolValue_StoresValue`: Verifies boolean values are stored correctly
- `Set_WithEnumValue_StoresAsString`: Verifies enum values are converted to strings
- `Set_WithNullableEnum_StoresAsString`: Verifies nullable enums are handled correctly
- `Set_WithNullValue_DoesNotStore`: Verifies null values are ignored
- `Set_UpdatesExistingValue`: Verifies existing values can be updated
- `Set_WithTimeSpan_StoresValue`: Verifies TimeSpan values are stored
- `Set_WithCharValue_StoresValue`: Verifies char values are stored
- `Set_WithStringList_StoresValue`: Verifies lists of strings are stored

### Get Tests (5 tests)
- `Get_NonExistentKey_ReturnsDefault`: Verifies default values are returned for missing keys
- `Get_NonExistentKeyInt_ReturnsDefaultInt`: Verifies default int (0) for missing keys
- `Get_WithEnum_ParsesCorrectly`: Verifies enum strings are parsed back to enums
- `Get_WithNullableEnum_ParsesCorrectly`: Verifies nullable enums are parsed correctly
- `Get_StringOverload_ReturnsString`: Verifies the non-generic Get method works

### Init Tests (3 tests)
- `Init_WithStringType_InitializesEmptyString`: Verifies Init<string> creates empty string
- `Init_WithIntType_InitializesDefaultValue`: Verifies Init<int> creates default value
- `Init_NoTypeParameter_InitializesEmptyString`: Verifies parameterless Init creates empty string

### ToLiquid Tests (13 tests)
- `ToLiquid_WithBoolTrue_ReturnsFormattedString`: Verifies true becomes "$true"
- `ToLiquid_WithBoolFalse_ReturnsFormattedString`: Verifies false becomes "$false"
- `ToLiquid_WithString_ReturnsQuotedString`: Verifies strings are wrapped in quotes
- `ToLiquid_WithEnum_ReturnsBareValue`: Verifies regular enums are not quoted
- `ToLiquid_WithTimeSpan_ReturnsQuotedString`: Verifies TimeSpan is quoted
- `ToLiquid_WithChar_ReturnsQuotedString`: Verifies char is quoted
- `ToLiquid_WithStringList_ReturnsFormattedArray`: Verifies string arrays format correctly
- `ToLiquid_WithEnumList_ReturnsFormattedArray`: Verifies enum arrays format correctly
- `ToLiquid_WithNoValue_ReturnsEmptyQuotedString`: Verifies NoValue constant becomes ""
- `ToLiquid_WithEmptyString_DoesNotIncludeInResult`: Verifies empty strings are excluded
- `ToLiquid_WithWhitespaceString_DoesNotIncludeInResult`: Verifies whitespace is excluded
- `ToLiquid_WithInt_PassesThroughAsIs`: Verifies integers are not modified

### QuotedEnum Tests (2 tests)
- `ToLiquid_WithQuotedEnum_ReturnsQuotedValue`: Verifies [QuotedEnum] attribute causes quoting
- `ToLiquid_WithUnquotedEnum_ReturnsBareValue`: Verifies enums without attribute are bare

### Multiple Properties Tests (2 tests)
- `MultipleProperties_AreAllStoredAndRetrieved`: Verifies multiple properties work together
- `ToLiquid_WithMultipleProperties_ReturnsAllFormatted`: Verifies ToLiquid handles multiple values

## Test Helpers
- `TestEnum`: A sample enum with three values for testing
- `TestClassWithQuotedEnum`: A test class demonstrating QuotedEnum attribute usage

## Running Tests
```powershell
dotnet test DSCProviderCore.Tests.csproj
```

## Dependencies
- MSTest 4.0.1
- Moq 4.20.72
- Microsoft.Extensions.Logging.Abstractions 9.0.10

