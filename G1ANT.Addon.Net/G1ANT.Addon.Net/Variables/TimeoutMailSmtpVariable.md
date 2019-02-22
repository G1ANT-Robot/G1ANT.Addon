# timeoutmailsmtp

## Syntax

```G1ANT
♥timeoutmailsmtp = ⟦timespan⟧
```

## Description

Determines the timeout value (in ms) for the [mail.smtp](G1ANT.Addon/G1ANT.Addon.Net/G1ANT.Addon.Net/Commands/MailSmtpCommand.md) command; the default value is 10000 (10 seconds).

## Example

```G1ANT
♥timeoutmailsmtp = 10
mail.smtp imap.gmail.com login mail@gmail.com password p@$$w0rD from robot@gmail.com to hi@g1ant.com subject Test body ‴Hi, G1ANT!‴
```

In this example the 10ms timeout value is too short to connect to the Gmail server, so an error message appears.

