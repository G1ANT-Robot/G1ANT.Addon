# timeoutconnect

## Syntax

```G1ANT
♥timeoutconnect = ⟦timespan⟧
```

## Description

Determines the timeout value (in ms) for the [is.accessible](G1ANT.Addon/G1ANT.Addon.Net/G1ANT.Addon.Net/Commands/IsAccessibleCommand.md) and [ping](G1ANT.Addon/G1ANT.Addon.Net/G1ANT.Addon.Net/Commands/PingCommand.md) commands; the default value is 1000 (1 second).

## Example

```G1ANT
♥timeoutconnect = 20
ping microsoft.com
```

In this example the 20ms timeout value is too short to get a server response, so an error message appears.

