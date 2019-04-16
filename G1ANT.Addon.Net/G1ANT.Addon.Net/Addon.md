# Index Of All Addon Elements


 All Commands

| Name | Description |
| ---- | ----------- |
| [as400.open](https://github.com/G1ANT-Robot/G1ANT.Addon/blob/master/G1ANT.Addon.Net/Commands/AS400OpenCommand.md) | This command opens a terminal connection to work with IBM AS/400 server |
| [is.accessible](https://github.com/G1ANT-Robot/G1ANT.Addon/blob/master/G1ANT.Addon.Net/Commands/IsAccessibleCommand.md) | This command checks if a host is accessible |
| [mail.imap](https://github.com/G1ANT-Robot/G1ANT.Addon/blob/master/G1ANT.Addon.Net/Commands/MailImapCommand.md) | This command uses the IMAP protocol to check an email inbox and allows the user to analyze their messages received within a specified time range, with the option to consider only unread messages and/or mark all of the checked ones as read |
| [mail.moveto](https://github.com/G1ANT-Robot/G1ANT.Addon/blob/master/G1ANT.Addon.Net/Commands/MailMoveToCommand.md) | This command moves selected message to another folder |
| [mail.smtp](https://github.com/G1ANT-Robot/G1ANT.Addon/blob/master/G1ANT.Addon.Net/Commands/MailSmtpCommand.md) | This command sends a mail message from a provided email address to a specified recipient |
| [ping](https://github.com/G1ANT-Robot/G1ANT.Addon/blob/master/G1ANT.Addon.Net/Commands/PingCommand.md) | This command pings a specified IP address and returns an approximate round-trip time in milliseconds |
| [rest](https://github.com/G1ANT-Robot/G1ANT.Addon/blob/master/G1ANT.Addon.Net/Commands/RestCommand.md) | This command prepares a request to a desired URL with a selected method |
| [vnc.connect](https://github.com/G1ANT-Robot/G1ANT.Addon/blob/master/G1ANT.Addon.Net/Commands/VncConnectCommand.md) | This command connects to a remote machine with a running VNC server, using a remote desktop connection |

 All Variables

| Name | Description |
| ---- | ----------- |
| [timeoutconnect](https://github.com/G1ANT-Robot/G1ANT.Addon/blob/master/G1ANT.Addon.Net/Variables/TimeoutConnectVariable.md) | Determines the timeout value (in ms) for the [is.accessible](G1ANT.Addon/G1ANT.Addon.Net/G1ANT.Addon.Net/Commands/IsAccessibleCommand.md) and [ping](G1ANT.Addon/G1ANT.Addon.Net/G1ANT.Addon.Net/Commands/PingCommand.md) commands; the default value is 1000 (1 second) |
| [timeoutmailsmtp](https://github.com/G1ANT-Robot/G1ANT.Addon/blob/master/G1ANT.Addon.Net/Variables/TimeoutMailSmtpVariable.md) | Determines the timeout value (in ms) for the [mail.smtp](G1ANT.Addon/G1ANT.Addon.Net/G1ANT.Addon.Net/Commands/MailSmtpCommand.md) command; the default value is 10000 (10 seconds) |
| [timeoutremotedesktop](https://github.com/G1ANT-Robot/G1ANT.Addon/blob/master/G1ANT.Addon.Net/Variables/TimeoutRemoteDesktopVariable.md) | Determines the timeout value (in ms) for the [vnc.connect](G1ANT.Addon/G1ANT.Addon.Net/G1ANT.Addon.Net/Commands/VncConnectCommand.md) command; the default value is 10000 (10 seconds) |
| [timeoutrest](https://github.com/G1ANT-Robot/G1ANT.Addon/blob/master/G1ANT.Addon.Net/Variables/TimeoutRestVariable.md) | Determines the timeout value (in ms) for the [rest](G1ANT.Addon/G1ANT.Addon.Net/G1ANT.Addon.Net/Commands/RestCommand.md) command; the default value is 5000 (5 seconds) |
