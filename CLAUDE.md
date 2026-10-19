# CLAUDE.md

Repairs corrupt XML and Office Open XML files (`.docx`, `.xlsx`, `.pptx`)
by **finding the first parser error, truncating just before it (with a
small safety offset), then running `xmllint --recover` to rebuild a valid
closing-tag stack**. Three implementations: a **PWA under `web/`**
(canonical user-facing app), a **Windows WinForms app** (.NET Framework
4.8), and a **`xmllint`-based CLI** for macOS / Linux / ChromeOS. All
produced from one source tree.

## Repo map

- `web/` — the PWA. Canonical user-facing app; powers the live page at
  `/app/` and every cross-platform release bundle.
- `XMLTruncator-Fixer/`, `XMLTruncator-Fixer.sln` — Windows WinForms
  source (.NET Framework 4.8, Visual Studio solution).
- `cli/` — the `xmltrncatorfixr` command-line tool used by the macOS /
  Linux / ChromeOS bundles. Uses `libxml2`'s `xmllint` for the recover
  pass.
- `releases/` — pre-packaged release archives committed to the repo.
- `.github/workflows/` — `build.yml` (CI), `pages.yml` (deploy `web/` to
  Pages on push to `main`), `release.yml` (build per-platform bundles on
  `v*` tag).

## Branch policy

Work on the assigned feature branch:

1. Commit and push the feature branch.
2. **Open a PR from the feature branch to `main`** using the GitHub MCP
   tools (`mcp__github__create_pull_request`). Do not merge directly —
   the maintainer reviews and merges.
3. CI runs on the PR; Pages and Release pipelines fire from `main` only.

## Releasing

- Push a `v*` tag to `main` (or use Actions → Release → workflow_dispatch)
  to produce: `*-web.zip` (self-host bundle), `*-windows-x86.zip`
  (WinForms), `*-macos.{zip,tar.gz}` and `*-linux.{zip,tar.gz}`
  (CLI + bundled `xmllint` where needed), `*-chromeos.zip` (CLI),
  `*-android.zip` and `*-ios.zip` (PWA bundles).

## Verifying changes

- PWA: open the live URL at `…/app/` or serve `web/` locally and run a
  repair on a known-corrupt `document.xml` fixture. Confirm both raw-XML
  repair and `.docx` / `.xlsx` / `.pptx` round-trip.
- Windows: build via `XMLTruncator-Fixer.sln` in Visual Studio. CI on
  `build.yml` validates this build.
- CLI: `cli/xmltrncatorfixr <file>` on Linux / macOS, requires
  `libxml2-utils` (`xmllint`) on PATH.

## Gotchas

- **The truncation safety offset is intentional.** XML validators
  sometimes report errors several characters late, so the truncator
  backs up a few bytes before cutting. Don't "fix" the offset to zero
  without understanding which validator quirk you're undoing.
- `xmllint --recover` does the heavy lifting on the CLI — when fixing a
  CLI bug, first confirm whether the bug is in our wrapper or in
  `xmllint` itself.
- The Windows app is **x86** (32-bit), not x64. The release artifact
  reflects this — `*-windows-x86.zip`. Don't change the target
  architecture; it's deliberate for compatibility with older Windows
  installs.
- The live app is served from `/app/`, not the repo root. Pages-relative
  paths in `web/` must work under that subpath.
