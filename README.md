# WeatherAlert
## Configuration
After first start program generates basic `config.json`
```
{
  "OWMAPIKey":"",
  "City":"Odessa,UA",
  "Lang":"ru",
  "GmailUsername":"",
  "GmailPassword":""
}
```
### Required
`OWMAPIKey` - API key from https://home.openweathermap.org/api_keys

`GmailUsername` and `GmailPassword` - login credentials for https://gmail.com
### Optional
`City` - city name and country code divided by comma, use ISO 3166 country codes

`Lang` - Arabic - ar, Bulgarian - bg, Catalan - ca, Czech - cz, German - de, Greek - el, English - en, Persian (Farsi) - fa, Finnish - fi, French - fr, Galician - gl, Croatian - hr, Hungarian - hu, Italian - it, Japanese - ja, Korean - kr, Latvian - la, Lithuanian - lt, Macedonian - mk, Dutch - nl, Polish - pl, Portuguese - pt, Romanian - ro, Russian - ru, Swedish - se, Slovak - sk, Slovenian - sl, Spanish - es, Turkish - tr, Ukrainian - ua, Vietnamese - vi, Chinese Simplified - zh_cn, Chinese Traditional - zh_tw.

## Limitations
Works only if gmail account's language is set to Russian.
