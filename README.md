# XML Truncator-Fixer

Finds the first XML error in a file, truncates just before it, then uses xmllint to add correct closing tags. Configurable truncation offset (default: 50 characters before the error).

**Language:** Perl  
**License:** MIT

## Features

- Locates the first XML parsing error automatically
- Truncates the file just before the error point
- Uses xmllint to add proper closing tags
- Configurable truncation offset for fine-tuning
- Useful for repairing corrupt Office XML files

## System Requirements

- Perl 5.10 or later
- Linux, macOS, or Windows (with Strawberry Perl or WSL)

## Installation & Usage

### Running

```bash
# Install Perl (if not already installed)
# Linux/macOS: usually pre-installed
# Windows: download Strawberry Perl from https://strawberryperl.com/

# Run the script
perl <script_name>.pl [arguments]
```

### Dependencies

If the script uses CPAN modules, install them with:
```bash
cpan install Module::Name
```

## Origin

This project was originally hosted on SourceForge and has been migrated to GitHub for easier access and collaboration.

- **SourceForge:** [xmltrncatorfixr](https://sourceforge.net/projects/xmltrncatorfixr/)
- **Migrated with:** [SF2GH Migrator](https://github.com/socrtwo/sf-to-github)

## Contributing

Contributions are welcome! Feel free to:

1. Fork this repository
2. Create a feature branch (`git checkout -b my-feature`)
3. Commit your changes (`git commit -m "Add my feature"`)
4. Push to the branch (`git push origin my-feature`)
5. Open a Pull Request

## License

MIT License — see [LICENSE](LICENSE) for details.
