# XML Truncator-Fixer

Finds the first XML error in a file, truncates just before it, then uses xmllint to add correct closing tags. Configurable truncation offset (default: 50 characters before the error).

**Language:** Perl

## Features

- Locates the first XML parsing error
- Truncates the file just before the error point
- Uses xmllint to add proper closing tags
- Configurable truncation offset

## Origin

Migrated from [SourceForge](https://sourceforge.net/projects/xmltrncatorfixr/) via [SF2GH Migrator](https://github.com/socrtwo/sf-to-github).

## License

MIT License — see [LICENSE](LICENSE) for details.
