# timeoutremotedesktop

## Syntax

```G1ANT
♥timeoutremotedesktop = ⟦timespan⟧
```

## Description

Determines the timeout value (in ms) for the [vnc.connect](G1ANT.Addon/G1ANT.Addon.Net/G1ANT.Addon.Net/Commands/VncConnectCommand.md) command; the default value is 10000 (10 seconds).

## Example

```G1ANT
♥timeoutremotedesktop = 10
vnc.connect host 10.0.0.1 port 5901 password vncpassword
```

In this example the 10ms timeout value is too short to connect to the VNC server, so an error message appears.

