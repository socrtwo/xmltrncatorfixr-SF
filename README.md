<!--MODERNIZED:v2-->
# XML Truncator-Fixer

> Repair corrupt XML and Office Open XML (docx / xlsx / pptx) files by
> truncating at the first parser error and rebuilding the closing tags.

[![Live app](https://img.shields.io/badge/live-app-ff2e93?style=for-the-badge)](https://socrtwo.github.io/xmltrncatorfixr-SF/app/)
[![Releases](https://img.shields.io/github/v/release/socrtwo/xmltrncatorfixr-SF?style=for-the-badge&color=7c3aed)](https://github.com/socrtwo/xmltrncatorfixr-SF/releases)
[![License](https://img.shields.io/github/license/socrtwo/xmltrncatorfixr-SF?style=for-the-badge&color=22d3ee)](https://github.com/socrtwo/xmltrncatorfixr-SF/blob/main/LICENSE)
[![Last commit](https://img.shields.io/github/last-commit/socrtwo/xmltrncatorfixr-SF?style=for-the-badge&color=34d399)](https://github.com/socrtwo/xmltrncatorfixr-SF/commits)

- **Try it now:** <https://socrtwo.github.io/xmltrncatorfixr-SF/app/> (runs entirely in your browser)
- **Downloads:** [Releases](https://github.com/socrtwo/xmltrncatorfixr-SF/releases)
- **Source:** [socrtwo/xmltrncatorfixr-SF](https://github.com/socrtwo/xmltrncatorfixr-SF)

---

XML was designed to be intolerant of errors. A single broken byte in a
`document.xml` part inside a `.docx` will make Word refuse to render the
whole document. This tool gives you a chance at partial recovery: it finds
the first parser error, truncates the file just before it (minus a small
safety offset because validators sometimes report errors several characters
late), then runs `xmllint --recover` to rebuild a valid closing-tag stack.

## Platform matrix

| Platform | How to run | Download |
|---|---|---|
| **Web** | Open the [live app](https://socrtwo.github.io/xmltrncatorfixr-SF/app/) | `*-web.zip` (self-host bundle) |
| **Windows** | Native WinForms app, .NET Framework 4.8 | `*-windows-x86.zip` |
| **macOS** | CLI (`xmltrncatorfixr`), uses bundled `xmllint` | `*-macos.zip` / `.tar.gz` |
| **Linux** | CLI (`xmltrncatorfixr`), needs `libxml2-utils` | `*-linux.zip` / `.tar.gz` |
| **ChromeOS** | PWA in Chrome, or CLI inside Crostini | `*-chromeos.zip` (CLI) — or install the [PWA](https://socrtwo.github.io/xmltrncatorfixr-SF/app/) |
| **Android** | Installable PWA via Chrome → "Install app" | `*-android.zip` (bundle) |
| **iOS / iPadOS** | Installable PWA via Safari → "Add to Home Screen" | `*-ios.zip` (bundle) |

All variants are produced from a single source tree by
[`.github/workflows/release.yml`](.github/workflows/release.yml).

## Features

- Locates the first XML parser error automatically
- Truncates the file just before the error point (configurable offset; default 50 bytes)
- Uses `xmllint --recover` (or the browser's DOMParser + a tag-stack walker) to add proper closing tags
- Web app also opens **`.docx` / `.xlsx` / `.pptx` / OpenDocument** archives and repairs broken parts in-place
- Works fully offline once the PWA is installed; no files are ever uploaded

## Quick start

### Web / Android / iOS / ChromeOS

Open <https://socrtwo.github.io/xmltrncatorfixr-SF/app/> in any modern
browser. To "install" as an app:

- **Android:** Chrome → ⋮ menu → *Install app*
- **iOS / iPadOS:** Safari → Share → *Add to Home Screen*
- **Desktop Chrome / Edge:** click the install icon in the address bar

### Linux / macOS / ChromeOS (CLI)

Requires `xmllint` (libxml2) and either `python3` or `perl`.

```bash
# Debian / Ubuntu / ChromeOS Crostini
sudo apt-get install -y libxml2-utils python3

# macOS (xmllint ships with the Command Line Tools)
xcode-select --install

# Run
./cli/xmltrncatorfixr.py path/to/broken.xml
./cli/xmltrncatorfixr.pl path/to/broken.xml   # Perl alternative

# Common flags
./cli/xmltrncatorfixr.py -n 100 -o repaired.xml broken.xml
./cli/xmltrncatorfixr.py --in-place broken.xml
```

### Windows

Download `xmltrncatorfixr-*-windows-x86.zip` from the [latest release](https://github.com/socrtwo/xmltrncatorfixr-SF/releases/latest),
unzip, and run `XMLTruncator-Fixer.exe`. Requires .NET Framework 4.8
(preinstalled on Windows 10 1903+ / Windows 11).

## How it works

1. Validate the file with `xmllint` (or Expat in the Python build, or
   `DOMParser` in the browser build) to find the line/column of the first
   parser error.
2. Convert that line/column to a byte offset.
3. Truncate the file at `(byte_offset − safety_offset)`.
4. Re-validate; if `xmllint` is available, run `xmllint --recover` to write
   a well-formed document with synthesised closing tags. The web build does
   this by walking the truncated content and appending `</tag>` for every
   still-open element.

## Building from source

- **Cross-platform CLI:** nothing to build — `cli/xmltrncatorfixr.py` and
  `cli/xmltrncatorfixr.pl` are runnable scripts.
- **Web / PWA:** static files under `web/`. Serve over HTTPS (GitHub Pages
  does this automatically) and the service worker takes over for offline use.
- **Windows:** `msbuild XMLTruncator-Fixer.sln /p:Configuration=Release
  /p:Platform=x86` — requires Visual Studio 2019+ with the .NET Framework 4.8
  targeting pack and the Visual Basic workload.

## System requirements

- **Web / PWA:** any browser from the last ~3 years.
- **CLI:** Python 3.8+ **or** Perl 5.10+, plus `xmllint` (libxml2).
- **Windows native:** .NET Framework 4.8 (preinstalled on Windows 10 1903+).

## Origin & heritage

This project was originally hosted on **SourceForge**
([xmltrncatorfixr](https://sourceforge.net/projects/xmltrncatorfixr/))
and was created in 2012 by Paul Pruitt as a Windows-only VB.NET application
bundling a packaged Perl validator. It was migrated to GitHub via
[SF2GH Migrator](https://github.com/socrtwo/sf-to-github) and modernized to
target all major platforms in 2026.

The repository at `socrtwo/xmltrncatorfixr-SF` is the canonical,
actively-maintained home. All future updates, issue tracking, and releases
happen on GitHub.

## Contributing

Issues and pull requests are welcome at
<https://github.com/socrtwo/xmltrncatorfixr-SF/issues>.

1. Fork this repository
2. Create a feature branch (`git checkout -b my-feature`)
3. Commit your changes
4. Push to the branch
5. Open a Pull Request

## License

MIT — see [LICENSE](LICENSE) for details.

---

*Maintained by [@socrtwo](https://github.com/socrtwo)*
