<!--MODERNIZED:v1-->
# Xmltrncatorfixr

> Migrated from SourceForge via SF2GH Migrator

[![Live page](https://img.shields.io/badge/live-page-ff2e93?style=for-the-badge)](https://socrtwo.github.io/xmltrncatorfixr-SF/)
[![Releases](https://img.shields.io/github/v/release/socrtwo/xmltrncatorfixr-SF?style=for-the-badge&color=7c3aed)](https://github.com/socrtwo/xmltrncatorfixr-SF/releases)
[![License](https://img.shields.io/github/license/socrtwo/xmltrncatorfixr-SF?style=for-the-badge&color=22d3ee)](https://github.com/socrtwo/xmltrncatorfixr-SF/blob/main/LICENSE)
[![Last commit](https://img.shields.io/github/last-commit/socrtwo/xmltrncatorfixr-SF?style=for-the-badge&color=34d399)](https://github.com/socrtwo/xmltrncatorfixr-SF/commits)

🌐 **Live:** https://socrtwo.github.io/xmltrncatorfixr-SF/  
📦 **Downloads:** [Releases](https://github.com/socrtwo/xmltrncatorfixr-SF/releases)  
📂 **Source:** [socrtwo/xmltrncatorfixr-SF](https://github.com/socrtwo/xmltrncatorfixr-SF)

---

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

---

## 📜 SourceForge heritage

This project originated on **SourceForge** before being migrated to GitHub. The legacy SourceForge entry, if still available, can be searched at:

🔗 https://sourceforge.net/projects/xmltrncatorfixr/

The repository here at `socrtwo/xmltrncatorfixr-SF` is the canonical, actively-maintained home. All future updates, issue tracking, and releases happen on GitHub.

## 🛠️ Contributing

Issues and pull requests are welcome at [https://github.com/socrtwo/xmltrncatorfixr-SF/issues](https://github.com/socrtwo/xmltrncatorfixr-SF/issues).

## 📝 License

See the [LICENSE](https://github.com/socrtwo/xmltrncatorfixr-SF/blob/main/LICENSE) file in this repository. If no license file is present, the project is shared as-is for reference and personal use; please contact the maintainer for other use cases.

---

*Maintained by [@socrtwo](https://github.com/socrtwo)*