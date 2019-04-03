# timeoutwatson

## Syntax

```G1ANT
♥timeoutwatson = ⟦timespan⟧
```

## Description

Determines the timeout value (in ms) for the `watson.` commands; the default value is 60000 (60 seconds).

## Example

```G1ANT
♥timeoutwatson = 100
♥apiKey = Enter your apikey here
♥serverUri = https://gateway-lon.watsonplatform.net/speech-to-text/api
watson.speechtotext C:\Test\speech.mp3 apikey ♥apiKey serveruri ♥serverUri
dialog ♥result
```

In the example above the 100ms timeout is not enough to complete speech recognition.