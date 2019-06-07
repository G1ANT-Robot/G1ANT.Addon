# timeoutrest

## Syntax

```G1ANT
♥timeoutrest = ⟦timespan⟧
```

## Description

Determines the timeout value (in ms) for the [rest](G1ANT.Addon/G1ANT.Addon.Net/G1ANT.Addon.Net/Commands/RestCommand.md) command; the default value is 5000 (5 seconds).

## Example

```G1ANT
♥timeoutrest = 500
list.create type1:xml result ♥list1
list.create par1:value result ♥list2

rest get url https://httpbin.org/get headers ♥list1 parameters ♥list2
dialog ♥result
dialog ♥status
```

In this example the 500ms timeout value is too short to get a server response, so an error message appears.

