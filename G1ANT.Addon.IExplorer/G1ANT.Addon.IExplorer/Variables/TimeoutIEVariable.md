# timeoutie

## Syntax

```
♥timeoutie = ⟦timespan⟧
```

## Description

Determines the timeout value (in ms) for several `ie.` commands; the default value is 20000 (20 seconds).

## Example

```G1ANT
♥timeoutie = 500
ie.open g1ant.com 
```

In this example the 500ms timeout value is too short to open the browser, so an error message appears.